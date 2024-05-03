using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Infrastructure.Contexts;

namespace UnitTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    // what the whole deal with this is 
    // similarly to bootstraping the application we're bootstraping env for testing 
    // replacing the strategy when it comes to persisting data into in memory db
    // we could replace services and add mock to it too
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<NsiDbContext>();

            var dbName = Guid.NewGuid()
                .ToString();

            services.AddDbContext<NsiDbContext>(options => { options.UseInMemoryDatabase(dbName); });
        });
    }
}