using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        { 
            _db= db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(CreateCategoryDtos createCategoryDtos)
        {
          if(createCategoryDtos.Name == createCategoryDtos.DisplayOrder.ToString()) {
                ModelState.AddModelError("Name", "The displayOrder cannot exactly match the name.");
            }

            if (createCategoryDtos.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "test is an invalid value");
            }

            if(ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Name = createCategoryDtos.Name,
                    DisplayOrder = createCategoryDtos.DisplayOrder
                };

                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        
        }
    }
}
