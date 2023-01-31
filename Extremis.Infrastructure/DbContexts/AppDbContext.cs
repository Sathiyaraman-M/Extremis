using Duende.IdentityServer.EntityFramework.Options;
using Extremis.Proposals;
using Extremis.Servers;
using Extremis.Users;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Extremis.DbContexts;

public class AppDbContext: ApiAuthorizationDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions, IOptions<OperationalStoreOptions> operationalStoreOptions) 
        : base(dbContextOptions, operationalStoreOptions)
    {
        
    }
    
    public DbSet<Server> Servers { get; set; }
    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<Reciprocation> Reciprocations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .ToTable("Users", "Identity")
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<IdentityRole>()
            .ToTable("Roles", "Identity");

        builder.Entity<IdentityUserRole<string>>()
            .ToTable("UserRoles", "Identity");

        builder.Entity<IdentityUserRole<string>>()
            .ToTable("UserClaims", "Identity");

        builder.Entity<IdentityUserLogin<string>>()
            .ToTable("UserLogins", "Identity");

        builder.Entity<IdentityRoleClaim<string>>()
            .ToTable("RoleClaims", "Identity");

        builder.Entity<IdentityUserToken<string>>()
            .ToTable("UserTokens", "Identity");

        builder.Entity<Server>()
            .ToTable("Servers", "dbo")
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}