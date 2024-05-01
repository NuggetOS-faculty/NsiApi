using Microsoft.EntityFrameworkCore;
using NSIProject.Domain.Entities;

namespace NSIProject.Application.Common.Interfaces;

public interface INsiDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<Post> Posts { get; }

    DbSet<ApplicationUser> Users { get; }
}