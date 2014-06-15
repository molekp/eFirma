using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.Customers;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Customer
{
    public interface  ICustomerRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        List<Database.Entities.Customers.Customer> GetAll();
        Database.Entities.Customers.Customer Get(int a_idCustomer);
        Firm GetFirm(int a_idCustomer);
    }
}
