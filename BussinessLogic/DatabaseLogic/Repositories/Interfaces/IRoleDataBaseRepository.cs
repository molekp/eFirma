using System.Collections.Generic;
using System;
using BussinessLogic.Mappers;
using Database.Core.Interfaces;
using Database.Entities;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces
{
    public interface IRoleDataBaseRepository
    {
        IDataBaseContext DataBaseContext { set; }

        ///<summary>
        ///Zwraca list� wszystkich typ�w u�ytkownik�w w systemie.
        ///</summary>
        ///<returns></returns>
        IEnumerable<string> GetAllRoleNamesList();

        /// <summary>
        /// Zwraca rol� o podanej nazwie. Rola musi istnie� w bazie.
        /// </summary>
        /// <param name="a_roleName">nazwa roli</param>
        /// <returns>RoleEntity</returns>
        /// <exception cref="ArgumentNullException">Je�li nie znajdzie Roli w bazie danych</exception>
        RoleEntity GetRoleForRoleName(string a_roleName);

        IEnumerable<RoleEntity> GetAll();
        RoleEntity GetRole(int a_roleId);
    }
}