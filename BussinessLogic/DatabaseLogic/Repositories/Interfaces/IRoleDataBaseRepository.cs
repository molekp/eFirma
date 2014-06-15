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
        ///Zwraca listê wszystkich typów u¿ytkowników w systemie.
        ///</summary>
        ///<returns></returns>
        IEnumerable<string> GetAllRoleNamesList();

        /// <summary>
        /// Zwraca rolê o podanej nazwie. Rola musi istnieæ w bazie.
        /// </summary>
        /// <param name="a_roleName">nazwa roli</param>
        /// <returns>RoleEntity</returns>
        /// <exception cref="ArgumentNullException">Jeœli nie znajdzie Roli w bazie danych</exception>
        RoleEntity GetRoleForRoleName(string a_roleName);

        IEnumerable<RoleEntity> GetAll();
        RoleEntity GetRole(int a_roleId);
    }
}