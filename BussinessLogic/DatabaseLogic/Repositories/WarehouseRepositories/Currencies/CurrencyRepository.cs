using System;
using System.Collections.Generic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.Currencies;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Currencies;
using System.Linq;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.Currencies
{
    public class CurrencyRepository : ICurrencyRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<CurrencyExchange> GetAll()
        {
            return (from currency in DataBaseContext.Currencies.Distinct().ToList()
                    join ce in
                            (from ce in
                                     DataBaseContext.CurrencyExchanges.Where(x => x.ExchangeCourseDate < DateTime.Now).OrderByDescending(x=>x.ExchangeCourseDate).ToList()
                             select ce) on currency.CurrencyId equals ce.Currency.CurrencyId
                    select ce);
        }

        public CurrencyExchange GetLatest(int a_currencyId)
        {
            return (from currency in DataBaseContext.Currencies.Distinct().ToList()
                    join ce in
                        (from ce in
                             DataBaseContext.CurrencyExchanges.Where(x => x.ExchangeCourseDate < DateTime.Now).OrderByDescending(x => x.ExchangeCourseDate).ToList()
                         select ce) on currency.CurrencyId equals ce.Currency.CurrencyId
                    where currency.CurrencyId == a_currencyId
                    select ce).FirstOrDefault();
        }

        public IEnumerable<CurrencyExchange> GetLatestList()
        {
            return (from currency in DataBaseContext.Currencies.Distinct().ToList()
                    join ce in
                        (from ce in
                             DataBaseContext.CurrencyExchanges.Where(x => x.ExchangeCourseDate < DateTime.Now).OrderByDescending(x => x.ExchangeCourseDate).ToList()
                         select ce) on currency.CurrencyId equals ce.Currency.CurrencyId
                    select ce);
        }

        public IEnumerable<Currency> GetAllCurrencies()
        {
            try
            {
                return DataBaseContext.Currencies.OrderBy(t => t.CurrencyId);
            }
            catch (Exception)
            {
                return new List<Currency>();
            }
        }

        public Currency GetCurrency(int a_currencyId)
        {
            return DataBaseContext.Currencies.FirstOrDefault(t => t.CurrencyId == a_currencyId);
        }
    }
}
