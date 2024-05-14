﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSIProject.Application.Commands.Post;
using NSIProject.Application.Common.Dto.Post;
using NSIProject.Auth.Constants;
using NSIProject.Auth.Options;

namespace NSIProject.Controllers;

public class PostController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetPostDetails([FromQuery] Guid id)
    {
        return Ok();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
    {
        return Ok(await Mediator.Send(new CreatePostCommand(postDto)));
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePost([FromQuery] Guid id)
    {
        await Mediator.Send(new DeletePostCommand(id));
        return Ok();
    }
}