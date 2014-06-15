using System;
using System.Web.Security;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.Repositories;
using Database.Core;

namespace BussinessLogic.AuthorizationLogic
{
    public class MyRoleProvider : RoleProvider
    {
        private readonly UserDataBaseRepository m_userDataBaseRepository;

        public MyRoleProvider()
        {
            m_userDataBaseRepository = new UserDataBaseRepository { DataBaseContext = new DataBaseContext() };
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void AddUsersToRoles(string[] a_usernames, string[] a_roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string a_roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string a_roleName, bool a_throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string a_roleName, string a_usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string a_username)
        {
            string roleName = m_userDataBaseRepository.GetRoleNameForUserName(a_username);
            return new[] { roleName };
        }

        public override string[] GetUsersInRole(string a_roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string a_userName, string a_roleName)
        {
            return m_userDataBaseRepository.GetRoleNameForUserName(a_userName) == a_roleName;
        }

        public override void RemoveUsersFromRoles(string[] a_usernames, string[] a_roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string a_roleName)
        {
            throw new NotImplementedException();
        }
    }
}