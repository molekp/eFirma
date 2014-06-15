using System.Web.Mvc;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse.Taxes;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers.Warehouse.Tax
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE + "," + ConstantsHelper.ADMIN_ROLE)]
    public class TaxController : Controller
    {

        TaxManagementLogic TaxLogic { get; set; } 

        public ActionResult Index()
        {
            ViewBag.Message = "Your tax page.";
            TaxLogic = RepositoryLogicFactory.GetTaxManagementLogic();

            return View(TaxLogic.GetAllTaxes());
        }

        public ActionResult AddTax()
        {
            ViewBag.Message = "Here add tax.";
            return View();
        }

        [HttpPost]
        public ActionResult AddTax(TaxAddDto a_taxAddDto)
        {
            if (false == ModelState.IsValid)
            {
                ViewBag.Message = "Form invalid";
                return View();
            }

            TaxLogic = RepositoryLogicFactory.GetTaxManagementLogic();
            if(false == TaxLogic.AddTax(a_taxAddDto))
            {
                ViewBag.Message = "Tax add failed";    
            }

            ViewBag.Message = "Tax successfully added";

            return View();
        }

        [HttpGet]
        public ActionResult EditTax(int idTax)
        {
            ViewBag.Message = "Tax edit";
            TaxLogic = RepositoryLogicFactory.GetTaxManagementLogic();
            return View(TaxLogic.GetTax(idTax));
        }

        [HttpPost]
        public ActionResult EditTax(TaxEditDto a_taxEditDto)
        {
            if (false == ModelState.IsValid)
            {
                ViewBag.Message = "Form invalid";
                return View();
            }
            TaxLogic = RepositoryLogicFactory.GetTaxManagementLogic();
            if(false == TaxLogic.SaveTax(a_taxEditDto))
            {
                ViewBag.Message = "Tax edit failed";
            }else
            {
                ViewBag.Message = "Tax successfully edited";    
            }
            return RedirectToAction("Index", "Tax");
        }

        [HttpGet]
        public ActionResult DeleteTax(int idTax)
        {
            TaxLogic = RepositoryLogicFactory.GetTaxManagementLogic();
            if(false == TaxLogic.RemoveTax(idTax))
            {
                return RedirectToAction("Index", "Tax");
            }
            return RedirectToAction("Index", "Tax");
        }
    }
}
