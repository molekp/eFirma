using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BussinessLogic.DTOs.Customers;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using ResourceLibrary;

namespace BussinessLogic.DTOs.EStore
{
    public class AddEStoreDto
    {
        [Display(Name = "nameOfEStore", ResourceType = typeof (Resource))]
        [Required]
        public String Name { get; set; }

    }
}
