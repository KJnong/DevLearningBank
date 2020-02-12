using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DL_Bank.Models
{
    public interface IApplicationDbContext
    {
        public IDbSet<Transaction> Transactions { get; set; }
        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        int SaveChanges();
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public IDbSet<Transaction> Transactions { get; set; }
        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}