using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class FirmViewDto
    {
        [Display(Name = "idFirm", ResourceType = typeof(Resource))]
        public int IdFirm { get; set; }

        [Display(Name = "firmAddress", ResourceType = typeof(Resource))]
        public string Address { get; set; }

        [Display(Name = "firmName", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "firmPhone", ResourceType = typeof(Resource))]
        public string Phone { get; set; }

        [Display(Name = "firmInfo", ResourceType = typeof(Resource))]
        public string Info { get; set; }
    }
}