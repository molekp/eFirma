using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories;
using Database.Core;
using Database.Core.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Project.Tests.DbRepositories.Warehouse
{
    [TestFixture]
    public class WarehouseRepositoryTest
    {
        private IWarehouseRepository m_warehouseRepository;

        [SetUp]
        public void Init()
        {
            m_warehouseRepository = new WarehouseRepository()
            {
                DataBaseContext = MockRepository.GenerateStub<IDataBaseContext>()
            };
            m_warehouseRepository.DataBaseContext.Warehouses = new FakeDbSet<Database.Entities.WarehouseEntities.Warehouse>();
        }

        [Test]
        public void AddWarehouse__zwraca_prawde_jesli_dodano()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_warehouseRepository.AddWarehouse(new Database.Entities.WarehouseEntities.Warehouse());
            //
            result.Should().BeTrue();
        }
    }
}
