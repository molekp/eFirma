using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Tests.Selenium.Login
{
    public class Logins
    {
        public static Login UserOne
        {
            get
            {
                return new Login("admin", "admin");
            }
        }

        public static Login UserTwo
        {
            get
            {
                return new Login("ziomek", "ziomek");
            }
        }
    }
}
