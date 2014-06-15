using System.Web.Mvc;
using BussinessLogic.DTOs.Store;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Store;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers.Store
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE+","+ConstantsHelper.ADMIN_ROLE)]
    public class StoreController : Controller
    {
        public IStoreLogic StoreLogic { get; set; }

        public StoreController()
        {
            StoreLogic = RepositoryLogicFactory.GetStoreLogic();
        }
        //
        // GET: /Store/

        public ActionResult Index(string a_message)
        {
            var model = StoreLogic.GetAllDisplayStoreDto();
            ViewBag.Message = a_message;
            return View(model);
        }

        public ActionResult ConcreteStore(int a_storeId)
        {
            var model = StoreLogic.GetDisplayStoreDto(a_storeId);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Index", new { a_message = Resource.chooseStoreFirst});
        }

        public ActionResult Sold(int a_storeId, string a_message=null)
        {
            var model = StoreLogic.GetAllSoldItemDto(a_storeId);
            ViewBag.Message = a_message;
            ViewBag.StoreId = a_storeId;
            return View(model);
        }


        public ActionResult NewSell(int a_storeId)
        {
            return RedirectToAction("AddDistribution", "Distribution",
                                    new { a_returnUrl = Url.Action("ManageSell", "Store", new { a_storeId }) });
        }

        public ActionResult ManageSell(int a_storeId, int a_distributionId)
        {
            var model = StoreLogic.GetStoreSellDto(a_storeId, a_distributionId);

            if (model == null) return RedirectToAction("Index");

            return View(model);
        }

        public ActionResult Complaint(int a_distributionId, int a_itemId,int a_storeId, ItemTypeEnum a_itemType)
        {
            var model = StoreLogic.GetComplaintDto(a_distributionId, a_itemId, a_itemType);
            if (model == null)
            {
                return RedirectToAction("Sold", new { a_storeId, a_message = Resource.complaintExists });
            }
            model.StoreId = a_storeId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Complaint(ComplaintDto a_complaintDto)
        {
            if (ModelState.IsValid)
            {
                if (!StoreLogic.AddComplaint(a_complaintDto))
                {
                    ModelState.AddModelError("",Resource.addComplainError);
                }
                return RedirectToAction("Sold", new { a_storeId = a_complaintDto.StoreId, a_message = Resource.complaintAddSuccess });
            }

            return View(a_complaintDto);
        }

        public ActionResult Returns(int a_distributionId, int a_itemId, int a_storeId, ItemTypeEnum a_itemType)
        {
            var model = StoreLogic.GetReturnDto(a_distributionId, a_itemId, a_itemType);
            model.StoreId = a_storeId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Returns(ReturnDto a_returnDto)
        {
            if (ModelState.IsValid)
            {
                if (!StoreLogic.ReturnItem(a_returnDto))
                {
                    ModelState.AddModelError("", Resource.returnItemError);
                }
                return RedirectToAction("Sold", new { a_storeId = a_returnDto.StoreId, a_message = Resource.returnItemSuccess });
            }

            return View(a_returnDto);
        }
    }
}
