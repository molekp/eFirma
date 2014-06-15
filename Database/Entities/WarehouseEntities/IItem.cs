using System;
using System.Collections.Generic;

namespace Database.Entities.WarehouseEntities
{
    public interface IItem
    {
        int IdItem { get; set; }

        string Name { get; set; }

        decimal Price { get; set; }

        double Quantity { get; set; }

        string Vin { get; set; }

        ItemState ItemState { get; set; }

        DateTime ExpirationTime { get; set; }

        SaleType SaleType { get; set; }

        string PKWiU { get; set; }

        double? Discount { get; set; }
    }

    public enum ItemState
    {
        Supplied =0, InWarehouse=1, PreDistributed=2, Distributed=3
    }
}
