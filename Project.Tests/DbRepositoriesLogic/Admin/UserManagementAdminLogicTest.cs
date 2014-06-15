using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Safety;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin;
using Database.Entities;
using Database.Entities.Safety;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositoriesLogic.Admin
{
    [TestFixture]
    public class UserManagementAdminLogicTest
    {
        private IUserManagementAdminLogic m_userManagementAdminLogic;

        [SetUp]
        public void Init()
        {
            m_userManagementAdminLogic = RepositoryLogicFactory.GetUserManagementAdminLogic();
            m_userManagementAdminLogic.UserDataBaseRepository = MockRepository.GenerateStub<IUserDataBaseRepository>();
            m_userManagementAdminLogic.UserDataBaseRepository.Stub(x => x.GetAllUsers())
                                      .Return(UserEntityList());
        }


        [Test]
        public void GetAllUsersForDiplay__zwraca_liste_AdminDisplayUserDto()
        {
            //
            var result = m_userManagementAdminLogic.GetAllUsersForDiplay();
            //
            result.Should().NotBeNull("wynik jest nullem");
            result.Count().Should().Be(UserEntityList().Count());
        }

        [Test]
        public void GetUserForManageSafetyPointGroups()
        {
            m_userManagementAdminLogic.UserDataBaseRepository.Stub(x => x.GetUser(1))
                                      .IgnoreArguments()
                                      .Return(new UserEntity {IdUser = 1, UserName = "test",UserSafetyPointGroups = new List<SafetyPointGroup>()});
            m_userManagementAdminLogic.SafetyPointGroupsRepository =
                    MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_userManagementAdminLogic.SafetyPointGroupsRepository.Stub(x => x.GetAll())
                                      .Return(new List<SafetyPointGroup>());
            var expected = new UserForManageSafetyPointGroups {IdUser = 1, UserName = "test"};
            //
            var result = m_userManagementAdminLogic.GetUserForManageSafetyPointGroups(1);
            //
            result.Should().NotBeNull("wynik jest nullem");
            result.ShouldHave().AllPropertiesBut(x=>x.SafetyPointGroupChoicesToAdd).But(x=>x.UserCurrentSafetyPointGroups).EqualTo(expected);
        }

        [Test] 
        public void AddUserToSafetyPointGroup_dla_istniejacych_grupy_i_usera__zwraca_prawde() {

            Mokuj_do_AddUserToSafetyPointGroup_i_zwroc(UserEntityList()[0], new SafetyPointGroup(), true);
            //
            var result = m_userManagementAdminLogic.AddUserToSafetyPointGroup(1, 1);
            //
            result.Should().BeTrue(); }

        [Test]
        public void AddUserToSafetyPointGroup_dla_nie_istniejacych_grupy_i_usera__zwraca_falsz()
        {
            Mokuj_do_AddUserToSafetyPointGroup_i_zwroc(null, null, true);
            //
            var result = m_userManagementAdminLogic.AddUserToSafetyPointGroup(1, 1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void AddUserToSafetyPointGroup_dla_istniejaej_grupy_i_nieistniejacego_usera__zwraca_falsz()
        {
            Mokuj_do_AddUserToSafetyPointGroup_i_zwroc(UserEntityList()[0], null, true);
            //
            var result = m_userManagementAdminLogic.AddUserToSafetyPointGroup(1, 1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void AddUserToSafetyPointGroup_dla_nieistniejacej_grupy_i_istniejacego_usera__zwraca_falsz()
        {
            Mokuj_do_AddUserToSafetyPointGroup_i_zwroc(null, new SafetyPointGroup(), true);
            //
            var result = m_userManagementAdminLogic.AddUserToSafetyPointGroup(1, 1);
            //
            result.Should().BeFalse();
        }

        [Test]
        public void AddUserToSafetyPointGroup_dla_usera_ktory_juz_ma_wskazana_grupe__zwraca_falsz()
        {
            var user = UserEntityList()[ 0 ];
            var group = new SafetyPointGroup() {IdSafetyPointGroup = 1};
            user.UserSafetyPointGroups.Add(group);
            Mokuj_do_AddUserToSafetyPointGroup_i_zwroc(user, group, true);
            //
            var result = m_userManagementAdminLogic.AddUserToSafetyPointGroup(1, 1);
            //
            result.Should().BeFalse();
        }

        private void Mokuj_do_AddUserToSafetyPointGroup_i_zwroc(UserEntity a_returnGetUser,
                                                                SafetyPointGroup a_returnGroupToAdd,bool a_return)
        {
            m_userManagementAdminLogic.UserDataBaseRepository.Stub(x => x.GetUser(1)).IgnoreArguments().Return(a_returnGetUser);
            m_userManagementAdminLogic.SafetyPointGroupsRepository =
                    MockRepository.GenerateStub<ISafetyPointGroupsRepository>();
            m_userManagementAdminLogic.SafetyPointGroupsRepository.Stub(x => x.Get(1))
                                      .IgnoreArguments()
                                      .Return(a_returnGroupToAdd);
            m_userManagementAdminLogic.UserDataBaseRepository.Stub(x => x.AddUserToSafetyPointGroup(null, null))
                                      .IgnoreArguments()
                                      .Return(a_return);
        }

        private List<UserEntity> UserEntityList()
        {
            return new List<UserEntity>
                {
                        new UserEntity{IdUser = 1,EMail = "asdf",UserSafetyPointGroups = new List<SafetyPointGroup>()},
                        new UserEntity{IdUser = 2,EMail = "qwer",UserSafetyPointGroups = new List<SafetyPointGroup>()}
                };
        }

    }
}