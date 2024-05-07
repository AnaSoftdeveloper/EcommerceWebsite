using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
            

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {

          List<Product> products = _productRepository.GetAll().ToList();
          IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().ToList().Select(u => new SelectListItem() {
            Text=u.Name,
            Value =u.Id.ToString()
            });

            ViewBag.categoryList = categoryList;
           return View(products);
        }

        [HttpGet]
        public IActionResult Upsert(int? Id)  //Create and Update=UpSert
        {
            IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().ToList().Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            // ViewBag.categoryList = categoryList;
            //  ViewData["categoryList"] = categoryList;
            ProductVM productVM = new ProductVM()
            {
                CategoryList = categoryList,
                Product = new Product()
            };

            if (Id == null || Id == 0) 
            { 
                //create
             return View(productVM);
            }
            else
            {
                //update
                Product product = _productRepository.Get(u=> u.Id == Id);
                productVM.Product = product;
                return View(productVM);
            }         
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    wwwRootPath = Path.GetFileName(wwwRootPath);
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //Delete The old Image
                        string oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\Images\Product\"+ fileName;
                }
                if(productVM.Product.Id == 0)
                {
                    _productRepository.Add(productVM.Product);
                }
                else
                {
                    _productRepository.Update(productVM.Product);
                }
             
                _productRepository.save();

                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {  //Handling when ModelState is not valid, populate dropdown and return
                IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().ToList().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });



                // ViewBag.categoryList = categoryList;
                //  ViewData["categoryList"] = categoryList;
                productVM.CategoryList = categoryList;
                return View(productVM);
            }         
        }


        [HttpPost]
        public IActionResult Edit(ProductVM productDto)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Title = productDto.Product.Title,
                    Description = productDto.Product.Description,
                    Author = productDto.Product.Author,
                    Price = productDto.Product.Price,
                    Price100 = productDto.Product.Price100,
                    Price50 = productDto.Product.Price50,
                    ISBN = productDto.Product.ISBN
                };

                _productRepository.Update(product);
                _productRepository.save();
                TempData["success"] = "Category is updated successfully";
                return RedirectToAction("Index");
            }
        
            return View();
        }

        
        public IActionResult Delete(int? Id)
        {
            if(Id == null ||Id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _productRepository.Get(u=>u.Id == Id);

            if(productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);

        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
           Product? product = _productRepository.Get(u=>u.Id ==Id);
            if(product == null)
            {
                return NotFound();
            }
           _productRepository.Remove(product);
            _productRepository.save();
            TempData["success"] = "Category removed successfully";
            return RedirectToAction("Index");
        }
    }
}
