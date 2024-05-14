using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NSIProject.WebHooks;

[ApiController]
[Route("webhook/[controller]/[action]")]
public class BaseWebHook : BaseController
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}