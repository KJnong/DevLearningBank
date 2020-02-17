using DL_Bank.Migrations;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<Transaction> Transactions { get; set; }
        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        public int SaveChanges()
        {
            return 0;
        }
    }

}