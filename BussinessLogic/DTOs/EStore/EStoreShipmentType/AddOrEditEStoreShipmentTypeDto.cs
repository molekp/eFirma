using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.EStore.EStoreShipmentType
{
    public class AddOrEditEStoreShipmentTypeDto
    {
        public int IdEStore { get; set; }

        public int IdEStoreShipmentType { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public decimal PriceShipment { get; set; }

        public int Sortorder { get; set; }
    }
}
