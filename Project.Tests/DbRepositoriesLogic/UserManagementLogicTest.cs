using System.Web.Security;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.Helpers;
using Database.Entities;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositoriesLogic
{
    [TestFixture]
    public class UserManagementLogicTest
    {
        private IUserManagementLogic m_userManagementLogic;

        [SetUp]
        public void Init()
        {
            m_userManagementLogic = RepositoryLogicFactory.GetUserManagementLogic();
            m_userManagementLogic.RoleDataBaseRepository = MockRepository.GenerateStub<IRoleDataBaseRepository>();
            m_userManagementLogic.UserDataBaseRepository = Mokuj_UserDataBaseRepository_i_podmien_metody_GetUser(
                new UserEntity {
                    IdUser = 1,
                    UserName = "admin",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "md5"),
                    EMail = "admin@admin.com",
                    Role = new RoleEntity { NameRole = ConstantsHelper.ADMIN_ROLE }
                });
        }

        //[Test]
        //public void EditUser_test()
        //{
        //    var editUserDto = new EditUserDto
        //    {
        //        EMail = "test@email.com",
        //        IdUser = 1,
        //        Password = "testowe",
        //        PasswordConfirm = "testowe",
        //        UserName = "test",
        //        RoleName = ConstantsHelper.CANDIDATE_ROLE
        //    };
        //    m_userManagementLogic.TypesOfSafetyPointsRepository.Stub(x => x.EditUser(null)).IgnoreArguments();
        //    //
        //    m_userManagementLogic.EditUser(editUserDto);
        //    //

        //    m_userManagementLogic.TypesOfSafetyPointsRepository.AssertWasCalled(x => x.EditUser(null), x => x.IgnoreArguments());
        //}


        [Test]
        public void GetRoleNameForUserName_test()
        {
            m_userManagementLogic.UserDataBaseRepository.Stub(x => x.GetRoleNameForUserName(null))
                .IgnoreArguments().Return(ConstantsHelper.ADMIN_ROLE);
            //
            string result = m_userManagementLogic.GetRoleNameForUserName("admin");
            //
            result.Should().BeEquivalentTo(ConstantsHelper.ADMIN_ROLE, "wyszukano zła rolę użytkownika");
        }

        //[Test]
        //public void GetUserForDetails_test()
        //{
        //    var expectedDetailsUserDto = new DetailsUserDto { UserName = "admin", EMail = "admin@admin.com", RoleName = ConstantsHelper.ADMIN_ROLE };
        //    //
        //    var result = m_userManagementLogic.GetUserForDetails("admin");
        //    //
        //    result.ShouldHave().AllProperties().EqualTo(expectedDetailsUserDto,"Nie wszystkie pola są poprawne");
        //}

        //[Test]
        //public void GetUserForEdit_test()
        //{
        //    var expectedEditUserDto = new EditUserDto { UserName = "admin", EMail = "admin@admin.com", RoleName = ConstantsHelper.ADMIN_ROLE, IdUser = 1};
        //    //
        //    var result = m_userManagementLogic.GetUserForEdit("admin");
        //    //
        //    result.ShouldHave().AllProperties().EqualTo(expectedEditUserDto, "Nie wszystkie pola są poprawne");
        //}

        [Test]
        public void DoesUserExists_test()
        {
            m_userManagementLogic.UserDataBaseRepository.Stub(x => x.DoesUserExists(null)).IgnoreArguments().Return(true);
            //
            bool result = m_userManagementLogic.DoesUserExists("admin");
            //
            result.Should().BeTrue("nie znaleziono użytkownika"); 
        }




        //[Test]
        //public void CreateUser_test()
        //{
        //    var userDto = new CreateUserDto
        //    {
        //        UserName = "test",
        //        EMail = "a@vsoft.pl",
        //        Password = "admin",
        //        RoleName = ConstantsHelper.ADMIN_ROLE
        //    };
        //    var roleEntity = new RoleEntity { IdRole = 1, NameRole = ConstantsHelper.ADMIN_ROLE };
           
        //    m_userManagementLogic.TypesOfSafetyPointsRepository.Stub(x => x.CreateUser(null, null)).IgnoreArguments().WhenCalled(mi => { });
        //    m_userManagementLogic.RoleDataBaseRepository.Stub(x => x.GetRoleForRoleName(null))
        //        .IgnoreArguments().Return(roleEntity);
        //    //act
        //    m_userManagementLogic.CreateUser(userDto);
        //    //assert
        //    m_userManagementLogic.TypesOfSafetyPointsRepository.AssertWasCalled(x => x.CreateUser(null, null), x => x.IgnoreArguments());
        //}

        
        private IUserDataBaseRepository Mokuj_UserDataBaseRepository_i_podmien_metody_GetUser(UserEntity a_returnEntity)
        {
            var userDataBaseRepository = MockRepository.GenerateStub<IUserDataBaseRepository>();
            userDataBaseRepository.Stub(x => x.GetUser(null)).IgnoreArguments().Return(a_returnEntity);
            userDataBaseRepository.Stub(x => x.GetUser(1)).IgnoreArguments().Return(a_returnEntity);
            return userDataBaseRepository;
        }
        

    }
}