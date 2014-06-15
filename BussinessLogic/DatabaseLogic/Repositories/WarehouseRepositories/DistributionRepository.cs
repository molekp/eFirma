using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories
{
    public class DistributionRepository : IDistributionRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<Distribution> GetAll()
        {
            return DataBaseContext.Distributions.ToList();
        }

        public Distribution Get(int a_distributionId)
        {
            return DataBaseContext.Distributions.FirstOrDefault(x => x.IdDistribution == a_distributionId);
        }

        public bool Edit(Distribution a_distribution)
        {
            try
            {
                DataBaseContext.SetModified(a_distribution);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Add(Distribution a_distribution)
        {
            try
            {
                DataBaseContext.Distributions.Add(a_distribution);
                DataBaseContext.SaveChanges();
                return a_distribution.IdDistribution;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IEnumerable<IItem> GetDistributionItems(int a_distributionId)
        {
            try
            {
                var distribution = Get(a_distributionId);
                List<IItem> items = distribution.ProductItems.Select(x=> (IItem)x).ToList();
                items.AddRange(distribution.ServiceItems.Select(x => (IItem)x).ToList());
                return items;
            }
            catch (Exception)
            {
                return new List<IItem>();
            }
        }

        public bool RemoveProductItem(Distribution a_distribution, ProductItem a_productItem)
        {
            try
            {
                if(a_distribution.ProductItems.Remove(a_productItem))
                {
                    if (!a_distribution.ServiceItems.Any() && !a_distribution.ProductItems.Any())
                    {
                        DataBaseContext.Distributions.Remove(a_distribution);
                    }
                    else
                    {
                        DataBaseContext.SetModified(a_distribution);
                    }
                    DataBaseContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveServiceItem(Distribution a_distribution, ServiceItem a_serviceItem)
        {
            try
            {
                if (a_distribution.ServiceItems.Remove(a_serviceItem))
                {
                    if (!a_distribution.ServiceItems.Any() && !a_distribution.ProductItems.Any())
                    {
                        DataBaseContext.Distributions.Remove(a_distribution);
                    }
                    else
                    {
                        DataBaseContext.SetModified(a_distribution);
                    }
                    DataBaseContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
