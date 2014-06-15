using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Management;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Safety;
using Database.Core.Interfaces;
using Database.Entities.Safety;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositories.Safety
{
    [TestFixture]
    public class SafetyPointGroupsRepositoryTest
    {
        private ISafetyPointGroupsRepository m_safetyPointGroupsRepository;

        [SetUp]
        public void Init()
        {
            m_safetyPointGroupsRepository = new SafetyPointGroupsRepository()
                {
                    DataBaseContext = zmokuj_DataBaseContext_ze_sztucznymi_SafetyPointGroups()
                };
        }

        [Test]
        public void CreateSafetyPointGroup_dla_nulla__zwraca_0()
        {
            m_safetyPointGroupsRepository.DataBaseContext.Stub(x => x.SaveChanges()).Throw(new SqlExecutionException());
            //
            int result = m_safetyPointGroupsRepository.CreateSafetyPointGroup(null);
            //
            result.Should().Be(0);
        }

        [Test]
        public void CreateSafetyPointGroup_dla_niepoprawnej_grupy__zwraca_0()
        {
            var safetyPointGroup = new SafetyPointGroup();
            m_safetyPointGroupsRepository.DataBaseContext.Stub(x => x.SaveChanges()).Throw(new SqlExecutionException());
            //
            int result = m_safetyPointGroupsRepository.CreateSafetyPointGroup(safetyPointGroup);
            //
            result.Should().Be(0);
        }

        [Test]
        public void CreateSafetyPointGroup_dla_poprawnej_grupy__zwraca_id_wstawionego_elementu()
        {
            var safetyPointGroup = new SafetyPointGroup{IdSafetyPointGroup = 1,GroupName = "nowa"};
            m_safetyPointGroupsRepository.DataBaseContext.Stub(x => x.SaveChanges());
            //
            int result = m_safetyPointGroupsRepository.CreateSafetyPointGroup(safetyPointGroup);
            //
            result.Should().BeGreaterThan(0);
        }

        [Test]
        public void GetAll__zwraca_wszystkie_SafetyPointGroups()
        {
            //
            IEnumerable<SafetyPointGroup> result = m_safetyPointGroupsRepository.GetAll();
            //
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Test]
        public void RemoveSafetyPointGroup_dla_istniejacej_grupy__zwraca_prawde()
        {
            //
            var result = m_safetyPointGroupsRepository.RemoveSafetyPointGroup(1);
            //
            result.Should().BeTrue();
        }

        [Test]
        public void RemoveSafetyPointGroup_dla_nie_istniejacej_grupy__zwraca_falsz()
        {
            //
            var result = m_safetyPointGroupsRepository.RemoveSafetyPointGroup(-1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RemoveSafetyPointGroup_dla_istniejacej_grupy__zwraca_falsz_jesli_nie_mozna_usunac_grupy()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            m_safetyPointGroupsRepository.DataBaseContext.Stub(x => x.SaveChanges()).Throw(new Exception());
            //act
            var result = m_safetyPointGroupsRepository.RemoveSafetyPointGroup(1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void Get_dla_istniejacej_grupy__zwraca_entit_grupy()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_safetyPointGroupsRepository.Get(1);
            //
            result.Should().NotBeNull();
            result.ShouldHave().AllProperties().But(x=>x.SafetyPoints).EqualTo(ListSafetyPointGroups()[0]);
        }

        [Test]
        public void Get_dla_nie_istniejacej_grupy__zwraca_null()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_safetyPointGroupsRepository.Get(-1);
            //
            result.Should().BeNull();
        }

        [Test]
        public void AddSafetyPointToGroup_dla_istniejacj_grupy__zwraca_prawde()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_safetyPointGroupsRepository.AddSafetyPointToGroup(ListSafetyPointGroups()[0],new SafetyPoint());
            //
            result.Should().BeTrue();
        }

        [Test]
        public void AddSafetyPointToGroup_dla_nie_istniejacj_grupy__zwraca_falsz()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_safetyPointGroupsRepository.AddSafetyPointToGroup(new SafetyPointGroup(), new SafetyPoint());
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RenameSafetyPointGroup__zwraca_prawde_jesli_poprawnie_zapisano_dane()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_safetyPointGroupsRepository.RenameSafetyPointGroup(new SafetyPointGroup(), "bla");
            //
            result.Should().BeTrue();
        }

        [Test]
        public void RenameSafetyPointGroup__zwraca_falsz_jesli_nie_zapisano_danych()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            m_safetyPointGroupsRepository.DataBaseContext.Stub(x => x.SaveChanges()).Throw(new Exception());
            //act
            var result = m_safetyPointGroupsRepository.RenameSafetyPointGroup(new SafetyPointGroup(), "bla");
            //
            result.Should().BeFalse();
        }

        [Test]
        public void RemoveSafetyPointFromGroup__zwraca_prawde_jesli_poprawnie_zapisano_dane()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            //act
            var result = m_safetyPointGroupsRepository.RemoveSafetyPointFromGroup(ListSafetyPointGroups()[0], SafetyPointList()[0]);
            //
            result.Should().BeTrue();
        }

        [Test]
        public void RemoveSafetyPointFromGroup__zwraca_falsz_jesli_nie_zapisano_danych()//jesli np uzytkownik jest przydzielony do tej grupy
        {
            m_safetyPointGroupsRepository.DataBaseContext.Stub(x => x.SaveChanges()).Throw(new Exception());
            //act
            var result = m_safetyPointGroupsRepository.RemoveSafetyPointFromGroup(ListSafetyPointGroups()[0], SafetyPointList()[0]);
            //
            result.Should().BeFalse();
        }
       
        private List<SafetyPointGroup> ListSafetyPointGroups()
        {
            return new List<SafetyPointGroup>()
                {
                        new SafetyPointGroup{IdSafetyPointGroup = 1,GroupName = "nowa",SafetyPoints = SafetyPointList()},
                        new SafetyPointGroup{IdSafetyPointGroup = 2,GroupName = "nowa2",SafetyPoints = new List<SafetyPoint>()}
                };
        }

        private List<SafetyPoint> SafetyPointList()
        {
            return new List<SafetyPoint>()
                {
                        new SafetyPoint{ IdSafetyPoint = 1,IdOfPointInTable = 1,TypeOfSafetyPoint =new TypeOfSafetyPoint(),NameOfsafetyPoint = "first",Read = true,Write = true},
                        new SafetyPoint{ IdSafetyPoint = 2,IdOfPointInTable = 2,TypeOfSafetyPoint =new TypeOfSafetyPoint(),NameOfsafetyPoint = "second",Read = true,Write = true}
                };
        }


        private IDataBaseContext zmokuj_DataBaseContext_ze_sztucznymi_SafetyPointGroups()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            dataBaseContext.SafetyPointGroups = new FakeDbSet<SafetyPointGroup>();
            foreach (var safetyPointGroup in ListSafetyPointGroups())
            {
                dataBaseContext.SafetyPointGroups.Add(safetyPointGroup);
            }
            return dataBaseContext;
        }

    }
}