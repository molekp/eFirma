using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class FactureAddDto
    {

        [Display(Name = "provider", ResourceType = typeof(Resource))]
        public List<SelectListItem> Provider { get; set; }

        [Display(Name = "client", ResourceType = typeof(Resource))]
        public List<SelectListItem> Client { get; set; }

        [Display(Name = "creationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [Required(ErrorMessageResourceName = "creationTimeRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public DateTime CreationTime { get; set; }
        
        [Display(Name = "realizationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [Required(ErrorMessageResourceName = "realizationTimeRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public DateTime RealizationTime { get; set; }

        [Display(Name = "expirationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [Required(ErrorMessageResourceName = "expirationTimeRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public DateTime ExpirationTime { get; set; }

        public int IdFacture { get; set; }

        public int IdDistribution { get; set; }

        private string providerName;
        [Display(Name = "providerName", ResourceType = typeof (Resource))]
        [Required(ErrorMessageResourceName = "providerNameRequiredErrorMsg",
                ErrorMessageResourceType = typeof (Resource))]
        public string ProviderName { get { return providerName ?? ""; } set { providerName = value; } }

        private string providerAddress;
        [Display(Name = "providerAddress", ResourceType = typeof (Resource))]
        [Required(ErrorMessageResourceName = "providerNameRequiredErrorMsg",
                ErrorMessageResourceType = typeof (Resource))]
        public string ProviderAddress { get { return providerAddress ?? ""; } set { providerAddress = value; } }

        [Display(Name = "providerNIP", ResourceType = typeof (Resource))]
        private string providerNIP;
        public string ProviderNIP { get { return providerNIP ?? ""; } set { providerNIP = value; } }

        [Display(Name = "providerInfo", ResourceType = typeof (Resource))]
        private string providerInfo;
        public string ProviderInfo { get { return providerInfo ?? ""; } set { providerInfo = value; } }

        [Display(Name = "providerBankAccountNumber", ResourceType = typeof (Resource))]
        private string providerBankAccountNumber;
        public string ProviderBankAccountNumber { get { return providerBankAccountNumber ?? ""; } set { providerBankAccountNumber = value; } }

        [Display(Name = "providerBankInfo", ResourceType = typeof (Resource))]
        private string providerBankInfo;
        public string ProviderBankInfo { get { return providerBankInfo ?? ""; } set { providerBankInfo = value; } }

        private string clientName;
        [Display(Name = "clientName", ResourceType = typeof (Resource))]
        [Required(ErrorMessageResourceName = "clientNameRequiredErrorMsg", ErrorMessageResourceType = typeof (Resource))]
        public string ClientName { get { return clientName ?? ""; } set { clientName = value; } }

        [Display(Name = "clientAddress", ResourceType = typeof (Resource))]
        private string clientAddress;
        public string ClientAddress { get { return clientAddress ?? ""; } set { clientAddress = value; } }

        [Display(Name = "clientNIP", ResourceType = typeof (Resource))]
        private string clientNIP;
        public string ClientNIP { get { return clientNIP ?? ""; } set { clientNIP = value; } }

        [Display(Name = "clientInfo", ResourceType = typeof (Resource))]
        private string clientInfo;
        public string ClientInfo { get { return clientInfo ?? ""; } set { clientInfo = value; } }

        private string creationPlace;
        [Display(Name = "creationPlace", ResourceType = typeof (Resource))]
        [Required(ErrorMessageResourceName = "creationPlaceRequiredErrorMsg",
                ErrorMessageResourceType = typeof (Resource))]
        public string CreationPlace { get { return creationPlace ?? ""; } set { creationPlace = value; } }

        [Display(Name = "sumWithTax", ResourceType = typeof(Resource))]
        public decimal SumWithTax { get; set; } // sum with tax

        private string sumString;
        [Display(Name = "inWords", ResourceType = typeof (Resource))]
        [Required(ErrorMessageResourceName = "inWordsRequiredErrorMsg", ErrorMessageResourceType = typeof (Resource))]
        public string SumString { get { return sumString ?? ""; } set { sumString = value; } } // sum in words

        [Display(Name = "sumWithoutTax", ResourceType = typeof(Resource))]
        public decimal SumWithoutTax { get; set; } // sum without tax

        [Display(Name = "sumOfTax", ResourceType = typeof(Resource))]
        public decimal SumOfTax { get; set; } // sum of tax

        [Display(Name = "paid", ResourceType = typeof(Resource))]
        public decimal Paid { get; set; } // allready paid cash

        [Display(Name = "remain", ResourceType = typeof(Resource))]
        public decimal Remain { get; set; } // Sum - Paid

        private string issuer;
        [Display(Name = "issuer", ResourceType = typeof (Resource))]
        [Required(ErrorMessageResourceName = "issuerRequiredErrorMsg", ErrorMessageResourceType = typeof (Resource))]
        public string Issuer { get { return issuer ?? ""; } set { issuer = value; } } // who is writing invoice

        private string reciver;
        [Display(Name = "reciver", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "reciverRequiredErrorMsg", ErrorMessageResourceType = typeof(Resource))]
        public string Reciver { get { return reciver ?? ""; } set { reciver = value; } } // who recives invoice

        public int[] ItemIds { get; set; }

        public List<FactureItemDto> Items { get; set; }

        public List<SelectListItem> PaymentList;

        [Display(Name = "payment", ResourceType = typeof(Resource))]
        public string Payment { get; set; }
    }
}