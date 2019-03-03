using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BakToDoFuture.Models
{
    public class FutureDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        public FutureDbContext() : base("Bak1", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FutureDbContext,Migrations.Configuration>("Bak1"));
        }
        public static FutureDbContext Create()
        {
            return new FutureDbContext();
        }

        //public System.Data.Entity.DbSet<BakToDoFuture.Models.ApplicationUser> ApplicationUsers { get; set; }
        
    }
}