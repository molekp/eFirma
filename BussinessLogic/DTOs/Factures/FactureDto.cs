using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class FactureDto
    {

        [Display(Name = "idFacture", ResourceType = typeof(Resource))]
        public int IdFacture { get; set; }

        [Display(Name = "factureNr", ResourceType = typeof (Resource))]
        public string FactureNr { get; set; }

        [Display(Name = "providerName", ResourceType = typeof (Resource))]
        public string ProviderName { get; set; }

        [Display(Name = "providerAddress", ResourceType = typeof (Resource))]
        public string ProviderAddress { get; set; }

        [Display(Name = "providerNIP", ResourceType = typeof (Resource))]
        public string ProviderNIP { get; set; }

        [Display(Name = "providerInfo", ResourceType = typeof (Resource))]
        public string ProviderInfo { get; set; }

        [Display(Name = "providerBankAccountNumber", ResourceType = typeof (Resource))]
        public string ProviderBankAccountNumber { get; set; }

        [Display(Name = "providerBankInfo", ResourceType = typeof (Resource))]
        public string ProviderBankInfo { get; set; }

        [Display(Name = "clientName", ResourceType = typeof (Resource))]
        public string ClientName { get; set; }

        [Display(Name = "clientAddress", ResourceType = typeof (Resource))]
        public string ClientAddress { get; set; }

        [Display(Name = "clientNIP", ResourceType = typeof (Resource))]
        public string ClientNIP { get; set; }

        [Display(Name = "clientInfo", ResourceType = typeof (Resource))]
        public string ClientInfo { get; set; }

        [Display(Name = "creationTime", ResourceType = typeof (Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "realizationTime", ResourceType = typeof (Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime RealizationTime { get; set; }

        [Display(Name = "expirationTime", ResourceType = typeof (Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime ExpirationTime { get; set; }

        [Display(Name = "creationPlace", ResourceType = typeof (Resource))]
        public string CreationPlace { get; set; }

        [Display(Name = "sumWithTax", ResourceType = typeof(Resource))]
        public decimal SumWithTax { get; set; } // sum with tax

        [Display(Name = "inWords", ResourceType = typeof(Resource))]
        public string SumString { get; set; } // sum in words

        [Display(Name = "sumWithoutTax", ResourceType = typeof(Resource))]
        public decimal SumWithoutTax { get; set; } // sum without tax
        
        [Display(Name = "sumOfTax", ResourceType = typeof(Resource))]
        public decimal SumOfTax { get; set; } // sum of tax

        [Display(Name = "paid", ResourceType = typeof(Resource))]
        public decimal Paid { get; set; } // allready paid cash

        [Display(Name = "remain", ResourceType = typeof(Resource))]
        public decimal Remain { get; set; } // Sum - Paid

        [Display(Name = "issuer", ResourceType = typeof(Resource))]
        public string Issuer { get; set; } // who is writing invoice

        [Display(Name = "reciver", ResourceType = typeof(Resource))]
        public string Reciver { get; set; } // who recives invoice

        public List<FactureItemDto> Items { get; set; }

        [Display(Name = "currency", ResourceType = typeof(Resource))]
        public List<SelectListItem> Currencies { get; set; }

        public string Currency { get; set; }
        
        [Display(Name = "payment", ResourceType = typeof(Resource))]
        public string Payment;
    }
}