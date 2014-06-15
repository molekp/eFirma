using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.SupplyRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.SupplyRepositories
{
    class SupplyRepository : ISupplyRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }
        
        public bool AddSupply(Supply a_supplyEntity)
        {
            try{
                DataBaseContext.Supplies.Add(a_supplyEntity);
                DataBaseContext.SaveChanges();
            }
            catch (Exception){
                return false;
            }
            return true;
        }

        public IEnumerable<Supply> GetAllSupplies()
        {
            try
            {
                return DataBaseContext.Supplies.OrderBy(t => t.Firm).ThenBy(t => t.State);
            }
            catch (Exception)
            {
                return new List<Supply>();
            }
        }

        public Supply GetSupply(int a_idSupply)
        {
            return DataBaseContext.Supplies.FirstOrDefault(t => t.IdSupply == a_idSupply);
        }
        
        public ProductItem GetProduct(int a_idProduct)
        {
            return DataBaseContext.ProductItems.FirstOrDefault(t => t.IdItem == a_idProduct);
        }

        public int StoreProduct(ProductItem a_entity)
        {
            int id = 0;
            try
            {
                var product = GetProduct(a_entity.IdItem);
                DataBaseContext.SetModified(product);
                product.Name = a_entity.Name;
                product.Price = a_entity.Price;
                product.Quantity = a_entity.Quantity;
                product.Vin = a_entity.Vin;
                product.Attributes = a_entity.Attributes;
                product.ItemState = ItemState.Supplied;
                DataBaseContext.SaveChanges();
                id = product.IdItem;
            }
            catch (Exception)
            {
                return 0;
            }
            return id;
        }

        public bool RemoveProductFromSupply(Supply a_getSupply, ProductItem a_getProduct)
        {
            try
            {
                DataBaseContext.SetModified(a_getSupply);
                a_getSupply.ProductItems.Remove(a_getProduct);
                DataBaseContext.SaveChanges();
            }catch
            {
                return false;
            }
            return true;
        }

        public bool RemoveProduct(int a_idProduct)
        {

            try
            {
                var product = GetProduct(a_idProduct);
                DataBaseContext.ProductItems.Remove(product);
                DataBaseContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void setState(int state, Supply supply)
        {
            try
            {
                DataBaseContext.SetModified(supply);
                supply.State = state;
                DataBaseContext.SaveChanges();
            }
            catch
            {
            }
        }

        public bool SaveSupply(Supply a_supply)
        {
            try
            {
                var supply = GetSupply(a_supply.IdSupply);
                DataBaseContext.SetModified(supply);
                supply.Firm = a_supply.Firm;
                supply.RealizationTime = a_supply.RealizationTime;
                supply.CreationTime = a_supply.CreationTime;
                supply.DeliveredTime = a_supply.DeliveredTime;
                supply.State = a_supply.State;
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveSupply(Supply a_supply)
        {
            try
            {
                DataBaseContext.Supplies.Remove(a_supply);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReadyToSendSupply(int a_idSupply)
        {
            try
            {
                var supply = GetSupply(a_idSupply);
                DataBaseContext.SetModified(supply);
                supply.State = 2;
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool SendSupply(int a_idSupply)
        {
            try
            {
                var supply = GetSupply(a_idSupply);
                DataBaseContext.SetModified(supply);
                supply.State = 3;
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



        public int AddProduct(ProductItem a_productItem)
        {
            try
            {
                a_productItem.ItemState = ItemState.Supplied;
                DataBaseContext.ProductItems.Add(a_productItem);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return a_productItem.IdItem;
        }

        public bool AddProductToSupply(int a_idSupply, int a_idProduct)
        {
            try
            {
                var supply = GetSupply(a_idSupply);
                DataBaseContext.SetModified(supply);
                var product = GetProduct(a_idProduct);
                if(product.IdItem != 0)
                {
                    supply.ProductItems.Add(product);
                }
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}