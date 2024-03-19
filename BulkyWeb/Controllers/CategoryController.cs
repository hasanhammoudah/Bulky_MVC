using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db) {
            _categoryRepository = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();

            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
                                if(obj.Name == obj.DisplayOrder.ToString())
                                  {
                                       ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
                                   }
                                     //         if (obj.Name!=null && obj.Name.ToLower() == "test")
                                       // {
                                         //   ModelState.AddModelError("", "Test is an invalid value");
                                        //}
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(obj);
                _categoryRepository.save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
             Category? categoryFromDb = _categoryRepository.Get(u=>u.Id==id);
            // Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id == id);
         //    Category? categoryFromDb = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
    
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(obj);
                _categoryRepository.save();
                TempData["success"] = "Category Edit successfully";

                return RedirectToAction("Index");

            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepository.Get(u => u.Id == id);
            // Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //    Category? categoryFromDb = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();

            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepository.Remove(obj);
            _categoryRepository.save();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("Index");

         
        }


    }
}
