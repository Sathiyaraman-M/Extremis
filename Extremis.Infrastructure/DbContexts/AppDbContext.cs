using Duende.IdentityServer.EntityFramework.Options;
using Extremis.ProjectChats;
using Extremis.Projects;
using Extremis.Proposals;
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
    
    //public DbSet<Server> Servers { get; set; }
    public DbSet<Project> Projects { get; set; }
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
            .ToTable("Users", "dbo")
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<IdentityRole>()
            .ToTable("Roles", "dbo");

        builder.Entity<IdentityUserRole<string>>()
            .ToTable("UserRoles");
        
        builder.Entity<IdentityUserClaim<string>>()
            .ToTable("UserClaims");

        builder.Entity<IdentityUserLogin<string>>()
            .ToTable("UserLogins");

        builder.Entity<IdentityRoleClaim<string>>()
            .ToTable("RoleClaims");

        builder.Entity<IdentityUserToken<string>>()
            .ToTable("UserTokens");

        builder.Entity<Project>()
            .ToTable("Projects")
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<Proposal>()
            .ToTable("Proposals")
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<Reciprocation>()
            .ToTable("Reciprocations")
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<ChatMessage>(entity =>
        {
            entity.HasOne(x => x.FromUser)
                .WithMany(x => x.ChatMessagesFromUsers)
                .HasForeignKey(x => x.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            entity.HasOne(x => x.ToUser)
                .WithMany(x => x.ChatMessagesToUsers)
                .HasForeignKey(x => x.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}