using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.Dtos;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

          List<Product> products =  _productRepository.GetAll().ToList();
           return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
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
            return View(product);
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
