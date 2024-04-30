using Microsoft.AspNetCore.Mvc;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Application.Common.Mappers;

namespace NSIProject.Controllers;

public class PostBaseController : ControllerBase
{
    private readonly INsiDbContext _dbContext;

    public PostBaseController(INsiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult GetPostDetails(Guid id)
    {
        var post = _dbContext.Posts.Find(id);
        if (post == null)
        {
            return NotFound();
        }

        var dto = post.MapToPostDetailsDto();
        return Ok(dto);
    }
}