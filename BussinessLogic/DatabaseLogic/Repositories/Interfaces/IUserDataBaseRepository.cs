using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces
{
    public interface IUserDataBaseRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
       
        /// <summary>
        /// Zwraca listę wszystkich użytkowników
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserEntity> GetAllUsers();

        /// <summary>
        /// Zwraca użytkownika o podanym loginie.
        /// </summary>
        /// <param name="a_userName">login</param>
        /// <returns></returns>
        UserEntity GetUser(string a_userName);

        /// <summary>
        /// Dodaje użytkownika do bazy danych.
        /// </summary>
        /// <param name="a_userEntity">użytkownik</param>
        /// <param name="a_roleEntity"> </param>
        bool CreateUser(UserEntity a_userEntity);

        /// <summary>
        /// Sprawdza czy użytkownik istnieje w bazie danych.
        /// </summary>
        /// <param name="a_userName">nazwa użytkownika</param>
        /// <returns></returns>
        bool DoesUserExists(string a_userName);

        /// <summary>
        /// Zwraca nazwę roli dla danego usera.
        /// </summary>
        /// <param name="a_userName"></param>
        /// <returns></returns>
        string GetRoleNameForUserName(string a_userName);

        /// <summary>
        /// Zmienia dane uzytkownika
        /// </summary>
        /// <param name="a_userEntity"></param>
        bool EditUser(UserEntity a_userEntity);

        UserEntity GetUser(int a_idUser);
        bool AddUserToSafetyPointGroup(UserEntity a_userEntity, SafetyPointGroup a_safetyPointGroup);
        bool RemoveUserFromSafetyPointGroup(UserEntity a_user, SafetyPointGroup a_group);
    }
}