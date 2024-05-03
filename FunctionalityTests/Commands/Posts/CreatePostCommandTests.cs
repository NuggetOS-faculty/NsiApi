using System.Net;
using System.Text;
using Newtonsoft.Json;
using NSIProject.Application.Common.Dto.Post;

namespace UnitTests.Commands.Posts;

public class CreatePostCommandTests : BaseTest
{
    public CreatePostCommandTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        // any additional setup
    }

    [Fact]
    public async Task CreatePost_WhenCalled_ReturnsUnauthorized()
    {
        // Arrange
        var postDto = new CreatePostDto
        (
            "Test title",
            "Test content"
        );
        var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/api/Post/CreatePost", content, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreatePost_WhenCalled_ReturnsBadRequest()
    {
        // Arrange
        var postDto = new CreatePostDto
        (
            "",
            ""
        );
        var user = await CreateTestUser();
        var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
        // Add headers
        SetHeaders(user.Email!, user.PasswordHash!);

        var response = await Client.PostAsync("/api/Post/CreatePost", content, CancellationToken.None);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // Act
    }

    [Fact]
    public async Task CreatePost_WhenCalled_ReturnsPost()
    {
        var user = await CreateTestUser();
        await NsiDbContext.Users.AddAsync(user);
        await NsiDbContext.SaveChangesAsync(new CancellationToken());
        // Arrange
        var postDto = new CreatePostDto
        (
            "Test title",
            "Test content"
        );
        var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");

        // Act

        // Add headers
        SetHeaders(user.Email!, user.PasswordHash!);

        var response = await Client.PostAsync("/api/Post/CreatePost", content, CancellationToken.None);
        // assert 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseString);
    }
}