using System.Web.Mvc;
using BussinessLogic.DTOs.Inventary;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers.Warehouse.Inventary
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE + "," + ConstantsHelper.ADMIN_ROLE)]
    public class InventaryController : Controller
    {
        IItemManagementLogic ItemLogic { get; set; }

        public InventaryController()
        {
            ItemLogic = RepositoryLogicFactory.GetItemManagementLogic();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchItems()
        {
            ViewBag.Message = "Your inventary page.";
            
            return View(ItemLogic.GetAllItemsForSearch());
        }

        public ActionResult SearchItemsByAttribute()
        {
            var model = ItemLogic.GetAllItemsForSearchByAttribute();

            return View(model);
        }

        public ActionResult RemoveItemFromProductWarehouse(int a_itemId, int a_specyficWarehouse)
        {
            var model = new RemoveItemFromSpecyficWarehouse
                {
                        IdItem = a_itemId,
                        IdSpecyficWarehouse = a_specyficWarehouse
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult RemoveItemFromProductWarehouse(RemoveItemFromSpecyficWarehouse a_removeItem)
        {
            if (ModelState.IsValid)
            {
                if (false == ItemLogic.RemoveItemFromProductWarehouse(a_removeItem))
                {
                    ModelState.AddModelError("RemoveItemFromProductWarehouse", Resource.removedError);
                }
                else
                {
                    return RedirectToAction("SearchItems");
                }
            }
            return View(a_removeItem);
        }

        public ActionResult DisplayItem(int a_itemId)
        {
            var type = ItemLogic.GetTypeOfItem(a_itemId);
            if (type == null) return RedirectToAction("SearchItems");

            if (type.Equals(ConstantsHelper.PRODUCT_ITEM))
            {
                return RedirectToAction("DisplayProduct", new { a_idProduct = a_itemId });
            }else
                return RedirectToAction("DisplayService", new { a_idService = a_itemId });
        }

        public ActionResult DisplayProduct(int a_idProduct)
        {
            var model = ItemLogic.GetDisplayProductDto(a_idProduct);

            return View(model);
        }

        public ActionResult ManageProduct(int a_idProduct, string a_returnUrl=null)
        {
            var model = ItemLogic.GetManageProductDto(a_idProduct);
            ViewBag.ReturnUrl = a_returnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult ManageProduct(ManageProductDto a_productDto)
        {
            if (ModelState.IsValid)
            {
                if (false == ItemLogic.EditProductItem(a_productDto))
                {
                    ModelState.AddModelError("ManageProduct", Resource.editError);
                }
            }
            a_productDto = ItemLogic.GetManageProductDto(a_productDto.IdProduct);
            a_productDto.AddAttributeDto.Value = "";

            return View(a_productDto);
        }

        public ActionResult DisplayService(int a_idService)
        {
            var model = ItemLogic.GetDisplayServiceDto(a_idService);

            return View(model);
        }

        public ActionResult RemoveAttributeFromProduct(int a_idProduct, int a_idAttribute)
        {
            var model = new RemoteAttributeModel {IdAttribute = a_idAttribute, IdItem = a_idProduct};

            return View(model);
        }

        [HttpPost]
        public ActionResult RemoveAttributeFromProduct(RemoteAttributeModel a_remoteAttributeModel)
        {
            if (ModelState.IsValid)
            {
                if (false == ItemLogic.RemoveAttributeFromProduct(a_remoteAttributeModel))
                {
                    ModelState.AddModelError("RemoveAttributeFromProduct", Resource.removedError);
                }
                else
                {
                    return RedirectToAction("ManageProduct", new {a_idProduct = a_remoteAttributeModel.IdItem});
                }
            }
            return View(a_remoteAttributeModel);
        }
    }
}
