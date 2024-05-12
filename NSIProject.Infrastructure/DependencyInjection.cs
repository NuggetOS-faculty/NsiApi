using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Domain.Entities;
using NSIProject.Infrastructure.Configuration;
using NSIProject.Infrastructure.Contexts;
using NSIProject.Infrastructure.Extensions.Auth;
using NSIProject.Infrastructure.Identity;
using NSIProject.Infrastructure.Services;

namespace NSIProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = new PostgresDbConfiguration();
        configuration.GetSection("PostgresDbConfiguration").Bind(dbConfiguration);
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test")
        {
            services.AddDbContext<NsiDbContext>(options =>
                options.UseNpgsql(dbConfiguration.ConnectionString,
                    x => x.MigrationsAssembly(typeof(NsiDbContext).Assembly.FullName)));
        }

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddUserManager<ApplicationUserManager>()
            .AddEntityFrameworkStores<NsiDbContext>()
            .AddDefaultTokenProviders()
            .AddPasswordlessLoginTokenProvider();
        services.AddScoped<INsiDbContext>(provider => provider.GetRequiredService<NsiDbContext>());

        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
        return services;
    }
}