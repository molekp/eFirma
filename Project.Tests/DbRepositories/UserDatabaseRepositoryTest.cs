using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.Helpers;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Safety;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssertions;

namespace Project.Tests.DbRepositories
{
    [TestFixture]
    public class UserDatabaseRepositoryTest 
    {
        private IUserDataBaseRepository m_userDataBaseRepository;

        [SetUp]
        public void Init()
        {
            m_userDataBaseRepository = new UserDataBaseRepository
                {
                        DataBaseContext = Mokuj_DataBaseContext_i_wprowadz_uzytkownikow()
                };
        }

        //[Test]
        //public void EditUser_test()
        //{
        //    var editUserDto = new EditUserDto {
        //        EMail = "test@email.com",
        //        IdUser = 1,
        //        Password = "testowe",
        //        PasswordConfirm = "testowe",
        //        UserName = "test",
        //        RoleName = ConstantsHelper.CANDIDATE_ROLE
        //    };
        //    UserEntity changedUserEntity = m_userDataBaseRepository.DataBaseContext.Users.First(x => x.UserName == "admin");
        //    m_userDataBaseRepository.DataBaseContext.Stub(x => x.SetModified(null)).IgnoreArguments();

        //    //
        //    m_userDataBaseRepository.EditUser(editUserDto);
        //    UserEntity result = m_userDataBaseRepository.DataBaseContext.Users.First(x => x.UserName == "test");
        //    //
        //    Assert.IsNotNull(result, "źle edytowano login");
        //    Assert.AreEqual("test", result.UserName, "zle zedytowano nazwe uzytkownika");
        //    Assert.AreEqual(ConstantsHelper.ADMIN_ROLE, result.Role.NameRole, "edytowano rolę użytkownika(zabronione)");
        //    Assert.AreEqual("test@email.com", result.EMail, "żle edytowano email");
        //    Assert.AreEqual(changedUserEntity.Password, result.Password, "nie edytowano hasła");
        //    Assert.AreNotEqual("testowe", result.Password, "nie zhaszowano hasła");
        //}

        [Test]
        public void GetAllUsers_test()
        {
            //
            IEnumerable<UserEntity> result = m_userDataBaseRepository.GetAllUsers();
            //
            Assert.IsNotNull(result, "nie znaleziono uzytkownikow");
            Assert.AreEqual(5, result.Count(), "znaleziono nie wszystkich uzytkownikow");
        }

        [Test]
        public void GetRoleNameForUserName_test()
        {
            //
            string result = m_userDataBaseRepository.GetRoleNameForUserName("admin");
            //
            Assert.AreEqual(result, ConstantsHelper.ADMIN_ROLE, "wyszukano zła rolę użytkownika");
        }

        [Test]
        public void GetUser_test()
        {
            //
            UserEntity result = m_userDataBaseRepository.GetUser("admin");
            //
            Assert.IsNotNull(result, "nie znaleziono użytkownika");
            Assert.AreEqual("admin", result.UserName, "znaleziono złego użytkownika");
            Assert.AreEqual(1, result.IdUser, "znaleziony uzytkownik ma zły IdUser");
        }

       

        [Test]
        public void UserExists_test()
        {
            //
            bool result = m_userDataBaseRepository.DoesUserExists("admin");
            //
            Assert.AreEqual(true, result, "nie znaleziono uzytkownika");
        }

        [Test]
        public void AddUserToSafetyPointGroup__zwraca_prawde()
        {
            var group = new SafetyPointGroup() {IdSafetyPointGroup = 1, GroupName = "test"};
            var user = UserEntityList()[ 0 ];
            //
            bool result = m_userDataBaseRepository.AddUserToSafetyPointGroup(user,group);
            //
            result.Should().BeTrue("wynik jest falszem");
        }


        //[Test]
        //public void CreateUser_test()
        //{
        //    var userDto = new CreateUserDto {
        //        UserName = "test",
        //        EMail = "a@vsoft.pl",
        //        Password = "admin",
        //        RoleName = ConstantsHelper.ADMIN_ROLE
        //    };
        //    var roleEntity = new RoleEntity {
        //        IdRole = 1,
        //        NameRole = ConstantsHelper.ADMIN_ROLE
        //    };
        //    //act
        //    m_userDataBaseRepository.CreateUser(userDto, roleEntity);
        //    UserEntity result = m_userDataBaseRepository.DataBaseContext.Users.First(x => x.UserName == "test");
        //    //assert
        //    Assert.IsNotNull(result, "nie znaleziono uzytkownika o loginie 'test'");
        //    Assert.AreEqual("a@vsoft.pl", result.EMail, "źle zapisano adres emial");
        //    Assert.AreEqual(ConstantsHelper.ADMIN_ROLE, result.Role.NameRole, "źle przypisano rolę");
        //    Assert.AreNotEqual("admin", result.Password, "hasło nie zostało zhaszowane");
        //}

        /// <summary>
        /// metoda mokuje DataBaseContext i robi fake z DataBaseContext.Users 
        /// oraz tworzy użytkowników dla fake (DataBaseContext.Users)
        /// </summary>
        /// <returns></returns>
        private IDataBaseContext Mokuj_DataBaseContext_i_wprowadz_uzytkownikow()
        {
            var dataBaseContext = MockRepository.GenerateStub<IDataBaseContext>();
            dataBaseContext.Users = new FakeDbSet<UserEntity>();

            foreach (UserEntity userEntity in UserEntityList())
                dataBaseContext.Users.Add(userEntity);
            return dataBaseContext;
        }

        private List<RoleEntity> RoleEntityList()
        {
            return new List<RoleEntity> {
                new RoleEntity { IdRole = 1, NameRole = ConstantsHelper.ADMIN_ROLE },
                new RoleEntity { IdRole = 2, NameRole = ConstantsHelper.EMPLOYEE_ROLE }
            };
        }

        private List<UserEntity> UserEntityList()
        {
            List<RoleEntity> roleEntityList = RoleEntityList();
            return new List<UserEntity> {
                new UserEntity {
                    IdUser = 1,
                    UserName = "admin",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "md5"),
                    EMail = "admin@admin.com",
                    Role = roleEntityList[0],
                    UserSafetyPointGroups = new List<SafetyPointGroup>()
                },
                new UserEntity {
                    IdUser = 3,
                    UserName = "spr",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("spr", "md5"),
                    EMail = "admin@admin.com",
                    Role = roleEntityList[1],
                    UserSafetyPointGroups = new List<SafetyPointGroup>()
                },
                new UserEntity {
                    IdUser = 4,
                    UserName = "acki",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("acki", "md5"),
                    EMail = "acki@acki.com",
                    Role = roleEntityList[1],
                    UserSafetyPointGroups = new List<SafetyPointGroup>()
                },
                new UserEntity {
                    IdUser = 5,
                    UserName = "ziomek",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("ziomek", "md5"),
                    EMail = "ziomek@ziomek.com",
                    Role = roleEntityList[0],
                    UserSafetyPointGroups = new List<SafetyPointGroup>()
                },
                new UserEntity {
                    IdUser = 2,
                    UserName = "sprawdz",
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile("sprawdz", "md5"),
                    EMail = "sprawdz@sprawdz.com",
                    Role = roleEntityList[1],
                    UserSafetyPointGroups = new List<SafetyPointGroup>()
                }
            };
        }
    }
}