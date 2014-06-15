using BussinessLogic.DTOs.Account;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.Mappers;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces
{
    public interface IUserManagementLogic
    {
        IUserDataBaseRepository UserDataBaseRepository { get; set; }
        IRoleDataBaseRepository RoleDataBaseRepository { get; set; }
        LogOnMapper LogOnMapper { set; }

        
        /// <summary>
        /// Tworzy uzytkownika i dodaje go do bazy danych
        /// </summary>
        /// <param name="a_createUserDto">Dto zawieraj¹ce informacje o kandydacie oraz jego dane logowania</param>
        bool CreateUser(RegisterUserDto a_createUserDto);

        /// <summary>
        /// Wyszukuje uzytkownika do widoku edycji uzytkownika
        /// </summary>
        /// <param name="a_userName"></param>
        /// <returns></returns>
        EditUserDto GetUserForEdit(string a_userName);

        /// <summary>
        /// Wyszukuje uzytkownika do widoku detali uzytkownika
        /// </summary>
        /// <param name="a_userName"></param>
        /// <returns></returns>
        DetailsUserDto GetUserForDetails(string a_userName);
  
        string GetRoleNameForUserName(string a_userName);
        bool DoesUserExists(string a_userName);

        bool EditUser(EditUserDto a_editUserDto);
    }
}