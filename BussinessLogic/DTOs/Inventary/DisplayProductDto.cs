using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Inventary
{
    public class DisplayProductDto
    {
        [Display(Name = "itemId", ResourceType = typeof(Resource))]
        public int IdProduct { get; set; }

        [Display(Name = "itemName", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "itemPrice", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "itemQuantity", ResourceType = typeof(Resource))]
        public double Quantity { get; set; }

        [Display(Name = "itemState", ResourceType = typeof(Resource))]
        public int ItemState { get; set; }

        [Display(Name = "itemExpirationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationTime { get; set; }

        [Display(Name = "itemType", ResourceType = typeof(Resource))]
        public string ProductTypeName { get; set; }

        [Display(Name = "taxName", ResourceType = typeof(Resource))]
        public string TaxName { get; set; }

        [Display(Name = "taxValue", ResourceType = typeof(Resource))]
        public double TaxValue { get; set; }

        [Display(Name = "itemSaleType", ResourceType = typeof(Resource))]
        public string SaleTypeName { get; set; }

        public List<DisplayProductAttributeDto> DisplayProductAttributeDtos { get; set; }
    }
}