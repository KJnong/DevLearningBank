using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DL_Bank.Models
{
    public class CheckingAccount
    {
        public int Id { get; set; }

        [Required]
        //[StringLength(10, MinimumLength =10)]
        [RegularExpression(@"\d{10}", ErrorMessage ="Account # must contain 10 digits")]
        [Display(Name ="Account #")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Name 
        { 
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            } 
        }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public virtual ApplicationUser user { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}