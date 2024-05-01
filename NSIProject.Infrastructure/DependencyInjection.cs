using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Infrastructure.Configuration;
using NSIProject.Infrastructure.Contexts;
using NSIProject.Infrastructure.Services;

namespace NSIProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = new PostgresDbConfiguration();
        configuration.GetSection("PostgresDbConfiguration").Bind(dbConfiguration);
        services.AddDbContext<NsiDbContext>(options => options.UseNpgsql(dbConfiguration.ConnectionString,
            x => x.MigrationsAssembly(typeof(NsiDbContext).Assembly.FullName)));
        services.AddScoped<INsiDbContext>(provider => provider.GetService<NsiDbContext>()!);
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}