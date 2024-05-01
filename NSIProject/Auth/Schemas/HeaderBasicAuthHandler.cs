using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using NSIProject.Auth.Constants;
using NSIProject.Auth.Options;

namespace NSIProject.Auth.Schemas;

public class HeaderBasicAuthHandler : AuthenticationHandler<HeaderBasicAuthSchemeOptions>

{
    [Obsolete]
    public HeaderBasicAuthHandler(IOptionsMonitor<HeaderBasicAuthSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public HeaderBasicAuthHandler(IOptionsMonitor<HeaderBasicAuthSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
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

            var user = Options.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
            }

            var authResult = AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(
                    user.Claims.Select(c => new Claim(c.Key, c.Value)), AuthConstants.HeaderBasicAuthSchema)),
                AuthConstants.HeaderBasicAuthSchema));

            return Task.FromResult(authResult);
        }
        catch (Exception ex)
        {
            return Task.FromResult(AuthenticateResult.Fail(ex.Message));
        }
    }
}