using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Store
{
    public class StoreSellDto
    {
        public int StoreId { get; set; }

        public int DistributionId { get; set; }

        [Display(Name = "totalPrice", ResourceType = typeof(Resource))]
        public decimal TotalPrice { get; set; }

        public IEnumerable<SimpleSellItemDto> Items { get; set; }
    }
}
