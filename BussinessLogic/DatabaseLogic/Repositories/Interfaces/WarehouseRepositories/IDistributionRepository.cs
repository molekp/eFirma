using System.Collections.Generic;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories
{
    public interface IDistributionRepository
    {
        IEnumerable<Distribution> GetAll();
        Distribution Get(int a_distributionId);
        bool Edit(Distribution a_distribution);
        int Add(Distribution a_distribution);
        IEnumerable<IItem> GetDistributionItems(int a_distributionId);
        bool RemoveProductItem(Distribution a_distribution, ProductItem a_productItem);
        bool RemoveServiceItem(Distribution a_distribution, ServiceItem a_serviceItem);
    }
}