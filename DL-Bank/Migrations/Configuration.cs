namespace DL_Bank.Migrations
{
    using DL_Bank.Models;
    using DL_Bank.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DL_Bank.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "DL_Bank.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "vadmin@dlatm.com"))
            {
                var user = new ApplicationUser { UserName = "vadmin@dlatm.com", Email = "vadmin@dlatm.com" };
                userManager.Create(user, "Martin123.");

                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("Vuyani", "Shabangu", user.Id, 50000);

                var admin = new IdentityRole { Name = "Admin" };
                context.Roles.AddOrUpdate(r => r.Name, admin);
                context.SaveChanges();

                userManager.AddToRole(user.Id, "Admin");

                

            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
