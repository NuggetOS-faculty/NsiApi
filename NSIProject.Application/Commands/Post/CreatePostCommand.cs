using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using NSIProject.Application.Common.Dto.Post;
using NSIProject.Application.Common.Exceptions;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Domain.Entities;

namespace NSIProject.Application.Commands.Post;

public record CreatePostCommand(CreatePostDto Request) : IRequest<string>;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, string>

{
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;


    public CreatePostCommandHandler(IPostService postService,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _currentUserService = currentUserService;
        _userService = userService;
        _postService = postService;
    }

    public async Task<string> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var username = _currentUserService.Email;
        if (username == null) throw new NotFoundException("User not found");

        var user = await _userService.FindByUserName(username);
        if (user == null) throw new NotFoundException("User not found");
        var result = await _postService.CreateAsync(request.Request, user, cancellationToken);
        return result.ToString();
    }
}