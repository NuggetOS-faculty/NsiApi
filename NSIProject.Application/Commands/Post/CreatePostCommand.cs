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
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public CreatePostCommandHandler(IPostService postService, IHttpContextAccessor httpContextAccessor,
        IUserService userService)
    {
        _userService = userService;
        _postService = postService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var ctx = _httpContextAccessor.HttpContext.User;
        var username = ctx.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (username == null) throw new NotFoundException("User not found");

        var user = await _userService.FindByUserName(username.Value);
        if (user == null) throw new NotFoundException("User not found");
        var result = await _postService.CreateAsync(request.Request, user, cancellationToken);
        return result.ToString();
    }
}