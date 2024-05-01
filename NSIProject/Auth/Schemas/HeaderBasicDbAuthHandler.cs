using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Auth.Constants;
using NSIProject.Auth.Options;

namespace NSIProject.Auth.Schemas;

public class HeaderBasicDbAuthHandler : AuthenticationHandler<HeaderBasicDbAuthSchemeOptions>
{
    private readonly IUserService _userService;


    public HeaderBasicDbAuthHandler(IOptionsMonitor<HeaderBasicDbAuthSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IUserService userService) : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    public HeaderBasicDbAuthHandler(
        IOptionsMonitor<HeaderBasicDbAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IUserService userService) : base(options, logger, encoder)
    {
        _userService = userService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var username = Context.Request.Headers[Options.Username].FirstOrDefault() ??
                           throw new Exception("Missing username");
            var password = Context.Request.Headers[Options.Password].FirstOrDefault() ??
                           throw new Exception("Missing password");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing username or password"));
            }

            var user = _userService.FindByUserName(username).Result;
            if (user == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
            }

            if (user.PasswordHash != password)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,
                    username)
            };

            var authResult = AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(
                    claims, AuthConstants.HeaderDbAuthSchema)),
                AuthConstants.HeaderDbAuthSchema));

            return Task.FromResult(authResult);
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex.Message));
        }
    }
}