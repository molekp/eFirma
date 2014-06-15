using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessLogic.DTOs.EStore.EStoreDisplayDto;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore;

namespace Project.Controllers.EStore.Display
{
    public class EStoreDisplayController : Controller
    {
        //
        // GET: /EStoreDisplay/
        private IEStoreDisplayLogic EStoreDisplayLogic { get; set; }

        public EStoreDisplayController()
        {
            EStoreDisplayLogic = RepositoryLogicFactory.GetEStoreDisplayLogic();
        }

        public ActionResult Index(int eStore, int type = 1, int category = 0)
        {
            var model = EStoreDisplayLogic.GetEStoreDisplayIndexDto(eStore,type);

            return View(model);
        }

        public ActionResult Display(int eStore, int type = 1, int item = 0)
        {
            var model = EStoreDisplayLogic.GetEStoreDisplayDetailDto(eStore, type, item);

            return View(model);
        }

        public ActionResult Basket(int eStore)
        {
            return View();
        }


    }
}
