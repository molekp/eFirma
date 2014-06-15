using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogic.DTOs.WarehouseDtos
{
    public class ManageProductWarehouseDto
    {
        public int IdProductWarehouse { get; set; }

        public int IdWarehouse { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductItemForManageProductWarehouseDto> ProductItems { get; set; }

        public int AddProductItem { get; set; } 
    }
}
