using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NSIProject;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}