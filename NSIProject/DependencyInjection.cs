using NSIProject.Auth.Constants;
using NSIProject.Auth.Options;
using NSIProject.Auth.Schemas;

namespace NSIProject;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)

    {
        services.AddAuthentication()
            .AddScheme<HeaderBasicAuthSchemeOptions, HeaderBasicAuthHandler>(AuthConstants.HeaderBasicAuthSchema,
                options => configuration.GetSection("Auth:Header").Bind(options));
        return services;
    }
}