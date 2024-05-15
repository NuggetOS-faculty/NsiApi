using System.Net;
using System.Text;
using Newtonsoft.Json;
using NSIProject.Application.Common.Dto.Post;
using NSIProject.Domain.Entities;

namespace UnitTests.Commands.Posts;

public class CreatePostCommandTests : BaseTest, IDisposable
{
    private ApplicationUser User;

    public CreatePostCommandTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        // any additional setup
    }

    public async void Dispose()
    {
        if (User != null)
        {
            NsiDbContext.Users.Remove(User);
            await NsiDbContext.SaveChangesAsync(new CancellationToken());
        }
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
        var response = await Client.PostAsync("/api/Post/Creas", content, CancellationToken.None);

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
        this.User = await CreateTestUser();
        var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
        // Add headers
        SetHeaders(this.User.Email!, this.User.PasswordHash!);

        var response = await Client.PostAsync("/api/Post/CreatePost", content, CancellationToken.None);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // Act
    }

    [Fact]
    public async Task CreatePost_WhenCalled_ReturnsPost()
    {
        this.User = await CreateTestUser();
        // Arrange
        var postDto = new CreatePostDto
        (
            "Test title",
            "Test content"
        );
        var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");

        // Act

        // Add headers
        SetHeaders(this.User.Email!, this.User.PasswordHash!);

        var response = await Client.PostAsync("/api/Post/CreatePost", content, CancellationToken.None);
        // assert 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseString);
    }
}