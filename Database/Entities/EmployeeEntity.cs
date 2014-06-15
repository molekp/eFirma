using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Database.Entities
{
    public class EmployeeEntity
    {
        [Key]
        public int IdEmployee { get; set; }

        public virtual UserEntity UserEntity { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public string BankAccountNumber { get; set; }
    }
}
