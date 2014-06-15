using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.Helpers;
using Database.Core.Interfaces;
using Database.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Project.Tests.DbRepositories
{
    [TestFixture]
    public class RoleDataBaseConnectorTest
    {
        private IRoleDataBaseRepository m_roleDataBaseRepository;

        [SetUp]
        public void Init()
        {
            m_roleDataBaseRepository = new RoleDataBaseRepository
                {
                        DataBaseContext = zmokuj_DataBaseContext_ze_sztucznymi_rolami()
                };
        }

        [Test]
        public void GetAllRoleNamesList_Test()
        {
            //
            IEnumerable<string> result = m_roleDataBaseRepository.GetAllRoleNamesList();
            //
            Assert.IsNotNull(result, "nie znaleziono ról");
            Assert.AreEqual(result.Count(), RoleEntityList().Count, "nie znaleziono wszystkich ról");
        }

        [Test]
        public void GetRoleForRoleName_Test()
        {
            //
            RoleEntity result = m_roleDataBaseRepository.GetRoleForRoleName(ConstantsHelper.ADMIN_ROLE);
            //
            Assert.IsNotNull(result, "nie znaleziono roli");
            Assert.AreEqual(result.NameRole, ConstantsHelper.ADMIN_ROLE, "znaleziono zła rolę");
        }

        private List<RoleEntity> RoleEntityList()
        {
            return new List<RoleEntity> {
                new RoleEntity { IdRole = 1, NameRole = ConstantsHelper.ADMIN_ROLE },
                new RoleEntity { IdRole = 2, NameRole = ConstantsHelper.EMPLOYEE_ROLE }
            };
        }

        private IDataBaseContext zmokuj_DataBaseContext_ze_sztucznymi_rolami()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            dataBaseContext.Roles = new FakeDbSet<RoleEntity>();
            foreach (RoleEntity roleEntity in RoleEntityList())
                dataBaseContext.Roles.Add(roleEntity);
            return dataBaseContext;
        }
    }
}