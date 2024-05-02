using MediatR;
using NSIProject.Application.Common.Dto.Post;
using NSIProject.Application.Common.Exceptions;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Application.Common.Mappers;

namespace NSIProject.Application.Queries.Post;

public record GetPostByIdQuery(Guid Id) : IRequest<PostDetailsDto>;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDetailsDto>
{
    private readonly IPostService _postService;

    public GetPostByIdQueryHandler(IPostService postService)
    {
        _postService = postService;
    }

    public async Task<PostDetailsDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postService.GetPostById(request.Id);
        if (post == null) throw new NotFoundException("Post not found");
        return post.MapToPostDetailsDto();
    }
}