using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RCM.Model;

namespace RCM.Data
{
    public class RCMContext : IdentityDbContext<User>
    {
        public RCMContext() : base((new DbContextOptionsBuilder())
           .UseLazyLoadingProxies()
           .UseSqlServer(@"Server=202.78.227.91;Database=RCM;user id=sa;password=zaq@123;Trusted_Connection=True;Integrated Security=false;", x => x.EnableRetryOnFailure())
           .Options)
        {

        }

        public DbSet<AssignedCollector> AssignedCollectors { get; set; }
        public DbSet<CollectionProgress> CollectionProgresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HubUserConnection> HubUserConnections { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileMessageForm> ProfileMessageForms { get; set; }
        public DbSet<ProfileStage> ProfileStages { get; set; }
        public DbSet<ProfileStageAction> ProfileStageActions { get; set; }
        public DbSet<ProgressMessageForm> ProgressMessageForms { get; set; }
        public DbSet<ProgressStage> ProgressStages { get; set; }
        public DbSet<ProgressStageAction> ProgressStageActions { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<FirebaseToken> FirebaseTokens { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<Receivable>()
            .HasOne(a => a.CollectionProgress)
            .WithOne(b => b.Receivable)
            .HasForeignKey<CollectionProgress>(b => b.ReceivableId);

            base.OnModelCreating(builder);
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
