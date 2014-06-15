using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Services;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using BussinessLogic.Helpers;
using Newtonsoft.Json.Linq;
using ResourceLibrary;

namespace Project.Controllers.Warehouse.Distribution
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE + "," + ConstantsHelper.ADMIN_ROLE)]
    public class DistributionController : Controller
    {
        public IDistributionLogic DistributionLogic { get; private set; }

        public DistributionController()
        {
            DistributionLogic = RepositoryLogicFactory.GetDistributionLogic();
        }


        public ActionResult Index()
        {
            var model =  DistributionLogic.GetAllDistributionQueue();

            return View(model);
        }

        public ActionResult DisplayDistribution(int a_distributionId )
        {
            var model = DistributionLogic.GetDisplayDistribution(a_distributionId);

            return View(model);
        }

        public ActionResult PerformDistribution(int a_distributionId,string a_returnUrl = null)
        {
            var model = DistributionLogic.GetPerformDistribution(a_distributionId);
            ViewBag.ReturnUrl = a_returnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult PerformDistribution(PerformDistributionDto a_distributionDto, string a_returnUrl = null)
        {
            var performSuccess = DistributionLogic.PerformDistribution(a_distributionDto.IdDistribution);

            var model = DistributionLogic.GetPerformDistribution(a_distributionDto.IdDistribution);
            ViewBag.ReturnUrl = a_returnUrl;
            if (performSuccess)
            {
                if (a_returnUrl != null) RedirectToLocal(a_returnUrl);
                ViewBag.Message = Resource.distributionPerformedSuccessful;
            }
            else
            {
                ViewBag.Message = Resource.distributionPerformedUnsuccessful;
            }
            return View(model);
        }


        public ActionResult EditDistribution(int a_distributionId, string a_message="")
        {
            var model = DistributionLogic.GetEditDistributionDto(a_distributionId);
            ViewBag.Message = a_message;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditDistribution(EditDistributionDto a_distributionDto)
        {
            if (ModelState.IsValid)
            {
                if (false == DistributionLogic.EditDistributionDto(a_distributionDto))
                {
                    ModelState.AddModelError("EditDistribution", Resource.editError);
                }
            }
            a_distributionDto = DistributionLogic.GetEditDistributionDto(a_distributionDto.IdDistribution);

            return View(a_distributionDto);
        }

        public ActionResult RemoveProductItemFromDistribution(int a_distributionId, int a_itemId,ItemTypeEnum a_itemType)
        {
            var removed = DistributionLogic.RemoveProductItemFrom(a_distributionId, a_itemId,a_itemType);
            if (removed)
                return RedirectToAction("EditDistribution", new {a_distributionId, a_message = "Item removed successfully from distribution" });
            return RedirectToAction("EditDistribution", new {a_distributionId, a_message = "Item not removed from distribution. Pleas contact to administrator." });
        }


        public ActionResult AddDistribution( string a_returnUrl=null)
        {
            var model = DistributionLogic.GetAddDistributionDto();
            ViewBag.ReturnUrl = a_returnUrl;
            return View(model);
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddDistribution(AddDistributionDto a_distributionDto)
        {

            int distributionId = DistributionLogic.AddDistribution(a_distributionDto, User.Identity.Name);
            if (-1 == distributionId)
            {
                ModelState.AddModelError("AddDistribution", Resource.distributionAddError);
                return null;
            }
            var result= new AddDistributionComplete
                {
                        DistributionId = distributionId,
                        RedirectUrl = Url.Action("DisplayDistribution", new {a_distributionId = distributionId})
                };
            return Json(result, "application/json");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
