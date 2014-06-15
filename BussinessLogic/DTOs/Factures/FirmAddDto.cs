using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Factures
{
    public class FirmAddDto
    {
        [Display(Name = "idFirm", ResourceType = typeof(Resource))]
        public int IdFirm { get; set; }

        [Display(Name = "firmAddress", ResourceType = typeof(Resource))]
        [Required]
        public string Address { get; set; }

        [Display(Name = "firmName", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "firmPhone", ResourceType = typeof(Resource))]
        public string Phone { get; set; }

        [Display(Name = "firmInfo", ResourceType = typeof(Resource))]
        public string Info { get; set; }

        [Display(Name = "firmBankAccountNumber", ResourceType = typeof(Resource))]
        public string BankAccountNumber { get; set; }

        [Display(Name = "firmBankInfo", ResourceType = typeof(Resource))]
        public string BankInfo { get; set; }

        [Display(Name = "firmNIP", ResourceType = typeof(Resource))]
        public string NIP { get; set; }
    }
}