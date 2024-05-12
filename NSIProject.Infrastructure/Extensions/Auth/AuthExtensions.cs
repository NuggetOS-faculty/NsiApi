using Microsoft.AspNetCore.Identity;
using NSIProject.Infrastructure.Auth.Providers;

namespace NSIProject.Infrastructure.Extensions.Auth;

public static class AuthExtensions
{
    public static IdentityBuilder AddPasswordlessLoginTokenProvider(this IdentityBuilder builder)
    {
        var userType = builder.UserType;
        var provider = typeof(PasswordlessLoginTokenProvider<>).MakeGenericType(userType);
        return builder.AddTokenProvider("PasswordlessLoginTokenProvider", provider);
    }
}