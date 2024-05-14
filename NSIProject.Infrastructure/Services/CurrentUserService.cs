using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NSIProject.Application.Common.Interfaces;

namespace NSIProject.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public string? UserId { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public List<string> Roles { get; private set; } = new();


    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        UserId = GetClaimValue(ClaimTypes.NameIdentifier);

        var identity = httpContextAccessor.HttpContext?.User.Identity;

        if (identity is not null && identity.IsAuthenticated)
        {
            var roles = GetRoleClaimValues();

            if (roles?.Count > 0)
            {
                Roles.AddRange(roles);
            }

            Email = GetClaimValue(ClaimTypes.Email);
            FirstName = GetClaimValue(ClaimTypes.GivenName);
            LastName = GetClaimValue(ClaimTypes.Surname);
        }
    }

    private string? GetClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value;

    private List<string>? GetRoleClaimValues() => _httpContextAccessor.HttpContext?.User.Claims
        .Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
}