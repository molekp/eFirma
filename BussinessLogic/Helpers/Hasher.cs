using System.Web.Security;

namespace BussinessLogic.Helpers
{
    public static class Hasher
    {
        public static string HashPassword(string a_password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(a_password.Trim(), "md5");
        }
    }
}
