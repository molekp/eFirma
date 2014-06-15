using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.TaxRepositories;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Project.Tests.DbRepositories
{
    [TestFixture]
    public class TaxRepositoryTest 
    {
        private ITaxRepository m_taxRepository;

        [SetUp]
        public void Init()
        {
            m_taxRepository = new TaxRepository
            {
                DataBaseContext = MokujDataBaseContextIWprowadzTaxes()
            };
        }

        [Test]
        public void GetAllTaxesTest()
        {
            //
            var result = m_taxRepository.GetAllTaxes();
            //
            Assert.IsNotNull(result, "nie znaleziono itemów");
            Assert.AreEqual(4, result.Count(), "znaleziono nie wszystkie itemy");
        }

        [Test]
        public void GetTaxTest()
        {
            //
            var result = m_taxRepository.GetTax(1);
            //
            Assert.IsNotNull(result, "nie znaleziono tax");
            Assert.AreEqual(1, result.IdTax, "zly tax id");
            Assert.AreEqual("A", result.TaxName, "zly tax name");
            Assert.AreEqual(0.23, result.TaxValue, "zly tax value");
        }

        [Test]
        public void SaveTaxTest()
        {
            //
            m_taxRepository.SaveTax(new TaxEntity{ IdTax = 1, TaxName = "a",TaxValue = 0.16});
            var result = m_taxRepository.DataBaseContext.Taxes.First(x => x.IdTax == 1);
            //
            Assert.IsNotNull(result, "nie znaleziono tax");
            Assert.AreEqual(1, result.IdTax, "zly tax id");
            Assert.AreEqual("a", result.TaxName, "zly tax name");
            Assert.AreEqual(0.16, result.TaxValue, "zly tax value");
        }

        [Test]
        public void RemoveTaxTest()
        {
            //
           var success = m_taxRepository.RemoveTax(new TaxEntity { IdTax = 2, TaxName = "B", TaxValue = 0.08 });
            var result = m_taxRepository.DataBaseContext.Taxes.ToList();
            //
            Assert.AreEqual(true, success, "blad zapisu tax do bazy danych");
            Assert.IsNotNull(result, "nie znaleziono itemów");
            Assert.AreEqual(4, result.Count(), "znaleziono nie wszystkie itemy");
        }

        [Test]
        public void TaxAdd()
        {
            var taxAddEntity = new TaxEntity { IdTax = 1, TaxName = "A", TaxValue = 0.23 };
            //act
            m_taxRepository.AddTax(taxAddEntity);
            TaxEntity result = m_taxRepository.DataBaseContext.Taxes.First(x => x.IdTax == 1);
            //assert
            Assert.IsNotNull(result, "nie znaleziono tax");
            Assert.AreEqual("A", result.TaxName, "Zły tax name");
            Assert.AreEqual(0.23, result.TaxValue, "Zły tax value");
        }

        private IDataBaseContext MokujDataBaseContextIWprowadzTaxes()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            dataBaseContext.Taxes = new FakeDbSet<TaxEntity>();

            foreach (TaxEntity taxEntity in TaxesList())
                dataBaseContext.Taxes.Add(taxEntity);
            return dataBaseContext;
        }

        private IEnumerable<TaxEntity> TaxesList()
        {
            return new List<TaxEntity>{
                new TaxEntity{ IdTax = 1, TaxName = "A",TaxValue = 0.23},
                new TaxEntity{ IdTax = 2, TaxName = "B", TaxValue = 0.08},
                new TaxEntity{ IdTax = 3, TaxName = "C",TaxValue = 0.4},
                new TaxEntity{ IdTax = 4, TaxName = "D", TaxValue = 0.0}
            };
        }

    }
}