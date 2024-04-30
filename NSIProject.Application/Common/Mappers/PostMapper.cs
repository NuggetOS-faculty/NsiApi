using NSIProject.Application.Common.Dto.Post;
using Riok.Mapperly.Abstractions;

namespace NSIProject.Application.Common.Mappers;

[Mapper]
public static partial class PostMapper
{
    public static PostDetailsDto MapToPostDetailsDto(this Domain.Entities.Post post)
    {
        return new PostDetailsDto(post.Title, post.Content, post.User.FirstName!,
            post.User.LastName!, post.Id);
    }
}