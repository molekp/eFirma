using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Supplies;
using BussinessLogic.Helpers;
using ResourceLibrary;
using System.Linq;

namespace Project.Controllers.Warehouse.Supply
{
    [Authorize(Roles = ConstantsHelper.EMPLOYEE_ROLE + "," + ConstantsHelper.ADMIN_ROLE)]
    public class SupplyController : Controller
    {
        public ISupplyLogic SupplyLogic { get; set; }
        public IWarehouseLogic WarehouseLogic { get; set; }


        public SupplyController()
        {
            SupplyLogic = RepositoryLogicFactory.GetSupplyLogic();
            WarehouseLogic = RepositoryLogicFactory.GetWarehouseLogic();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your supply page.";

            return View(SupplyLogic.GetAllSupplies());
        }

        public ActionResult AddSupply()
        {
            ViewBag.Message = "Here add supply.";
            var model = new SupplyAddDto {RealizationTime = DateTime.Now};
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddSupply(SupplyAddDto a_addSupplyDto)
        {
            if (ModelState.IsValid)
            {
                if (SupplyLogic.AddSupply(a_addSupplyDto))
                {
                    ViewBag.Message = "Supply successfully added";
                    return View(a_addSupplyDto);
                }
            } 
            ViewBag.Message = "Adding Supply failed";
            
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct(int idSupply, int a_idProductType = 0)
        {
            var model = new ProductAddDto();
            model.IdSupply = idSupply;
            model.ProductTypes = SupplyLogic.GetProductTypes(a_idProductType);
            model.SaleTypes = SupplyLogic.GetSaleTypes();
            model.IdProductType = a_idProductType;
            model.Attributes = SupplyLogic.GetAttributes(a_idProductType);
            model.ExpirationTime = DateTime.Now.Date;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductAddDto a_product)
        {
            if(ModelState.IsValid)
            {
                SupplyLogic.AddProductToSupply(a_product.IdSupply, SupplyLogic.AddProduct(a_product));
            }
            return RedirectToAction("ViewSupply", "Supply", new { idSupply = a_product.IdSupply});
        }

        [HttpGet]
        public ActionResult EditSupply(int idSupply)
        {
            ViewBag.Message = "Supply edit";
            return View(SupplyLogic.GetSupply(idSupply));
        }

        [HttpPost]
        public ActionResult EditSupply(SupplyEditDto a_supplyAddDto)
        {
            if (false == ModelState.IsValid)
            {
                ViewBag.Message = "Form invalid";
                return View();
            }
            if (false == SupplyLogic.SaveSupply(a_supplyAddDto))
            {
                ViewBag.Message = "Supply edit failed";
            }
            else
            {
                ViewBag.Message = "Supply successfully edited";
            }
            return RedirectToAction("Index", "Supply");
        }

        [HttpGet]
        public ActionResult DeleteSupply(int idSupply)
        {
            if (false == SupplyLogic.RemoveSupply(idSupply))
            {
                return RedirectToAction("Index", "Supply");
            }
            return RedirectToAction("Index", "Supply");
        }

        [HttpGet]
        public ActionResult SendSupply(int idSupply)
        {
            // wyslij Supply
            if (false == SupplyLogic.SendSupply(idSupply))
            {
                return RedirectToAction("Index", "Supply");
            }
            return RedirectToAction("Index", "Supply");
        }

        [HttpGet]
        public ActionResult StoreProduct(int idProduct, int idSupply, int idWarehouse = 0, int idProductWarehouse = 0)
        {
            var model = SupplyLogic.StoreProduct(idProduct);
            model.Warehouses = SupplyLogic.GetWarehousesWithProducts(idWarehouse);
            model.IdWarehouse = idWarehouse;
            model.IdSupply = idSupply;
            if (model.Warehouses.Count == 1)
            {
                model.ProductWarehouses = SupplyLogic.GetProductWarehouses(Convert.ToInt32(model.Warehouses.First().Value), idProductWarehouse);
            }else
            if (idWarehouse > 0)
            {
                model.ProductWarehouses = SupplyLogic.GetProductWarehouses(idWarehouse, idProductWarehouse);
            }else
            {
                model.ProductWarehouses = new List<SelectListItem>();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult StoreProduct(ProductDto a_productDto)
        {
            if (false == ModelState.IsValid)
            {
                ViewBag.Message = "Form invalid";
                return RedirectToAction("StoreProduct",
                                        new
                                            {
                                                    idProduct = a_productDto.IdProduct,
                                                    idSupply = a_productDto.IdSupply,
                                                    idWarehouse = a_productDto.IdWarehouse,
                                                    idProductWarehouse = a_productDto.IdProductWarehouse
                                            });
            }
            int idProduct = SupplyLogic.StoreProduct(a_productDto);
            if (idProduct > 0)
            {
                ViewBag.Message = "Product successfully stored.";
                if(SupplyLogic.RemoveProductFromSupply(idProduct, a_productDto.IdSupply) == false)
                {
                    ViewBag.Message = ViewBag.Message + " Product not removed from Supply";
                }else
                {
                    if(SupplyLogic.SupplyRepository.GetSupply(a_productDto.IdSupply).ProductItems.Count == 0){
                        return RedirectToAction("DeleteSupply", "Supply", new {idSupply = a_productDto.IdSupply});
                    }
                }
            }
            else
            {
                ViewBag.Message = "Store failed";
            }
            return RedirectToAction("ViewSupply", "Supply", new {idSupply = a_productDto.IdSupply});
        }
        
        public ActionResult RemoveProduct(int idProduct, int idSupply)
        {
            SupplyLogic.RemoveProduct(idProduct, idSupply);
            return RedirectToAction("ViewSupply", "Supply", new { IdSupply = idSupply });
        }

        public ActionResult ViewSupply(int idSupply)
        {
            var model = SupplyLogic.ViewSupply(idSupply);
            return View(model);
        }

        
        public ActionResult ViewProduct(int idProduct)
        {
            return RedirectToAction("DisplayItem", "Inventary", idProduct);
        }
        
    }
}
