using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessLogic.DTOs.Factures;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Factures;
using BussinessLogic.Helpers;
using iTextSharp.text;


namespace Project.Controllers.Facture
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE + "," + ConstantsHelper.ADMIN_ROLE)]
    public class FactureController : Controller
    {
        public IFactureLogic FactureLogic { get; set; }

        public FactureController()
        {
            FactureLogic = RepositoryLogicFactory.GetFactureLogic();
        }


        //
        // GET: /Facture/

        public ActionResult Index()
        {
            return View(FactureLogic.GetAllFactures());
        }

        public ActionResult ViewFacture(int idFacture, string a_returnUrl)
        {
            var model = FactureLogic.ViewFacture(idFacture);
            ViewBag.a_returnUrl = a_returnUrl;
            model.Currencies = FactureLogic.getCurrencies();
            return View(model);
        }

        public List<SelectListItem> paymentList()
        {
            return new List<SelectListItem>(){
                new SelectListItem{Selected = true, Text = "gotówka", Value = "gotówka"},
                new SelectListItem{Selected = false,Text = "karta płatnicza",Value = "karta płatnicza"},
                new SelectListItem{Selected = false, Text = "przelew", Value = "przelew"},
                new SelectListItem{Selected = false, Text = "zaliczka", Value = "zaliczka"}
            };
        }

        public ActionResult AddFacture(int idDistribution, string a_returnUrl = null)
        {
            var idFacture = FactureLogic.idFacture(idDistribution);
            if(idFacture > 0)
            {
                RedirectToAction("ViewFacture", "Facture", idFacture);
            }
            var model = FactureLogic.AddFacture(idDistribution);
            if(model != null)
            {
                model.Items.Reverse();
                ViewBag.Message = "Create Invoice";
                ViewBag.a_returnUrl = a_returnUrl;
                model.PaymentList = paymentList();
                return View(model);
            }
            if(a_returnUrl != null && a_returnUrl.Contains("?")){
                return Redirect(a_returnUrl + "&result=false");
            }
            return Redirect(a_returnUrl + "?result=false");
        }

        public ActionResult AddFactureEdited(FactureAddDto factureAddDto)
        {
            factureAddDto.ItemIds = (int[])TempData["facture_ItemIds"];
            ViewBag.a_returnUrl = (string)TempData["facture_a_returnUrl"];
            TempData["facture_ItemIds"] = null;
            TempData["facture_a_returnUrl"] = null;
            factureAddDto.Items = FactureLogic.getItems(factureAddDto.ItemIds.ToList());
            factureAddDto = FactureLogic.getSum(factureAddDto);
            factureAddDto.PaymentList = paymentList();
            return View("AddFacture", factureAddDto);
        }

        [HttpPost]
        public ActionResult SaveFacture(FactureAddDto factureAddDto, string a_returnUrl)
        {
            if (ModelState.IsValid)
            {
                var idFacture = FactureLogic.SaveFacture(factureAddDto);
                if (idFacture > 0)
                {
                    return RedirectToAction("ViewFacture", "Facture", new{idFacture = idFacture, a_returnUrl = a_returnUrl});
                }
            }
            ViewBag.Message = "Saving Facture failed";
            TempData["facture_ItemIds"] = factureAddDto.ItemIds;
            TempData["facture_a_returnUrl"] = a_returnUrl;
            return RedirectToAction("AddFactureEdited", "Facture", factureAddDto);
        }

        
        public void PrintFacture(int idFacture, int idCurrency)
        {
            var facture = FactureLogic.ViewFacture(idFacture);
            Document pdf = new Document(PageSize.A4,25,25,15,15);
            var exchange = new Exchange(FactureLogic.getExchange(facture.IdFacture, idCurrency));
            var output = PDFMaker.createPDF(pdf, new MemoryStream(), facture, exchange);

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=FV{0}.pdf", facture.FactureNr));

            Response.BinaryWrite(output.ToArray());
            Response.End();
            Response.Flush();
            Response.Clear();
        }

        public ActionResult ViewFirms()
        {
            return View(FactureLogic.GetAllFirms());
        }

        public ActionResult ViewFirm(int idFirm)
        {
            var model = FactureLogic.ViewFirm(idFirm);
            return View(model);
        }


        [HttpGet]
        public ActionResult EditFirm(int idFirm)
        {
            ViewBag.Message = "Firm edit";
            return View(FactureLogic.GetFirm(idFirm));
        }

        [HttpPost]
        public ActionResult EditFirm(FirmAddDto a_FirmAddDto)
        {
            if (false == ModelState.IsValid)
            {
                ViewBag.Message = "Form invalid";
                return View();
            }
            if (false == FactureLogic.SaveFirm(a_FirmAddDto))
            {
                ViewBag.Message = "Firm edit failed";
            }
            else
            {
                ViewBag.Message = "Firm successfully edited";
            }
            return RedirectToAction("ViewFirm", "Facture", new { idFirm = a_FirmAddDto.IdFirm });
        }

        public ActionResult AddFirm()
        {
            ViewBag.Message = "Here add Firm.";
            var model = new FirmAddDto();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddFirm(FirmAddDto a_addFirmDto)
        {
            if (ModelState.IsValid)
            {
                var id = FactureLogic.AddFirm(a_addFirmDto);
                if (id > 0)
                {
                    return RedirectToAction("ViewFirm", "Facture", new{ idFirm = id});
                }
            }
            ViewBag.Message = "Adding Firm failed";
            return View();
        }

        [HttpGet]
        public ActionResult DeleteFirm(int idFirm)
        {
            if (false == FactureLogic.RemoveFirm(idFirm))
            {
                return RedirectToAction("ViewFirms", "Facture");
            }
            return RedirectToAction("ViewFirms", "Facture");
        }

        
    }
}
