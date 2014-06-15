using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Inventary
{
    public  class ManageProductDto
    {
        [Display(Name = "itemId", ResourceType = typeof(Resource))]
        public int IdProduct { get; set; }

        [Display(Name = "itemName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "itemPrice", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "itemQuantity", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public double Quantity { get; set; }

        [Display(Name = "itemState", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public int ItemState { get; set; }

        [Display(Name = "itemExpirationDate", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "itemExpirationTime", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "requiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Time)]
        public DateTime ExpirationTime { get; set; }

        [Display(Name = "itemType", ResourceType = typeof(Resource))]
        public string ProductTypeName { get; set; }

        [Display(Name = "itemChoicesItemType", ResourceType = typeof(Resource))]
        public List<SelectListItem> ChoicesProductTypes { get; set; }

        public int SelectedChoiceIdProductType { get; set; }

        [Display(Name = "taxName", ResourceType = typeof(Resource))]
        public string TaxName { get; set; }

        [Display(Name = "taxValue", ResourceType = typeof(Resource))]
        public double TaxValue { get; set; }

        //[Display(Name = "itemChoicesTax", ResourceType = typeof(Resource))]
        //public List<SelectListItem> ChoicesTaxes { get; set; }

        //public SaleTypeName SelectedChoiceIdTax{ get; set; }

        [Display(Name = "itemSaleType", ResourceType = typeof(Resource))]
        public string SaleTypeName { get; set; }

        [Display(Name = "itemChoicesSaleType", ResourceType = typeof(Resource))]
        public List<SelectListItem> ChoicesSaleTypes{ get; set; }

        public int SelectedChoiceIdSaleType{ get; set; }

        [Display(Name = "itemAddAttribute", ResourceType = typeof(Resource))]
        public AddAttributeDto AddAttributeDto { get; set; }

        public List<ManageProductAttributeDto> ManageProductAttributeDtos { get; set; }
    }
}