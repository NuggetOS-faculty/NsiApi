using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NSIProject;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
    private ISender _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

    public BaseController()
    {
    }
}