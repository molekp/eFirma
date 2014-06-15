using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ResourceLibrary;

namespace BussinessLogic.DTOs.EStore.EStoreCategoryDto
{
    public class EditEStoreCategoryDto
    {
        public int IdEStoreCategory { get; set; }

        public int IdEStore { get; set; }

        [Display(Name = "nameOfEStoreCategory", ResourceType = typeof(Resource))]
        [Required]
        public String Name { get; set; }

        [Required]
        public int SortOrder { get; set; }
    }
}
