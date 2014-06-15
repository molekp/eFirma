using System;
using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class FactureViewDto
    {
        [Display(Name = "idFacture", ResourceType = typeof(Resource))]
        public int IdFacture { get; set; }

        [Display(Name = "factureNr", ResourceType = typeof(Resource))]
        public string FactureNr { get; set; }

        [Display(Name = "providerName", ResourceType = typeof(Resource))]
        public string ProviderName { get; set; }
        
        [Display(Name = "clientName", ResourceType = typeof(Resource))]
        public string ClientName { get; set; }

        [Display(Name = "creationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime CreationTime { get; set; }
        
        [Display(Name = "realizationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime RealizationTime { get; set; }

        [Display(Name = "expirationTime", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime ExpirationTime { get; set; }
        
    }
}