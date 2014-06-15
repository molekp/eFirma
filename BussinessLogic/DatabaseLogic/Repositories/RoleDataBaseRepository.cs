using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.Mappers;
using Database.Core.Interfaces;
using Database.Entities;

namespace BussinessLogic.DatabaseLogic.Repositories
{
    public class RoleDataBaseRepository : IRoleDataBaseRepository
    {
        public IDataBaseContext DataBaseContext { private get; set; }

        ///<summary>
        ///Zwraca listę wszystkich typów użytkowników w systemie.
        ///</summary>
        ///<returns></returns>
        public IEnumerable<string> GetAllRoleNamesList()
        {
            List<RoleEntity> roleEntityList = DataBaseContext.Roles.OrderBy(r => r.NameRole).ToList();

            return roleEntityList.Select(roleEntity => roleEntity.NameRole).ToList();
        }

        /// <summary>
        /// Zwraca rolę o podanej nazwie. Rola musi istnieć w bazie.
        /// </summary>
        /// <param name="a_roleName">nazwa roli</param>
        /// <returns>RoleEntity</returns>
        /// <exception cref="ArgumentNullException">Jeśli nie znajdzie Roli w bazie danych</exception>
        public RoleEntity GetRoleForRoleName(string a_roleName)
        {
            return DataBaseContext.Roles.FirstOrDefault(r => r.NameRole == a_roleName);
        }

        public IEnumerable<RoleEntity> GetAll()
        {
            return DataBaseContext.Roles.ToList();
        }

        public RoleEntity GetRole(int a_roleId)
        {
            return DataBaseContext.Roles.FirstOrDefault(x => x.IdRole == a_roleId);
        }
    }
}
