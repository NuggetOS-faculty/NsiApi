using Microsoft.AspNetCore.Authentication;
using NSIProject.Application.Configuration;

namespace NSIProject.Auth.Options;

public class HeaderBasicAuthSchemeOptions : AuthenticationSchemeOptions
{
    
    public string Username { get; set; } = "X-Nsi-Username";
    public string Password { get; set; } = "X-Nsi-Password";

    public IEnumerable<UserBasicConfiguration> Users { get; init; } = Array.Empty<UserBasicConfiguration>();
}