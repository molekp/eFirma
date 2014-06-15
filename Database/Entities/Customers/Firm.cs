using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.Customers
{
    [Table("Firm")]
    public class Firm : Customer
    {
        public string Phone { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankInfo { get; set; }

        public string Info { get; set; }

        public string NIP { get; set; }

    }
}
