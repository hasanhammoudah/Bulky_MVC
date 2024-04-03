using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.viewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {



            //ViewBag.CategoryList = CategoryList;
            //   ViewData["CategoryList"] = CategoryList;

            if (id == null || id == 0)
            {
                // create
                return View(new Company());

            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);

            }
        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {

            if (ModelState.IsValid)
            {


         
            if (companyObj.Id == 0)
            {
                _unitOfWork.Company.Add(companyObj);
            }
            else
            {
                _unitOfWork.Company.Update(companyObj);

            }

            _unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");

        }
            else
            {  
             
                return View(companyObj);

    }

        }

      
     
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if ((CompanyToBeDeleted == null))
            {
                return Json(new { success = false, message="Error while deleting" });

            }
        
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = false, message = "Error while deleting" });

        }
        #endregion


    }
}
