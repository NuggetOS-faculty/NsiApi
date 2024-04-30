using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Domain.Entities;

namespace NSIProject.Infrastructure.Contexts;

public class NsiDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    string,
    IdentityUserClaim<string>,
    ApplicationUserRole,
    IdentityUserLogin<string>,
    IdentityRoleClaim<string>,
    IdentityUserToken<string>>, INsiDbContext
{
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = base.SaveChangesAsync(cancellationToken);
        return result;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=root;Database=nsi");
}