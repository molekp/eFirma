using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Customers
{
    public class DisplayCustomerDto
    {
        [Display(Name = "customerName", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        public int IdCustomer { get; set; }

        [Display(Name = "customerAddress", ResourceType = typeof(Resource))]
        public string Address { get; set; }

        public bool IsFirm { get; set; }
    }
}