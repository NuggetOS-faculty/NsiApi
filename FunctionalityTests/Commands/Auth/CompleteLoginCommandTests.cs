using System.Net;
using System.Text;
using BaseTests.Builders.Auth;
using Newtonsoft.Json;
using NSIProject.Application.Common.Dto.Auth;

namespace UnitTests.Commands.Auth;

public class CompleteLoginCommandTests : BaseTest
{
    public CompleteLoginCommandTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task CompleteLoginCommand_WhenCalledWithInvalidToken_ReturnsBadRequest()
    {
        // Arrange
        var token = "InvalidToken";
        var response = await Client.GetAsync($"/api/Auth/CompleteLogin/{token}/CompleteLogin", CancellationToken.None);

        var responseString = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<CompleteLoginResponseDto>(responseString);

        // Assert
        Assert.NotNull(responseDto);
        Assert.Null(responseDto.JwtToken);
        Assert.Null(responseDto.EmailAddress);
        Assert.Null(responseDto.Roles);
    }

    [Fact]
    public async Task CompleteLoginCommand_WhenCalledWithValidToken_ReturnsJwtToken()
    {
        // Arrange
        var user = await this.CreateTestUser();
        var beginLoginCommand = new BeginLoginCommandBuilder().WithEmail(user.Email).Build();
        var content = new StringContent(JsonConvert.SerializeObject(beginLoginCommand), Encoding.UTF8,
            "application/json");
        var response = await Client.PostAsync("/api/Auth/BeginLogin", content, CancellationToken.None);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<BeginLoginResponseDto>(responseString);

        var token = responseDto.ValidationToken;
        var authorizationResponse =
            await Client.GetAsync($"/api/Auth/CompleteLogin/{token}/CompleteLogin", CancellationToken.None);

        var authorizationResponseString = await authorizationResponse.Content.ReadAsStringAsync();
        var authorizationResponseDto =
            JsonConvert.DeserializeObject<CompleteLoginResponseDto>(authorizationResponseString);


        // Assert
        Assert.NotNull(authorizationResponseDto);
        Assert.NotNull(authorizationResponseDto.JwtToken);
        Assert.NotNull(authorizationResponseDto.EmailAddress);
        Assert.NotNull(authorizationResponseDto.Roles);
    }
}