using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Customer;
using Database.Core.Interfaces;
using Database.Entities.Customers;

namespace BussinessLogic.DatabaseLogic.Repositories.Customer
{
    class CustomerRepository :ICustomerRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public List<Database.Entities.Customers.Customer> GetAll()
        {
            return DataBaseContext.Customers.ToList();
        }

        public Database.Entities.Customers.Customer Get(int a_idCustomer)
        {
            return DataBaseContext.Customers.FirstOrDefault(x => x.IdCustomer == a_idCustomer);
        }

        public Firm GetFirm(int a_idCustomer)
        {
            try
            {
                return (Firm) DataBaseContext.Customers.FirstOrDefault(x => x.IdCustomer == a_idCustomer);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
