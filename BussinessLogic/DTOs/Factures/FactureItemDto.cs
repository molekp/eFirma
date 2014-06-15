using System.ComponentModel.DataAnnotations;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class FactureItemDto
    {

        [Display(Name = "idFactureItem", ResourceType = typeof(Resource))]
        public int IdItem { get; set; }

        [Display(Name = "factureItemName", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "factureItemPricePerUnit", ResourceType = typeof(Resource))]
        public decimal PricePerUnit { get; set; }

        [Display(Name = "factureItemPriceWithTax", ResourceType = typeof(Resource))]
        public decimal PriceWithTax { get; set; }

        [Display(Name = "factureItemPriceWithTax", ResourceType = typeof(Resource))]
        public decimal PriceWithoutTax { get; set; }

        [Display(Name = "factureItemPriceTax", ResourceType = typeof(Resource))]
        public decimal PriceTax { get; set; }

        [Display(Name = "factureItemQuantity", ResourceType = typeof(Resource))]
        public double Quantity { get; set; }

        [Display(Name = "factureItemSaleType", ResourceType = typeof(Resource))]
        public string SaleTypeName { get; set; }

        [Display(Name = "factureItemPKWiU", ResourceType = typeof(Resource))]
        public string PKWiU { get; set; }

        [Display(Name = "factureItemTax", ResourceType = typeof(Resource))]
        public double TaxValue { get; set; }
    }
}
