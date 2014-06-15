using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers.Warehouse
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE + "," + ConstantsHelper.ADMIN_ROLE)]
    public class WarehouseController : Controller
    {
        public IWarehouseLogic WarehouseLogic { get; private set; }

        public WarehouseController()
        {
            WarehouseLogic = RepositoryLogicFactory.GetWarehouseLogic();
        }


        public ActionResult Index()
        {
            ViewBag.Message = "Your Warehousepage";
            return View();
        }

        public ActionResult DisplayWarehouses()
        {
            var model = WarehouseLogic.GetAllWarehouses();

            return View(model);
        }

        public ActionResult AddWarehouse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWarehouse(AddWarehouseDto a_addWarehouse)
        {
            if (ModelState.IsValid)
            {
                if (true == WarehouseLogic.AddWarehouse(a_addWarehouse))
                {
                    ModelState.AddModelError("AddWarehouse", Resource.AddWarehouseSuccessful);
                    a_addWarehouse = new AddWarehouseDto();
                }
                else
                {
                    ModelState.AddModelError("AddWarehouse", Resource.createdError);
                }
            }

            return View(a_addWarehouse);
        }

        public ActionResult ManageWarehouse(int a_idWarehouse)
        {
            var model = WarehouseLogic.GetWarehouseForManage(a_idWarehouse);

            return View(model);
        }

        [HttpPost]
        public ActionResult ManageWarehouse(EditWarehouseDto a_editWarehouse)
        {
            if (!ModelState.IsValid || false == WarehouseLogic.EditWarehouse(a_editWarehouse))
            {
                ModelState.AddModelError("ManageWarehouse", Resource.editError);
            }

            a_editWarehouse = WarehouseLogic.GetWarehouseForManage(a_editWarehouse.IdWarehouse);

            return View(a_editWarehouse);
        }

        public ActionResult RemoveProductWarehouseFromWarehouse(int a_idWarehouse, int a_idProductWarehouse)
        {
            WarehouseLogic.RemoveProductWarehouseFromWarehouse(a_idWarehouse, a_idProductWarehouse);

            return RedirectToAction("ManageWarehouse", new { a_idWarehouse = a_idWarehouse });
        }

        public ActionResult RemoveServiceWarehouseFromWarehouse(int a_idWarehouse, int a_idServiceWarehouse)
        {
            WarehouseLogic.RemoveServiceWarehouseFromWarehouse(a_idWarehouse, a_idServiceWarehouse);

            return RedirectToAction("ManageWarehouse", new { a_idWarehousea_ = a_idWarehouse });
        }



        public ActionResult ManageProductWarehouse(int a_idProductWarehouse, int a_idWarehouse)
        {
            var model = WarehouseLogic.GetProductWarehouseForManage(a_idProductWarehouse,a_idWarehouse);
            ViewBag.ReturnUrl = Url.Action("ManageWarehouse", "Warehouse", new { a_idWarehouse });
            return View(model);
        }

        [HttpPost]
        public ActionResult ManageProductWarehouse(ManageProductWarehouseDto a_manageWarehouse)
        {
            if (!ModelState.IsValid || false == WarehouseLogic.EditProductWarehouse(a_manageWarehouse))
            {
                ModelState.AddModelError("ManageWarehouse", Resource.editError);
            }

            a_manageWarehouse = WarehouseLogic.GetProductWarehouseForManage( a_manageWarehouse.IdProductWarehouse, a_manageWarehouse.IdWarehouse);
            ViewBag.ReturnUrl = Url.Action("ManageWarehouse", "Warehouse", new { a_manageWarehouse.IdWarehouse });
            return View(a_manageWarehouse);
        }


        public ActionResult RemoveItemFromProductWarehouse(int a_itemid, int a_idProductWarehouse, int a_idWarehouse)
        {
            WarehouseLogic.RemoveProductItemFromProductWarehouse(a_itemid, a_idProductWarehouse);

            return RedirectToAction("ManageWarehouse", new { a_idWarehouse = a_idWarehouse });
        }
    }
}
