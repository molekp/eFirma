using System.Collections.Generic;
using System.Web.Security;
using BussinessLogic.DTOs;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.Helpers;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositoriesLogic
{
    [TestFixture]
    public class ItemManagementLogicTest
    {
        private IItemManagementLogic m_itemManagementLogic;


        [SetUp]
        public void Init()
        {
            m_itemManagementLogic = RepositoryLogicFactory.GetItemManagementLogic();
            m_itemManagementLogic.ItemRepository = MockRepository.GenerateStub<IItemRepository>();
        }
        
        [Test]
        public void GetAllItemsTest()
        {
            var list = new List<ProductItem>{
                new ProductItem(),
                new ProductItem(),
                new ProductItem(),
                new ProductItem()
            };
            m_itemManagementLogic.ItemRepository.Stub(x => x.GetAllProductItems()).IgnoreArguments().Return(list);
            //
            var result = m_itemManagementLogic.GetAllItemsForSearch();
            //
            Assert.AreEqual(4, result.Count, "znaleziono nie wszystkie itemy");
        }
    }
}