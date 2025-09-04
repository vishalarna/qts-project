using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QTD2.Data.Mapping.Authentication;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Data
{
    public class QTDAuthenticationContext : IdentityDbContext<AppUser>
    {
        public QTDAuthenticationContext(DbContextOptions<QTDAuthenticationContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Ignore<AppClaim>();

            builder.ApplyConfiguration(new ClientMap());
            builder.ApplyConfiguration(new InstanceMap());
            builder.ApplyConfiguration(new InstanceSettingMap());
            builder.ApplyConfiguration(new EventLogMap());

            builder.ApplyConfiguration(new IdentityProviderMap());
            builder.ApplyConfiguration(new InstanceIdentityProviderLinkMap());
            builder.ApplyConfiguration(new AdminMessageAuthMap());
            
            builder.ApplyConfiguration(new AuthenticationSettingMap());
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Instance> Instances { get; set; }

        public DbSet<InstanceSetting> InstanceSettings { get; set; }

        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<IdentityProvider> IdentityProviders { get; set; }
        public DbSet<InstanceIdentityProviderLink> Instance_IdentityProvider_Links { get; set; }
        public DbSet<AdminMessageAuth> AdminMessagesAuth { get; set; }
        public DbSet<AuthenticationSetting> AuthenticationSettings { get; set; }
    }
}
