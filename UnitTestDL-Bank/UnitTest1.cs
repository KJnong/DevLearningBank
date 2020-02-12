using System;
using DL_Bank.Controllers;
using DL_Bank.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDL_Bank
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BalanceIsCorrectAfterDeposit()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.CheckingAccounts = new FakeDbSet<CheckingAccount>();

            var checkingAccount = new CheckingAccount { Id = 1, AccountNumber = "0123TEST", Balance = 0 };
            fakeDb.CheckingAccounts.Add(checkingAccount);

            fakeDb.Transactions = new FakeDbSet<Transaction>();
            var transactionController = new TransactionController(fakeDb);
            var deposit = new Transaction { CheckingAccountId = 1, Amount = 100 };
            transactionController.Deposit(deposit);

            Assert.AreEqual(100, checkingAccount.Balance);
        }
    } 
}
