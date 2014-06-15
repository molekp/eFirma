using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories
{
    public interface IServiceWarehouseRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        ServiceWarehouse Get(int a_idServiceWarehouse);
        IEnumerable<ServiceWarehouse> GetAll();
        IEnumerable<ServiceWarehouse> GetAllFreeServiceWarehouses();
        bool Add(ServiceWarehouse a_serviceWarehouse, ServiceItem a_serviceItem);
        bool RemoveServiceItemFromWarehouse(ServiceWarehouse a_serviceWarehouse, ServiceItem a_serviceItem);
    }
}