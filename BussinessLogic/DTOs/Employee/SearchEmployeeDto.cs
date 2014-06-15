using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.DTOs.Employee
{
    public class SearchEmployeeDto
    {
        public int IdEmployee { get; set; }

        public string UserName { get; set; }

        public string EMail { get; set; }

        public string NameRole { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }
    }
}
