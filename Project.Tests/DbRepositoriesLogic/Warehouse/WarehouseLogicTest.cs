using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using NUnit.Framework;
using FluentAssertions;
using Rhino.Mocks;

namespace Project.Tests.DbRepositoriesLogic.Warehouse
{
    [TestFixture]
    public class WarehouseLogicTest
    {
        private IWarehouseLogic m_warehouseLogic;


        [SetUp]
        public void Init()
        {
            m_warehouseLogic = RepositoryLogicFactory.GetWarehouseLogic();
        }
        
        [Test]
        public void AddWarehouse__zwraca_prawde_jesli_utworzono()
        {
            m_warehouseLogic.WarehouseRepository = MockRepository.GenerateStub<IWarehouseRepository>();
            m_warehouseLogic.WarehouseRepository.Stub(x => x.AddWarehouse(null)).IgnoreArguments().Return(true);
            //
            var result = m_warehouseLogic.AddWarehouse(new AddWarehouseDto());
            //
            result.Should().BeTrue();
        }
    }
}