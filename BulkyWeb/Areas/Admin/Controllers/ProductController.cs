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
            

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().ToList().Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.categoryList = categoryList;
            return View();

        }


        [HttpPost]
        public IActionResult Create(ProductDto obj)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Title = obj.Title,
                    Description = obj.Description,
                    Author = obj.Author,
                    ISBN = obj.ISBN,
                    Price = obj.Price,
                    Price50 = obj.Price50,
                    Price100 = obj.Price100,
                    ListPrice = obj.ListPrice,
                };
                _productRepository.Add(product);
                _productRepository.save();

                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null ||Id == 0)
            {
                return NotFound();
                
            }

           Product product = _productRepository.Get(u=>u.Id == Id);
            if (product == null)
            {
                return NotFound();
            }

            ProductDto productDto = new ProductDto()
            {
                Title = product.Title,
                Description = product.Description,
                Author = product.Author,
                Price = product.Price,
                Price100 = product.Price100,
                Price50 = product.Price50,
                ISBN = product.ISBN
            };        
            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Title = productDto.Title,
                    Description = productDto.Description,
                    Author = productDto.Author,
                    Price = productDto.Price,
                    Price100 = productDto.Price100,
                    Price50 = productDto.Price50,
                    ISBN = productDto.ISBN
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
