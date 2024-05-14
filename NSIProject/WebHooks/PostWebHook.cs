using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSIProject.Application.Commands.Post;
using NSIProject.Application.Common.Dto.Post;
using NSIProject.Auth.Constants;

namespace NSIProject.WebHooks;

public class PostWebHook : BaseWebHook
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = nameof(AuthConstants.HeaderDbAuthSchema))]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
    {
        return Ok(await Mediator.Send(new CreatePostCommand(postDto)));
    }
}