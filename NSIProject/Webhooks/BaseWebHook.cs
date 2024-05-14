using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NSIProject.Webhooks;

[ApiController]
[Route("webhook/[controller]/[action]")]
public class BaseWebHook : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}