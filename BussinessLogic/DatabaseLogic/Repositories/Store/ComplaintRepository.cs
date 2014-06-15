using System;
using System.Linq;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Store;
using Database.Core.Interfaces;
using Database.Entities.Stores;

namespace BussinessLogic.DatabaseLogic.Repositories.Store
{
    public class ComplaintRepository : IComplaintRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public bool DoesExists(int a_itemId, ItemTypeEnum a_itemType)
        {
            switch (a_itemType)
            {
                case ItemTypeEnum.Product:
                    return DataBaseContext.Complaints.FirstOrDefault(x => x.ProductItem.IdItem == a_itemId) != null;
                case ItemTypeEnum.Service:
                    return DataBaseContext.Complaints.FirstOrDefault(x => x.ServiceItem.IdItem == a_itemId) != null;
            }
            return false;
        }

        public bool AddComplaint(Complaint a_complaint)
        {
            try
            {
                DataBaseContext.Complaints.Add(a_complaint);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}