using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RCM.Model;
using System;

namespace RCM.Data
{
    public class RCMContext : IdentityDbContext<User>
    {
        public RCMContext() : base((new DbContextOptionsBuilder())
           .UseLazyLoadingProxies()
           //.UseSqlServer(@"Server=.;Database=CRMDB;user id=sa;password=zaq@123;Trusted_Connection=True;")
           .UseSqlServer(@"Server=.;Database=RCM;user id=sa;password=zaq@123;Trusted_Connection=True;Integrated Security=false;")
           .Options)
        {

        }

        //public DbSet<User> Users{ get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}