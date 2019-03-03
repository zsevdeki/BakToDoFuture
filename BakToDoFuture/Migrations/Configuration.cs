namespace BakToDoFuture.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using BakToDoFuture.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<BakToDoFuture.Models.FutureDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BakToDoFuture.Models.FutureDbContext context)
        {
            if (!context.Users.Any())
            {
                
                var store = new UserStore<ApplicationUser>(context);
                var manager = new ApplicationUserManager(store);
                var user = new Models.ApplicationUser() { Email = "onur@gmail.com", UserName = "onur@gmail.com" };
                Task<IdentityResult> task = Task.Run(() => manager.CreateAsync(user, "Onur123+"));

                // Will block until the task is completed...
                var result = task.Result;


                context.SaveChanges();
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
