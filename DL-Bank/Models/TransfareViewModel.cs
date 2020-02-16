using System.ComponentModel.DataAnnotations;

namespace DL_Bank.Controllers
{
    public class TransfareViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public int CheckingAccountId { get; set; }
    }
}