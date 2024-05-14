using NSIProject.Application.Common.Dto.Post;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Domain.Entities;

namespace NSIProject.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly INsiDbContext _dbContext;


    public PostService(INsiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(CreatePostDto post, ApplicationUser user, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        var newPost = new Post
        {
            Id = guid,
            Title = post.Title,
            Content = post.Content,
            User = user
        };

        _dbContext.Posts.Add(newPost);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newPost.Id;
    }

    public async Task<Post?> GetPostById(Guid id)
    {
        return await _dbContext.Posts.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.FindAsync(id);
        if (post == null) return;

        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}