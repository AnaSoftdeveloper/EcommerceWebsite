using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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

                _categoryRepo.Add(category);
                _categoryRepo.save();
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
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==Id);// find only works with primary
         
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
                _categoryRepo.Update(obj);
                _categoryRepo.save();
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
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==Id);// find only works with primary
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
        public IActionResult DeleteCategory(int? Id)
        {
            Category? category = _categoryRepo.Get(u => u.Id == Id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepo.Remove(category);
            _categoryRepo.save();
            TempData["success"] = "Category is deleted successfully";
            
            return RedirectToAction("Index");

        }
    }
}
