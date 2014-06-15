using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Supplies
{
    public class ProductDto
    {

        public int IdProduct { get; set; }

        [Display(Name = "itemName", ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "itemPrice", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "itemQuantity", ResourceType = typeof(Resource))]
        public double Quantity { get; set; }

        [Display(Name = "itemWarehouses", ResourceType = typeof(Resource))]
        public List<SelectListItem> Warehouses { get; set; }

        [Display(Name = "itemProductWarehouses", ResourceType = typeof (Resource))]
        public List<SelectListItem> ProductWarehouses { get; set; }

        public int IdWarehouse { get; set; }

        public int IdProductWarehouse { get; set; }

        public List<AttributeDto> Attributes { get; set; }

        //public int IdSaleType { get; set; }

        public int IdProductType { get; set; }

        public int IdSupply { get; set; }

        public int State { get; set; }

        public string SaleTypeName { get; set; }

        public string Vin { get; set; }
        
    }
}
