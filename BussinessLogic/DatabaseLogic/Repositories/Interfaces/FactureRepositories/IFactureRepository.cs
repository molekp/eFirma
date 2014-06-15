using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.Customers;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Currencies;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.FactureRepositories
{
    
    public interface IFactureRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        IEnumerable<Facture> GetAllFactures();
        Facture GetFacture(int a_idFacture);
        IEnumerable<Database.Entities.Customers.Customer> GetAllFirms();
        Firm GetFirm(int a_idFirm);
        Distribution GetDistribution(int a_idDistribution);
        int SaveFacture(Facture a_factureEntity);
        bool SetDistributionFacture(int a_idDistribution, int a_idFacture);
        int SaveFactureItem(FactureItem a_mapDtoToEntity);
        FactureItem GetFactureItem(int a_factureItemId);
        CurrencyExchange getFactureExchange(int a_idFacture, int a_idCurrency);
        int AddFirm(Firm a_mapDtoToEntity);
        Database.Entities.Customers.Customer GetCustomer(int a_idFirm);
        bool SaveFirm(Firm a_mapDtoToEntity);
        int idFacture(int a_idDistribution);
        bool RemoveFirm(Firm a_getFirm);
    }
}