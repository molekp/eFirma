using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using Database.Core.Interfaces;
using Database.Entities.Stores;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Store
{
    public interface IComplaintRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        bool DoesExists(int a_itemId, ItemTypeEnum a_itemType);
        bool AddComplaint(Complaint a_complaint);
    }
}
