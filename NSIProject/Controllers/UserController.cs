using Microsoft.AspNetCore.Mvc;
using NSIProject.Application.Queries.User;

namespace NSIProject.Controllers;

public class UserController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUserDetails([FromQuery] string id)
    {
        return Ok(await Mediator.Send(new GetUserByIdQuery(id)));
    }
}