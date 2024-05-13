using MediatR;
using NSIProject.Application.Common.Interfaces;
using Guid = System.Guid;

namespace NSIProject.Application.Commands.Post;

public record DeletePostCommand(Guid Id) : IRequest;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IPostService _postService;

    public DeletePostCommandHandler(IPostService postService)
    {
        _postService = postService;
    }

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        await _postService.DeleteAsync(request.Id, cancellationToken);
    }
}