using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories
{
    public class UserDataBaseRepository : IUserDataBaseRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }
        
        /// <summary>
        /// Zwraca listę wszystkich użytkowników
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return DataBaseContext.Users.OrderBy(r => r.UserName).ToList();
        }
        
        /// <summary>
        /// Zwraca użytkownika o podanym loginie.
        /// </summary>
        /// <param name="a_userName">login</param>
        /// <returns></returns>
        public UserEntity GetUser(string a_userName)
        {
            return DataBaseContext.Users.FirstOrDefault(u => u.UserName == a_userName);
        }

        /// <summary>
        /// Dodaje użytkownika do bazy danych.
        /// </summary>
        /// <param name="a_userEntity">użytkownik</param>
        /// <param name="a_roleEntity"> </param>
        public bool CreateUser(UserEntity a_userEntity)
        {
            try
            {
                DataBaseContext.Users.Add(a_userEntity);
                DataBaseContext.SaveChanges();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sprawdza czy użytkownik istnieje w bazie danych.
        /// </summary>
        /// <param name="a_userName">nazwa użytkownika</param>
        /// <returns></returns>
        public bool DoesUserExists(string a_userName)
        {
            return (DataBaseContext.Users.SingleOrDefault( u => (u.UserName == a_userName) ) != null);
        }

        /// <summary>
        /// Zwraca nazwę roli dla danego usera.
        /// </summary>
        /// <param name="a_userName"></param>
        /// <returns></returns>
        public string GetRoleNameForUserName(string a_userName)
        {
            var user = GetUser(a_userName);

            if (user == null)
            {
                return "";
            }

            return user.Role.NameRole;
        }

        /// <summary>
        /// Zmienia dane uzytkownika
        /// </summary>
        /// <param name="a_editUserEntity"></param>
        public bool EditUser(UserEntity a_editUserEntity)
        {
            try
            {
                DataBaseContext.SetModified(a_editUserEntity);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserEntity GetUser(int a_idUser)
        {
            return DataBaseContext.Users.First(u => u.IdUser == a_idUser);
        }

        public bool AddUserToSafetyPointGroup(UserEntity a_userEntity, SafetyPointGroup a_safetyPointGroup)
        {
            try
            {
                a_userEntity.UserSafetyPointGroups.Add(a_safetyPointGroup);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool RemoveUserFromSafetyPointGroup(UserEntity a_user, SafetyPointGroup a_group)
        {
            try
            {
                a_user.UserSafetyPointGroups.Remove(a_group);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
