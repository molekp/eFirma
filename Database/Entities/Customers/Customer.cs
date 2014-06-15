using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Database.Entities.Customers
{
    public abstract class Customer
    {
        [Key]
        public int IdCustomer { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }
    }
}
