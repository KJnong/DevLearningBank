using DL_Bank.Models;
using DL_Bank.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DL_Bank.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private IApplicationDbContext db;

        public TransactionController()
        {
            db = new ApplicationDbContext();        
        }

        public TransactionController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        // POST: Transaction/Deposit
        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Withdrawal(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdrawal(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Amount *= (-1);

                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult QuickCash(int checkingAccountId, decimal amount)
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuickCash(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Transfare(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfare(TransfareViewModel transfareViewModel)
        {
            var sender = new Transaction { Amount = transfareViewModel.Amount*-1, CheckingAccountId = transfareViewModel.CheckingAccountId };
            var receiverId = db.CheckingAccounts.Where(r => r.AccountNumber == transfareViewModel.AccountNumber).First().Id;

            var receiver = new Transaction { CheckingAccountId = receiverId, Amount = transfareViewModel.Amount };

            if (ModelState.IsValid)
            {
                db.Transactions.Add(sender);
                db.SaveChanges();
                db.Transactions.Add(receiver);
                db.SaveChanges();

                var service = new CheckingAccountService(db);
                service.UpdateBalance(sender.CheckingAccountId);
                var service2 = new CheckingAccountService(db);
                service2.UpdateBalance(receiver.CheckingAccountId);

                


                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}