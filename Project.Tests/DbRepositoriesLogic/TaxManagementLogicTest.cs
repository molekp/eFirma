using System.Collections.Generic;
using System.Web.Security;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Taxes;
using BussinessLogic.Helpers;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositoriesLogic
{
    [TestFixture]
    public class TaxManagementLogicTest
    {
        private ITaxManagementLogic m_taxManagementLogic;


        [SetUp]
        public void Init()
        {
            m_taxManagementLogic = RepositoryLogicFactory.GetTaxManagementLogic();
            m_taxManagementLogic.TaxRepository = MockRepository.GenerateStub<ITaxRepository>();
        }
        
        [Test]
        public void GetAllTaxesTest()
        {
            var list = new List<TaxEntity>{
                new TaxEntity(),
                new TaxEntity(),
                new TaxEntity()
            };
            m_taxManagementLogic.TaxRepository.Stub(x => x.GetAllTaxes()).IgnoreArguments().Return(list);
            //
            var result = m_taxManagementLogic.GetAllTaxes();
            //
            Assert.AreEqual(3, result.Count, "znaleziono nie wszystkie taxes");
        }

        [Test]
        public void AddTaxTest()
        {
            var taxAddDto = new TaxAddDto
            {
                IdTax = 1,
                TaxName = "A",
                TaxValue = 0.23
            };

            m_taxManagementLogic.TaxRepository.Stub(x => x.AddTax(null)).IgnoreArguments().Return(true);
            //
            m_taxManagementLogic.AddTax(taxAddDto);
            //
            m_taxManagementLogic.TaxRepository.AssertWasCalled(x => x.AddTax(null), x => x.IgnoreArguments());
        }

        [Test]
        public void SaveTaxTest()
        {
            var taxEditDto = new TaxEditDto
            {
                IdTax = 1,
                TaxName = "A",
                TaxValue = 0.23
            };

            m_taxManagementLogic.TaxRepository.Stub(x => x.SaveTax(null)).IgnoreArguments().Return(true);
            //
            m_taxManagementLogic.SaveTax(taxEditDto);
            //
            m_taxManagementLogic.TaxRepository.AssertWasCalled(x => x.SaveTax(null), x => x.IgnoreArguments());
        }

        [Test]
        public void RemoveTaxTest()
        {
            m_taxManagementLogic.TaxRepository.Stub(x => x.RemoveTax(null)).IgnoreArguments().Return(true);
            //
            m_taxManagementLogic.RemoveTax(1);
            //
            m_taxManagementLogic.TaxRepository.AssertWasCalled(x => x.RemoveTax(null), x => x.IgnoreArguments());
        }
    }
}