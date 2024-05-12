using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSIProject.Application.Commands.Auth;

namespace NSIProject.Controllers;

public class AuthController : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> BeginLogin(BeginLoginCommand command) => Ok(await Mediator.Send(command));

    [AllowAnonymous]
    [HttpGet("{validationToken}/CompleteLogin")]
    public async Task<ActionResult> CompleteLogin([FromRoute] string validationToken) =>
        Ok(await Mediator.Send(new CompleteLoginCommand(validationToken)));
}