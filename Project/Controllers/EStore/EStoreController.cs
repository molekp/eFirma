using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BussinessLogic.DTOs.EStore;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using BussinessLogic.DTOs.EStore.EStoreShipmentType;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore;
using Entit = Database.Entities.EStore;

namespace Project.Controllers.EStore
{
    public class EStoreController : Controller
    {
        public IEStoreManagementLogic EStoreManagementLogic { get; set; }

        public EStoreController()
        {
            EStoreManagementLogic = RepositoryLogicFactory.GetEStoreLogic();
        }
        //
        // GET: /EStore/
        public ActionResult Index()
        {
            var model = EStoreManagementLogic.GetAllEStores();
            return View(model);
        }

        public ActionResult AddEStore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEStore(AddEStoreDto model)
        {
            var isTrue = EStoreManagementLogic.AddEStore(model);
            return isTrue ? RedirectToAction("Index") : RedirectToRoute("~/Shared/Error");
        }

        public ActionResult DeleteEStore(int a_idEStore)
        {
            var isTrue = EStoreManagementLogic.DeleteEStore(a_idEStore);

            return isTrue ? RedirectToAction("Index") : RedirectToRoute("~/Shared/Error"); //nie dziala - zrobic storne bledu :)
        }

        public ActionResult ManageEStore(int a_idEStore)
        {
            var model = EStoreManagementLogic.GetEStore(a_idEStore);

            return View(model);
        }

        public ActionResult ManageWarehouseEStore(int a_idEStore)
        {
            var model = EStoreManagementLogic.GetWarehouses(a_idEStore);
            model.IdEStore = a_idEStore;

            return View(model);
        }

        public ActionResult DeleteEStoreWarehouse(int a_idEStore, int a_idWarehouse)
        {
            var isTrue = EStoreManagementLogic.DeleteEStoreWarehouse(a_idEStore, a_idWarehouse);

            return isTrue ? RedirectToAction("ManageWarehouseEStore", new { a_idEStore = a_idEStore}) : RedirectToRoute("Error"); //nie wiem czy to poprawnie :)
        }

        public ActionResult AddEStoreWarehouse(int a_idEStore, int a_idWarehouse)
        {
            var isTrue = EStoreManagementLogic.AddEStoreWarehouse(a_idEStore, a_idWarehouse);

            return isTrue ? RedirectToAction("ManageWarehouseEStore", new { a_idEStore = a_idEStore }) : RedirectToRoute("Error"); //nie wiem czy to poprawnie :)
        }

        public ActionResult ManageCategoryEStore(int a_idEStore)
        {
            var model = EStoreManagementLogic.GetCategory(a_idEStore);            

            return View(model);
        }

        public ActionResult AddEStoreCategory(int a_idEStore)
        {
            var model = new AddEStoreCategoryDto() {IdEStore = a_idEStore};
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEStoreCategoryWithModel(AddEStoreCategoryDto model)
        {
            var isTrue = EStoreManagementLogic.AddEStoreCategory(model);
            return isTrue ? RedirectToAction("ManageCategoryEStore", new { a_idEStore = model.IdEStore }) : RedirectToRoute("Error");
        }

        public ActionResult EditEStoreCategory(int a_category)
        {
            var model = EStoreManagementLogic.GetOneOfCategoryEdit(a_category);
            return View(model);
        }

        public ActionResult DeleteEStoreCategory(int a_idEStore, int a_category)
        {
            var isTrue = EStoreManagementLogic.DeleteEStoreCategory(a_category);

            return isTrue ? RedirectToAction("ManageCategoryEStore", new { a_idEStore = a_idEStore }) : RedirectToRoute("Error"); //nie wiem czy to poprawnie :)
        }

        [HttpPost]
        public ActionResult EditEStoreCategoryWithModel(EditEStoreCategoryDto model)
        {
            var isTrue = EStoreManagementLogic.EditEStoreCategory(model);
            return isTrue ? RedirectToAction("ManageCategoryEStore", new { a_idEStore = model.IdEStore }) : RedirectToRoute("Error");
        }

        public ActionResult ManageShipmentTypeEStore(int a_idEStore)
        {
            var model = EStoreManagementLogic.GetShipmentType(a_idEStore);

            return View(model);
        }

        public ActionResult AddEStoreShipmentType(int a_idEStore)
        {
            var model = new AddOrEditEStoreShipmentTypeDto { IdEStore = a_idEStore };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEStoreShipmentTypWithModel(AddOrEditEStoreShipmentTypeDto model)
        {
            var isTrue = EStoreManagementLogic.AddEStoreShipmentType(model);
            return isTrue ? RedirectToAction("ManageShipmentTypeEStore", new { a_idEStore = model.IdEStore }) : RedirectToRoute("Error");
        }

        public ActionResult EditEStoreShipmentType(int a_idEStore,int a_shipmentType)
        {
            var model = EStoreManagementLogic.GetOneOfShipmentTypeEdit(a_shipmentType);
            return View(model);
        }

        public ActionResult DeleteEStoreShipmentType(int a_shipmentType, int a_idEStore)
        {
            var isTrue = EStoreManagementLogic.DeleteEStoreShipmentType(a_shipmentType, a_idEStore);

            return isTrue ? RedirectToAction("ManageShipmentTypeEStore", new { a_idEStore = a_idEStore }) : RedirectToRoute("Error"); //nie wiem czy to poprawnie :)
        }

        [HttpPost]
        public ActionResult EditEStoreShipmentTypWithModel(AddOrEditEStoreShipmentTypeDto model)
        {
            var isTrue = EStoreManagementLogic.EditEStoreShipmentType(model);
            return isTrue ? RedirectToAction("ManageShipmentTypeEStore", new { a_idEStore = model.IdEStore }) : RedirectToRoute("Error");
        }
    }
}
