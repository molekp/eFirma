using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.WarehouseDtos.Supplies
{
    public class ProductAddDto
    {
        [Display(Name = "itemName", ResourceType = typeof(Resource))]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "itemPrice", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "itemQuantity", ResourceType = typeof(Resource))]
        public double Quantity { get; set; }

        [Display(Name = "itemSaleType", ResourceType = typeof(Resource))]
        public List<SelectListItem> SaleTypes { get; set; }

        [Display(Name = "itemType", ResourceType = typeof (Resource))]
        public List<SelectListItem> ProductTypes { get; set; }
        
        public List<AttributeDto> Attributes { get; set; }

        public int IdSaleType { get; set; }

        public int IdProductType { get; set; }

        public int IdSupply { get; set; }

        [Display(Name = "itemExpirationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        public DateTime ExpirationTime { get; set; }
    }
}
