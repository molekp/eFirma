using System.Web.Security;

namespace BussinessLogic.AuthorizationLogic
{

    public interface IFormsAuthentication
    {
        void DoAuth(string a_username, bool a_remember);
        void SignOut();
    }

    public class FormsAuthenticationWrapper : IFormsAuthentication
    {
        public void DoAuth(string a_username, bool a_remember)
        {
            FormsAuthentication.SetAuthCookie(a_username, a_remember);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}
