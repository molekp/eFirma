using System;
using System.Collections.Generic;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Distributions;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Customers;
using Database.Entities.Safety;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using NUnit.Framework;
using Project.Controllers;
using Project.Controllers.Warehouse.Distribution;
using Rhino.Mocks;
using FluentAssertions;
using System.Linq;

namespace Project.Tests.IntegrationTests
{
    [TestFixture]
    public class AdminTest
    {
        private AdminController m_controller;
        private IDataBaseContext m_dataBaseContext;
        [SetUp]
        public void Init()
        {
            m_dataBaseContext = MokujDb();
            RepositoryLogicFactory.DataBaseContext = m_dataBaseContext;
            m_controller = new AdminController();
        }

        [Test]
        public void TestGetAllSafetyPoints_dla_istniejacej_zabezpieczen__zwraca_liste()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetAllDisplaySafetyPoints();
            //assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(5);
        }

        [Test]
        public void TestDisplayDistribution_dla_nie_istniejacej_dystrybucji__zwraca_pusta_liste()
        {
            var points = m_dataBaseContext.SafetyPoints.ToList();
            points.ForEach(x => m_dataBaseContext.SafetyPoints.Remove(x));
            //act
            var result = m_controller.SafetyPointsLogic.GetAllDisplaySafetyPoints();
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }


