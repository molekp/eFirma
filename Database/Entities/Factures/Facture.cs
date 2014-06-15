using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities.Customers;
using Database.Entities.WarehouseEntities.Currencies;

namespace Database.Entities.Factures
{
    public class Facture
    {
        [Key] 
        public int IdFacture { get; set; }

        public string ProviderName { get; set; }

        public string ProviderAddress { get; set; }

        public string ProviderNIP { get; set; }

        public string ProviderInfo { get; set; }

        public string ProviderBankAccountNumber { get; set; }

        public string ProviderBankInfo { get; set; }

        public string ClientName { get; set; }

        public string ClientAddress { get; set; }

        public string ClientNIP { get; set; }

        public string ClientInfo { get; set; }

        public DateTime CreationTime { get; set; } // when invoice was created

        public string CreationPlace { get; set; } // where invoice was created

        public DateTime RealizationTime { get; set; } // date of realization
         
        public DateTime ExpirationTime { get; set; } // time of payment

        public virtual List<FactureItem> Items { get; set; }
        
        public decimal? Sum { get; set; }  // sum with tax

        public string SumString { get; set; } // sum in words

        public decimal? Paid { get; set; }    // allready paid cash
        
        public string Issuer { get; set; }  // who is writing invoice

        public string Reciver { get; set; }  // who recives invoice

        public string Payment { get; set; }  // way of payment

        public virtual List<CurrencyExchange> Exchanges { get; set; }
    }
}