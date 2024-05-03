using BaseTests.Builders.User;
using Microsoft.Extensions.DependencyInjection;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Domain.Entities;
using NSIProject.Infrastructure.Contexts;

namespace UnitTests;

public abstract class BaseTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    protected readonly HttpClient Client;
    protected readonly INsiDbContext NsiDbContext;


    protected BaseTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Client = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        NsiDbContext = scope.ServiceProvider.GetRequiredService<NsiDbContext>();
    }

    protected void SetHeaders(string username, string password)
    {
        Client.DefaultRequestHeaders.Add("x-nsi-username", username);
        Client.DefaultRequestHeaders.Add("x-nsi-password", password);
    }

    protected async Task<ApplicationUser> CreateTestUser()
    {
        var userId = Guid.NewGuid().ToString();
        /*var user = new ApplicationUser
        {
            Id = userId,
            UserName = "test@gmail.com",
            NormalizedUserName = "test@gmail.com",
            Email = "test@gmail.com",
            NormalizedEmail = "Test@gmail.com",
            PasswordHash = "test1",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = new Guid().ToString("D"),
            FirstName = "Test",
            LastName = "Test",
            ConcurrencyStamp = "c188a435-cfc8-45fd-836c-9a18bb9de405",
            AccessFailedCount = 0
        }; */
        var user = new ApplicationUserBuilder().WithId(userId).WithUserName("test@gmail.com")
            .WithEmail("test@gmail.com").WithPasswordHash("test1").WithEmailConfirmed(true)
            .WithPhoneNumberConfirmed(true).WithSecurityStamp(new Guid().ToString("D")).WithFirstName("Test")
            .WithLastName("Test").WithConcurrencyStamp("c188a435-cfc8-45fd-836c-9a18bb9de405").WithAccessFailedCount(0)
            .Build();

        await NsiDbContext.Users.AddAsync(user);
        await NsiDbContext.SaveChangesAsync(new CancellationToken());
        var createdUser = await NsiDbContext.Users.FindAsync(user.Id);
        Assert.NotNull(createdUser);
        return createdUser;
    }
}