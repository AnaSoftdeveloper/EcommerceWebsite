using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
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
            if (createCategoryDtos.Name == createCategoryDtos.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The displayOrder cannot exactly match the name.");
            }

            if (!createCategoryDtos.Name.IsNullOrEmpty())
            {

                if (createCategoryDtos.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("", "test is an invalid value");
                }
            }
            if (ModelState.IsValid)
            {


                Category category = new Category()
                {
                    Name = createCategoryDtos.Name,
                    DisplayOrder = createCategoryDtos.DisplayOrder
                };

                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category is created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
               return NotFound();
            }
           // there are multiple ways to retrieve a record
            Category? categoryFromDb = _db.Categories.Find(Id);// find only works with primary
         
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {   
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category is updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
               return NotFound();
            }
            // there are multiple ways to retrieve a record
            Category? categoryFromDb = _db.Categories.Find(Id);// find only works with primary
                                                               //  Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==Id);//it try to find whether there is any record, if not, It returns null object,and we can use linq op[erations, use contain or other modificatio but find ony works for primary key
                                                               //  Category? categoryFromDb2 = _db.Categories.Where(u=> u.Id==Id).FirstOrDefault();// typically I use second approach but If we need filtering in some situation, we use this approach
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(Category obj)
        {
           Category? category = _db.Categories.Find(obj.Id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            TempData["success"] = "Category is deleted successfully";
            _db.SaveChanges();
            
            return RedirectToAction("Index");

        }
    }
}
