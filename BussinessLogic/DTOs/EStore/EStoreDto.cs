using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Database.Entities.WarehouseEntities;
using ResourceLibrary;

namespace BussinessLogic.DTOs.EStore
{
    public class EStoreDto
    {
        public int IdEStore { get; set; }

        [Display(Name = "nameOfEStore", ResourceType = typeof(Resource))]
        public String Name { get; set; }

        [Display(Name = "collectionOfWarehouses", ResourceType = typeof(Resource))]
        public IEnumerable<Warehouse> Warehouses { get; set; }
    }
}
