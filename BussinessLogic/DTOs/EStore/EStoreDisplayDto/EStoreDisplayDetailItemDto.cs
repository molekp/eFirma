using System;
using BussinessLogic.DTOs.WarehouseDtos.Items;

namespace BussinessLogic.DTOs.EStore.EStoreDisplayDto
{
    public class EStoreDisplayDetailItemDto
    {
        public int  IdItem { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }   

        public ItemTypeEnum ItemType { get; set; }

        public string SaleType { get; set; }

        public int ItemState { get; set; }

        public string Vin { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}