        [Test]
        public void TestGetAllTypesOfSafetyPoints_istniejacych_typow__zwraca_liste()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetAllTypesOfSafetyPoints();
            //assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(4);
        }

        [Test]
        public void TestDisplayDistribution_dla_nie_istniejacych_typow__zwraca_pusta_liste()
        {
            var points = m_dataBaseContext.SafetyPoints.ToList();
            points.ForEach(x => m_dataBaseContext.SafetyPoints.Remove(x));
            //act
            var result = m_controller.SafetyPointsLogic.GetAllDisplaySafetyPoints();
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
        

        [Test]
        public void TestAddSafetyPoint_dla_poprawnego_obiektu__zwraca_liste()
        {
            var dto = new AddNewSafetyPointDto{IdTypeOfSafetyPoint = 1, IdOfPointInTable = 1, NameOfsafetyPoint = "test",Read = true, Write = true};
            //act
            var result = m_controller.SafetyPointsLogic.CreateSafetyPoint(dto);
            //assert
            result.Should().BeTrue();
        }

        [Test]
        public void TestAddSafetyPoint_dla_niepoprawnego_obiektu__zwraca_pusta_liste()
        {
            var dto = new AddNewSafetyPointDto();
            //act
            var result = m_controller.SafetyPointsLogic.CreateSafetyPoint(dto);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestAddSafetyPoint_bez_podanego_typu__zwraca_pusta_liste()
        {
            var dto = new AddNewSafetyPointDto { IdOfPointInTable = 1, NameOfsafetyPoint = "test", Read = true, Write = true };
            //act
            var result = m_controller.SafetyPointsLogic.CreateSafetyPoint(dto);
            //assert
            result.Should().BeFalse();
        }

        [Test]
        public void TestGetRecordsForType_dla_poprawnego_typu__zwraca_liste()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetRecordsForType(1);
            //assert
            result.Should().NotBeNull();
        }

        [Test]
        public void TestGetRecordsForType_dla_nie_istniejacego_typu__zwraca_pusta_liste()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetRecordsForType(5);
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestGetAllDisplaySafetyPointsGroups_dla_istniejacych_grup__zwraca_liste()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetAllDisplaySafetyPointsGroups();
            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Test]
        public void TestGetAllDisplaySafetyPointsGroups_dla_nie_istniejacych_grup__zwraca_pusta_liste()
        {
            m_dataBaseContext.SafetyPointGroups.Remove(m_dataBaseContext.SafetyPointGroups.First());
            //act
            var result = m_controller.SafetyPointsLogic.GetAllDisplaySafetyPointsGroups();
            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void TestAddSafetyPointGroup_dla_poprawnego_obiektu__zwraca_prawde()
        {
            var dto = new AddSafetyPointGroup {GroupName = "test"};
            //act
            var result = m_controller.SafetyPointsLogic.CreateSafetyPointGroup(dto);
            //assert
            m_dataBaseContext.SafetyPointGroups.Should().HaveCount(2);
        }

        [Test]
        public void TestAddSafetyPointGroup_dla_nie_poprawnego_obiektu__zwraca_falsz()
        {
            var dto = new AddSafetyPointGroup();
            //act
            var result = m_controller.SafetyPointsLogic.CreateSafetyPointGroup(dto);
            //assert
            result.Should().Be(0);
            m_dataBaseContext.SafetyPointGroups.Should().HaveCount(1);
        }

        [Test]
        public void TestRemoveSafetyPointGroup_dla_istniejacej_grupy__zwraca_prawde()
        {
            //act
            var result = m_controller.SafetyPointsLogic.RemoveSafetyPointGroup(1);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.SafetyPointGroups.Should().HaveCount(0);
        }

        [Test]
        public void TestRemoveSafetyPointGroup_dla_nie_isniejacej_grupy__zwraca_falsz()
        {
            //act
            var result = m_controller.SafetyPointsLogic.RemoveSafetyPointGroup(5);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.SafetyPointGroups.Should().HaveCount(1);
        }

        [Test]
        public void TestGetEditSafetyPointGroupDto_dla_istniejacej_grupy__zwraca_obiekt()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetEditSafetyPointGroupDto(1);
            //assert
            result.Should().NotBeNull();
            result.IdSafetyPointGroup.Should().Be(1);
        }

        [Test]
        public void TestGetEditSafetyPointGroupDto_dla_nie_isniejacej_grupy__zwraca_null()
        {
            //act
            var result = m_controller.SafetyPointsLogic.GetEditSafetyPointGroupDto(5);
            //assert
            result.Should().BeNull();
        }


        [Test]
        public void TestEditSafetyPointGroup_dla_istniejacej_grupy_zmiana_nazwy__zwraca_true()
        {
            //act
            var result = m_controller.SafetyPointsLogic.RenameSafetyPointGroup(1, "test");
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.SafetyPointGroups.First().GroupName.Should().BeEquivalentTo("test");
        }

        [Test]
        public void TestEditSafetyPointGroup_dla_nie_isniejacej_grupy_zmiana_nazwy__zwraca_falsz()
        {
            //act
            var result = m_controller.SafetyPointsLogic.RenameSafetyPointGroup(2, "test");
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.SafetyPointGroups.First().GroupName.Should().BeEquivalentTo("group");
        }

        [Test]
        public void TestAddSafetyPointToGroup_dla_istniejacej_grupy_dodanie_pointu__zwraca_true()
        {
            //act
            var result = m_controller.SafetyPointsLogic.AddSafetyPointToGroup(1,5);
            //assert
            result.Should().BeTrue();
            m_dataBaseContext.SafetyPointGroups.First().SafetyPoints.Should().HaveCount(5);
        }

        [Test]
        public void TestAddSafetyPointToGroup_dla_nie_isniejacej_grupy_dodanie_nieistniejacego_pointu__zwraca_falsz()
        {
            //act
            var result = m_controller.SafetyPointsLogic.AddSafetyPointToGroup(1, 12);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.SafetyPointGroups.First().SafetyPoints.Should().HaveCount(4);
        }

        [Test]
        public void TestAddSafetyPointToGroup_dla_nie_isniejacej_grupy_dodanie_ponowne_tego_samego_pointu__zwraca_falsz()
        {
            //act
            var result = m_controller.SafetyPointsLogic.AddSafetyPointToGroup(1, 1);
            //assert
            result.Should().BeFalse();
            m_dataBaseContext.SafetyPointGroups.First().SafetyPoints.Should().HaveCount(4);
        }


        private IDataBaseContext MokujDb()
        {
            var context = MockRepository.GenerateStub<IDataBaseContext>();
            context.SafetyPointGroups = new FakeDbSet<SafetyPointGroup>();
            context.SafetyPoints = new FakeDbSet<SafetyPoint>();
            context.TypesOfSafetyPoints = new FakeDbSet<TypeOfSafetyPoint>();

            var typesOfSafetyPoints = GetTypesOfSafetyPoints();
            typesOfSafetyPoints.ForEach(x=>context.TypesOfSafetyPoints.Add(x));
            var safetyPoints = GetSafetyPoints(typesOfSafetyPoints);
            safetyPoints.ForEach(x => context.SafetyPoints.Add(x));
            var sf = safetyPoints.Where(x => x.IdSafetyPoint < 5).ToList();
            var safetyPointGroup = GetSafetyPointGroup(sf);
            context.SafetyPointGroups.Add(safetyPointGroup);

            context.SaveChanges();
            return context;
        }

        private SafetyPointGroup GetSafetyPointGroup(List<SafetyPoint> a_safetyPoints)
        {
            return new SafetyPointGroup
                {
                        GroupName = "group",
                        IdSafetyPointGroup = 1,
                        SafetyPoints = a_safetyPoints
                };
        }

        private List<TypeOfSafetyPoint> GetTypesOfSafetyPoints()
        {
            return new List<TypeOfSafetyPoint>
                {
                        new TypeOfSafetyPoint{IdTypeOfSafetyPoint = 1, Name = "type1"},
                        new TypeOfSafetyPoint{IdTypeOfSafetyPoint = 2, Name = "type2"},
                        new TypeOfSafetyPoint{IdTypeOfSafetyPoint = 3, Name = "type3"},
                        new TypeOfSafetyPoint{IdTypeOfSafetyPoint = 4, Name = "type4"},
                };
        } 

        private List<SafetyPoint> GetSafetyPoints(List<TypeOfSafetyPoint> a_types )
        {
            return new List<SafetyPoint>
                {
                        new SafetyPoint{TypeOfSafetyPoint = a_types[0],IdOfPointInTable = 1,IdSafetyPoint = 1,NameOfsafetyPoint = "point1",Read = true,Write = true},
                        new SafetyPoint{TypeOfSafetyPoint = a_types[1],IdOfPointInTable = 1,IdSafetyPoint = 2,NameOfsafetyPoint = "point2",Read = true,Write = false},
                        new SafetyPoint{TypeOfSafetyPoint = a_types[2],IdOfPointInTable = 1,IdSafetyPoint = 3,NameOfsafetyPoint = "point3",Read = false,Write = true},
                        new SafetyPoint{TypeOfSafetyPoint = a_types[3],IdOfPointInTable = 1,IdSafetyPoint = 4,NameOfsafetyPoint = "point4",Read = false,Write = false},
                        new SafetyPoint{TypeOfSafetyPoint = a_types[3],IdOfPointInTable = 1,IdSafetyPoint = 5,NameOfsafetyPoint = "point5",Read = false,Write = false}
                };
        }
    }
}
