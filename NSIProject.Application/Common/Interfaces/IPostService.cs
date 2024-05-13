using NSIProject.Application.Common.Dto.Post;
using NSIProject.Domain.Entities;

namespace NSIProject.Application.Common.Interfaces;

public interface IPostService
{
    Task<Guid> CreateAsync(CreatePostDto post, ApplicationUser user, CancellationToken cancellationToken);
    Task<Post?> GetPostById(Guid id);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
}