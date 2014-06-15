using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.FactureRepositories;
using Database.Core.Interfaces;
using Database.Entities.Customers;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Currencies;

namespace BussinessLogic.DatabaseLogic.Repositories.FactureRepositories
{
    class FactureRepository : IFactureRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<Facture> GetAllFactures()
        {
            try
            {
                return DataBaseContext.Factures.OrderBy(t => t.IdFacture);
            }
            catch (Exception)
            {
                return new List<Facture>();
            }
        }

        public Facture GetFacture(int a_idFacture)
        {
            return DataBaseContext.Factures.FirstOrDefault(t => t.IdFacture == a_idFacture);
        }
    
        public IEnumerable<Database.Entities.Customers.Customer> GetAllFirms()
        {
            try
            {
                return DataBaseContext.Customers.OfType<Firm>().OrderBy(t => t.IdCustomer);
            }
            catch (Exception)
            {
                return new List<Database.Entities.Customers.Customer>();
            }
        }

        public Database.Entities.Customers.Customer GetCustomer(int a_idFirm)
        {
            return DataBaseContext.Customers.OfType<Firm>().FirstOrDefault(t => t.IdCustomer == a_idFirm);
        }

        public bool SaveFirm(Firm a_mapDtoToEntity)
        {
            try
            {
                var firm = GetFirm(a_mapDtoToEntity.IdCustomer);
                DataBaseContext.SetModified(firm);
                firm.Name = a_mapDtoToEntity.Name;
                firm.Address = a_mapDtoToEntity.Address;
                firm.NIP = a_mapDtoToEntity.NIP;
                firm.Phone = a_mapDtoToEntity.Phone;
                firm.Info = a_mapDtoToEntity.Info;
                firm.BankAccountNumber = a_mapDtoToEntity.BankAccountNumber;
                firm.BankInfo = a_mapDtoToEntity.BankInfo;
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public int idFacture(int a_idDistribution)
        {
            int id = 0;
            try
            {
                id = GetDistribution(a_idDistribution).Facture.IdFacture;
            }
            catch (Exception)
            {
                return 0;
            }
            return id;
        }

        public bool RemoveFirm(Firm a_getFirm)
        {
            try
            {
                DataBaseContext.Customers.Remove(a_getFirm);
                DataBaseContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Distribution GetDistribution(int a_idDistribution)
        {
            return DataBaseContext.Distributions.FirstOrDefault(t => t.IdDistribution == a_idDistribution);
        }

        public FactureItem GetFactureItem(int a_idFactureItem)
        {
            return DataBaseContext.FactureItems.FirstOrDefault(t => t.IdItem == a_idFactureItem);
        }

        public CurrencyExchange getFactureExchange(int a_idFacture, int a_idCurrency)
        {
            return DataBaseContext.Factures.FirstOrDefault(t => t.IdFacture == a_idFacture).Exchanges.FirstOrDefault(e => e.Currency.CurrencyId == a_idCurrency);
        }

        public int AddFirm(Firm a_FirmEntity)
        {
            int idFirm = 0;
            try
            {
                var item = DataBaseContext.Customers.Add(a_FirmEntity);
                DataBaseContext.SaveChanges();
                idFirm = item.IdCustomer;
            }
            catch (Exception)
            {
                return 0;
            }
            return idFirm;
        }

        public Firm GetFirm(int a_idFirm)
        {
            return DataBaseContext.Customers.OfType<Firm>().FirstOrDefault(t => t.IdCustomer == a_idFirm);
        }

        public int SaveFactureItem(FactureItem factureItem)
        {
            int idFactureItem = 0;
            try
            {
                var item = DataBaseContext.FactureItems.Add(factureItem);
                DataBaseContext.SaveChanges();
                idFactureItem = item.IdItem;
            }
            catch (Exception)
            {
                return 0;
            }
            return idFactureItem;
        }

        public int SaveFacture(Facture a_factureEntity)
        {
            int idFacture = 0;
            try
            {
                var item = DataBaseContext.Factures.Add(a_factureEntity);
                DataBaseContext.SaveChanges();
                idFacture = item.IdFacture;
            }
            catch (Exception)
            {
                return 0;
            }
            return idFacture;
        }

        public bool SetDistributionFacture(int a_idDistribution, int a_idFacture)
        {
            try
            {
                var dist = GetDistribution(a_idDistribution);
                var facture = GetFacture(a_idFacture);
                DataBaseContext.SetModified(dist);
                dist.Facture = facture;
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}