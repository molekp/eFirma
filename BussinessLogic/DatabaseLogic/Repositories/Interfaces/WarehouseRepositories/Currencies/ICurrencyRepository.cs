using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Currencies;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.Currencies
{
    public interface ICurrencyRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        IEnumerable<CurrencyExchange> GetAll();

        CurrencyExchange GetLatest(int a_currencyId);

        IEnumerable<CurrencyExchange> GetLatestList();

        IEnumerable<Currency> GetAllCurrencies();

        Currency GetCurrency(int a_currencyId);
        
    }
}
