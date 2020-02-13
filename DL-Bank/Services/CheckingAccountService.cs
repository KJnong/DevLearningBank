using DL_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DL_Bank.Services
{
    public class CheckingAccountService
    {
        private IApplicationDbContext db;

        public CheckingAccountService(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance) 
        {
            var accountNumber = (12346 + db.CheckingAccounts.Count()).ToString().PadLeft(10, '0');
            var checkinAccount = new CheckingAccount
            {
                AccountNumber = accountNumber,
                FirstName = firstName,
                LastName = lastName,
                Balance = initialBalance,
                ApplicationUserId = userId
            };
            db.CheckingAccounts.Add(checkinAccount);
            db.SaveChanges();
        }

        public void UpdateBalance(int checkingAccountId)
        {
            var checkingAccount = db.CheckingAccounts.Where(c => c.Id == checkingAccountId).First();
            checkingAccount.Balance = db.Transactions.Where(t => t.CheckingAccountId == checkingAccountId).Sum(t => t.Amount);
            db.SaveChanges();
        }
    }
}