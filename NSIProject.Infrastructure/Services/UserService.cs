using NSIProject.Application.Common.Interfaces;
using NSIProject.Domain.Entities;
using NSIProject.Infrastructure.Identity;

namespace NSIProject.Infrastructure.Services;

// these can be swapped to application user manager but here are just a few examples
public class UserService : IUserService
{
    private readonly INsiDbContext _context;
    private readonly ApplicationUserManager _userManager;

    public UserService(INsiDbContext context, ApplicationUserManager userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> FindById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user;
    }

    public Task<ApplicationUser?> FindByUserName(string userName)
    {
        // leave for debugging purposes var users = _context.Users.ToList();

        var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
        return Task.FromResult(user);
    }
}