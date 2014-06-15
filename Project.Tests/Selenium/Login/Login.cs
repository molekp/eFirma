using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Tests.Selenium.Login
{
    public class Login
    {
        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public override bool Equals(object obj)
        {
            Login compareTo = obj as Login;
            if (compareTo == null)
                return false;

            return compareTo.Username == Username &&
                   compareTo.Password == Password;
        }
    }

}
