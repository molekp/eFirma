using System;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.Helpers;
using Database.Core;
using System.Web.Security;
using Database.Entities;

namespace BussinessLogic.AuthorizationLogic
{
    public class MyMembershipProvider : MembershipProvider
    {
        private readonly UserDataBaseRepository m_userDataBaseConnector;

        public MyMembershipProvider()
        {
            m_userDataBaseConnector = new UserDataBaseRepository { DataBaseContext = new DataBaseContext() };
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUser CreateUser(
            string a_username, string a_password, string a_email, string a_passwordQuestion, string a_passwordAnswer, bool a_isApproved,
            object a_providerUserKey, out MembershipCreateStatus a_status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(
            string a_username, string a_password, string a_newPasswordQuestion, string a_newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string a_username, string a_answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string a_username, string a_oldPassword, string a_newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string a_username, string a_answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser a_user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string a_username, string a_password)
        {
            if (string.IsNullOrEmpty(a_password.Trim()) ||
                string.IsNullOrEmpty(a_username.Trim()))
                return false;
            string hash = Hasher.HashPassword(a_password);
            return m_userDataBaseConnector.GetAllUsers().Any(u => (u.UserName == a_username.Trim()) && (u.Password == hash));
        }

        public override bool UnlockUser(string a_userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object a_providerUserKey, bool a_userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string a_username, bool a_userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string a_email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string a_username, bool a_deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int a_pageIndex, int a_pageSize, out int a_totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string a_usernameToMatch, int a_pageIndex, int a_pageSize, out int a_totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string a_emailToMatch, int a_pageIndex, int a_pageSize, out int a_totalRecords)
        {
            throw new NotImplementedException();
        }
    }
}