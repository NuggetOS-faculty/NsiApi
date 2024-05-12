using System.Text;
using BaseTests.Builders.Auth;
using MediatR;
using Newtonsoft.Json;
using NSIProject.Application.Commands.Auth;
using NSIProject.Application.Common.Dto.Auth;

namespace UnitTests.Commands.Auth;

public class BeginLoginCommandTests : BaseTest
{
    public BeginLoginCommandTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task BeginLoginCommand_WhenCalledWithNonExistingUser_ReturnsNull()
    {
        // Arrange
        var email = "someemail@gmail.com";
        var command = new BeginLoginCommandBuilder();
        var beginLoginCommand = command.WithEmail(email).Build();

        // Act
        var content = new StringContent(JsonConvert.SerializeObject(beginLoginCommand), Encoding.UTF8,
            "application/json");
        var response = await Client.PostAsync("/api/Auth/BeginLogin", content, CancellationToken.None);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<BeginLoginResponseDto>(responseString);
        Assert.NotNull(responseDto);
        Assert.Null(responseDto.ValidationToken);
    }

    [Fact]
    public async Task BeginLoginCommand_WhenCalledWithExistingUser_ReturnsValidationToken()
    {
        // Arrange
        var user = await this.CreateTestUser();
        var command = new BeginLoginCommandBuilder();
        var beginLoginCommand = command.WithEmail(user.Email).Build();

        // Act
        var content = new StringContent(JsonConvert.SerializeObject(beginLoginCommand), Encoding.UTF8,
            "application/json");
        var response = await Client.PostAsync("/api/Auth/BeginLogin", content, CancellationToken.None);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<BeginLoginResponseDto>(responseString);
        Assert.NotNull(responseDto);
        Assert.NotNull(responseDto.ValidationToken);
    }
}