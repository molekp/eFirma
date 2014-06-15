using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Admin;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin;
using BussinessLogic.Mappers.Safety;
using Database.Entities.Safety;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositoriesLogic.Admin
{
    [TestFixture]
    public class SafetyPointsLogicTest
    {
        private ISafetyPointsLogic m_safetyPointsLogic;

        [SetUp]
        public void Init()
        {
            m_safetyPointsLogic = RepositoryLogicFactory.GetSafetyPointLogic();
            m_safetyPointsLogic.TypesOfSafetyPointsRepository = Mokuj_TypesOfSafetyPointsRepository();
            m_safetyPointsLogic.WarehouseRepository = Mokuj_WarehouseRepository_i_zastap_metode_GetAll();
            m_safetyPointsLogic.SafetyPointRepository = Mokuj_SafetyPointRepository_i_zastap_metode_GetAll();
        }


        [Test]
        public void GetAllTypesOfSafetyPoints__zwraca_selectList_z_wszystkimi_typami_safety_points()//TODO lepsze sprawdzanie selectlisty
        {
            //
            var result =m_safetyPointsLogic.GetAllTypesOfSafetyPoints();
            //
            Assert.AreEqual(3,result.Count());
        }

        [Test]
        public void GetRecordsForType_dla_istniejacego_typu__zwraca_selectList_z_wszystkimi_rekordami_danego_typu()//TODO lepsze sprawdzanie selectlisty
        {
            //
            var result = m_safetyPointsLogic.GetAllDisplaySafetyPoints();
            //
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAllDisplaySafetyPoints__zwraca_wszystkie_SafetyPoints_przerobione_na_DisplaySafetyPointDto()//TODO lepsze sprawdzanie selectlisty
        {
            //
            var result = m_safetyPointsLogic.GetRecordsForType(1);
            //
            result.Should().NotBeNull("wynik jest nullem");
            result.Count.Should().Be(SafetyPointList().Count());
        }

   

        [Test]
        public void CreateSafetyPointGroup_dla_poprawnej_grupy__zwraca_id_wstawionego_elementu()
        {
            var addSafetyPointGroup = new AddSafetyPointGroup {GroupName = "nowa"};
            Mokuj_SafetyPointGroupsRepository_i_podstaw_CreateSafetyPointGroup_zwracajac(1);
            //
            int result = m_safetyPointsLogic.CreateSafetyPointGroup(addSafetyPointGroup);
            //
            result.Should().Be(1);
        }

        [Test]
        public void CreateSafetyPointGroup_dla_niepoprawnej_grupy__zwraca_0()
        {
            var addSafetyPointGroup = new AddSafetyPointGroup { GroupName = "nowa" };
            Mokuj_SafetyPointGroupsRepository_i_podstaw_CreateSafetyPointGroup_zwracajac(0);
            //
            var result = m_safetyPointsLogic.CreateSafetyPointGroup(addSafetyPointGroup);
            //
            result.Should().Be(0);
        }


        [Test]
        public void GetAllDisplaySafetyPointsGroups__zwraca_liste_DisplaySafetyPointGroups()
        {
            Mokuj_SafetyPointGroupsRepository_i_podstaw_GetAll();
            //
            var result = m_safetyPointsLogic.GetAllDisplaySafetyPointsGroups();
            //
            result.Should().NotBeNull("wynik jest nullem");
            result.Count().Should().Be(SafetyPointGroupList().Count());
        }

        [Test]
        public void RemoveSafetyPointGroup__zwraca_prawde_jesli_usunieto_grupe()
        {
            Mokuj_SafetyPointGroupsRepository_i_podstaw_GetAll();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RemoveSafetyPointGroup(1))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RemoveSafetyPointGroup(1);
            //
            result.Should().BeTrue();
        }

        [Test]
        public void GetEditSafetyPointGroupDto__zwraca_poprawny_obiekt_dto()
        {
            m_safetyPointsLogic = Mockuj_SafetyPoinsLogic_z_mapperami();
            m_safetyPointsLogic.Stub(x => x.NameForRecordInTable(SafetyPointList()[0])).IgnoreArguments().Return("nowy rekord");
            m_safetyPointsLogic.SafetyPointGroupsRepository = Mokuj_SafetyPointGroupsRepository_i_zastap_metode_Get();
            m_safetyPointsLogic.SafetyPointRepository = Mokuj_SafetyPointRepository_i_zastap_metode_GetAll();
            //
            var result = m_safetyPointsLogic.GetEditSafetyPointGroupDto(1);
            //
            result.Should().NotBeNull("obiekt jest nullem");
            result.SafetyPoints.Count().Should().Be(SafetyPointList().Count);
            result.ChoicesToAddSafetyPointToGroup.Count().Should().Be(SafetyPointGroupList().Count+1);//bo pierwszy item to --wybierz--
            result.IdNewAddSafetyPoint.Should().Be(0);
            result.ChoicesToAddSafetyPointToGroup.First().Value.Should().Be("0");
        }

        [Test]
        public void AddSafetyPointToGroup_dla_istniejacego_safetyPointa__zwraca_prawde()
        {

            Mokuj_pod_metode_AddSafetyPointToGroup_zwracajac_odpowiednio(SafetyPointGroupList()[0], SafetyPointList()[0], true);
            //
            var result = m_safetyPointsLogic.AddSafetyPointToGroup(1,1);
            //
            result.Should().BeTrue();
        }

        [Test]
        public void AddSafetyPointToGroup_dla_nie_istniejacego_safetyPointa__zwraca_falsz()
        {
            Mokuj_pod_metode_AddSafetyPointToGroup_zwracajac_odpowiednio(SafetyPointGroupList()[0],null,true);
           //
            var result = m_safetyPointsLogic.AddSafetyPointToGroup(1, 1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void AddSafetyPointToGroup_dla_istniejacej_grupy__zwraca_prawde()
        {

            Mokuj_pod_metode_AddSafetyPointToGroup_zwracajac_odpowiednio(SafetyPointGroupList()[0], SafetyPointList()[0], true);
            //
            var result = m_safetyPointsLogic.AddSafetyPointToGroup(1, 1);
            //
            result.Should().BeTrue();
        }

        [Test]
        public void AddSafetyPointToGroup_dla_nie_istniejacej_grupy__zwraca_falsz()
        {
            Mokuj_pod_metode_AddSafetyPointToGroup_zwracajac_odpowiednio(null, SafetyPointList()[0], true);
            //
            var result = m_safetyPointsLogic.AddSafetyPointToGroup(1, 1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RenameSafetyPointGroup_dla_istniejacej_grupy__zwraca_prawde()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository = Mokuj_SafetyPointGroupsRepository_i_zastap_metode_Get();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RenameSafetyPointGroup(null, ""))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RenameSafetyPointGroup(1, "blabla");
            //
            result.Should().BeTrue();
        }

        [Test]
        public void RenameSafetyPointGroup_dla_nie_istniejacej_grupy__zwraca_falsz()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository =
                    MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(null);
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RenameSafetyPointGroup(null, ""))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RenameSafetyPointGroup(1, "blabla");
            //
            result.Should().BeFalse();
        }


        [Test]
        public void RemoveSafetyPointFromGroup_dla_istniejacej_grupy_i_safetyPointa__zwraca_prawde()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository = Mokuj_SafetyPointGroupsRepository_i_zastap_metode_Get();
            m_safetyPointsLogic.SafetyPointRepository.Stub(x => x.Get(1))
                               .IgnoreArguments()
                               .Return(SafetyPointList()[ 0 ]);

            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RemoveSafetyPointFromGroup(null, null))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RemoveSafetyPointFromGroup(new RemoveSafetyPointFromGroup());
            //
            result.Should().BeTrue();
        }

        [Test]
        public void RemoveSafetyPointFromGroup_dla_nie_istniejacej_grupy_i_safetyPointa__zwraca_falsz()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository =
                    MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(null);
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RenameSafetyPointGroup(null, ""))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RemoveSafetyPointFromGroup(new RemoveSafetyPointFromGroup());
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RemoveSafetyPointFromGroup_dla_istniejacej_grupy_i_nieistniejacego_safetyPointa__zwraca_falsz()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository = Mokuj_SafetyPointGroupsRepository_i_zastap_metode_Get();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RenameSafetyPointGroup(null, ""))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RemoveSafetyPointFromGroup(new RemoveSafetyPointFromGroup());
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RemoveSafetyPointFromGroup_dla_nie_istniejacej_grupy_i_istniejacego_safetyPointa__zwraca_falsz()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository =
                    MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(null);
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RenameSafetyPointGroup(null, ""))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RemoveSafetyPointFromGroup(new RemoveSafetyPointFromGroup());
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RemoveSafetyPointFromGroup_dla_grupy_i_safetyPointa_nie_nalezacego_do_grupy__zwraca_falsz()
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository =
                    MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(null);
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.RenameSafetyPointGroup(null, ""))
                               .IgnoreArguments()
                               .Return(true);
            //
            var result = m_safetyPointsLogic.RemoveSafetyPointFromGroup(new RemoveSafetyPointFromGroup());
            //
            result.Should().BeFalse();
        }



       
        private ISafetyPointsLogic Mockuj_SafetyPoinsLogic_z_mapperami()
        {
            var tmp = MockRepository.GeneratePartialMock<SafetyPointsLogic>();
            tmp.DispalySafetyPointDtoMapper = new DispalySafetyPointDtoMapper();
            tmp.EditSafetyPointGroupDtoMapper = new EditSafetyPointGroupDtoMapper();
            return tmp;
        }

        private void Mokuj_pod_metode_AddSafetyPointToGroup_zwracajac_odpowiednio(SafetyPointGroup a_safetyPointGroup,
                                                                                  SafetyPoint a_safetyPoint,bool a_returns)
        {
            m_safetyPointsLogic.SafetyPointGroupsRepository = MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.Get(1))
                               .IgnoreArguments()
                               .Return(a_safetyPointGroup);
            m_safetyPointsLogic.SafetyPointRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(a_safetyPoint);
            m_safetyPointsLogic.SafetyPointGroupsRepository.Stub(x => x.AddSafetyPointToGroup(null, null))
                               .IgnoreArguments()
                               .Return(a_returns);
        }

        private ITypesOfSafetyPointsRepository Mokuj_TypesOfSafetyPointsRepository()
        {
            var typesOfSafetyPointsRepository = MockRepository.GenerateStub<ITypesOfSafetyPointsRepository>();
            typesOfSafetyPointsRepository.Stub(x => x.GetAll()).IgnoreArguments().Return(TypeOfSafetyPointList());
            typesOfSafetyPointsRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(TypeOfSafetyPointList()[0]);
            return typesOfSafetyPointsRepository;
        }


        private IWarehouseRepository Mokuj_WarehouseRepository_i_zastap_metode_GetAll()
        {
            var warehouseRepository = MockRepository.GenerateStub<IWarehouseRepository>();
            warehouseRepository.Stub(x => x.GetAll()).IgnoreArguments().Return(WarehousesList());
            warehouseRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(new Database.Entities.WarehouseEntities.Warehouse{IdWarehouse = 1,Name = "name"});
            return warehouseRepository;
        }


        private ISafetyPointRepository Mokuj_SafetyPointRepository_i_zastap_metode_GetAll()
        {
            var safetyPointRepository = MockRepository.GenerateStub<ISafetyPointRepository>();
            safetyPointRepository.Stub(x => x.GetAll()).Return(SafetyPointList());
            return safetyPointRepository;
        }

        private ISafetyPointGroupsRepository Mokuj_SafetyPointGroupsRepository_i_zastap_metode_Get()
        {
            var safetyPointGroupsRepository = MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            safetyPointGroupsRepository.Stub(x => x.Get(1)).IgnoreArguments().Return(SafetyPointGroupList()[0]);
            return safetyPointGroupsRepository;
        }

        private void Mokuj_SafetyPointGroupsRepository_i_podstaw_CreateSafetyPointGroup_zwracajac(int a_returns)
        { 
            var groupsRepository = MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            groupsRepository.Stub(x => x.CreateSafetyPointGroup(null)).IgnoreArguments().Return(a_returns);
            groupsRepository.Stub(x => x.GetAll()).Return(SafetyPointGroupList());
            m_safetyPointsLogic.SafetyPointGroupsRepository = groupsRepository;
        }

        private void Mokuj_SafetyPointGroupsRepository_i_podstaw_GetAll()
        {
            var groupsRepository = MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            groupsRepository.Stub(x => x.GetAll()).Return(SafetyPointGroupList());
            m_safetyPointsLogic.SafetyPointGroupsRepository = groupsRepository;
        }

        private List<SafetyPointGroup> SafetyPointGroupList()
        {
            return new List<SafetyPointGroup>
                {
                        new SafetyPointGroup{GroupName = "nowa",IdSafetyPointGroup = 1,SafetyPoints = SafetyPointList()},
                        new SafetyPointGroup{GroupName = "nowa1",IdSafetyPointGroup = 2,SafetyPoints = new List<SafetyPoint>()}
                };
        } 

        private List<TypeOfSafetyPoint> TypeOfSafetyPointList()
        {
            return new List<TypeOfSafetyPoint> {
                new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 1,Name = "Warehouse"},
                new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 2,Name = "ServicesWarehouse"},
                new TypeOfSafetyPoint { IdTypeOfSafetyPoint = 3,Name = "Products"}
            };
        }

        private IEnumerable<Database.Entities.WarehouseEntities.Warehouse> WarehousesList()
        {
            return new List<Database.Entities.WarehouseEntities.Warehouse>
                {
                        new Database.Entities.WarehouseEntities.Warehouse { IdWarehouse =1, Name = "product warehouse 1"},
                        new Database.Entities.WarehouseEntities.Warehouse { IdWarehouse = 2, Name = "product warehouse 2"}
                };
        } 

        private List<SafetyPoint> SafetyPointList()
        {
            var typesOfSafetyPoint = TypeOfSafetyPointList();
            return new List<SafetyPoint>()
                {
                        new SafetyPoint{ IdSafetyPoint = 1,IdOfPointInTable = 1,TypeOfSafetyPoint =typesOfSafetyPoint[0],NameOfsafetyPoint = "first",Read = true,Write = true},
                        new SafetyPoint{ IdSafetyPoint = 2,IdOfPointInTable = 2,TypeOfSafetyPoint =typesOfSafetyPoint[1],NameOfsafetyPoint = "second",Read = true,Write = true}
                };
        }
    }
}