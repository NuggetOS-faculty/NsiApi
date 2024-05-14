using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSIProject.Application.Common.Dto.Auth;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Infrastructure.Configuration;
using NSIProject.Infrastructure.Identity;

namespace NSIProject.Infrastructure.Services;

public class AuthService(ApplicationUserManager userManager, IOptions<JwtConfiguration> jwtOptions) : IAuthService
{
    private readonly JwtConfiguration _jwtConfiguration = jwtOptions.Value;
    private const string Purpose = "passwordless-auth";
    private const string Provider = "PasswordlessLoginTokenProvider";

    public async Task<BeginLoginResponseDto> BeginLoginAsync(string emailAddress)
    {
        var normalizedEmail = emailAddress.ToLower();
        var user = userManager.Users.FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
        string? validationToken = null;

        if (user == null)
            return new BeginLoginResponseDto(validationToken);

        var token = await userManager.GenerateUserTokenAsync(user,
            Provider,
            Purpose);
        var bytes = Encoding.UTF8.GetBytes($"{token}:{emailAddress}");
        validationToken = Convert.ToBase64String(bytes);

        //todo :: send email with this validation token
        return new BeginLoginResponseDto(validationToken);
    }

    public async Task<CompleteLoginResponseDto> CompleteLoginAsync(string validationToken)
    {
        var (userToken, emailAddress) = ExtractValidationToken(validationToken);
        var normalizedEmail = emailAddress.ToLower();
        var user = userManager.Users.FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);

        if (user is not null)
        {
            var isValid = await userManager.VerifyUserTokenAsync(user,
                Provider,
                Purpose,
                userToken);

            if (!isValid)
                return new CompleteLoginResponseDto();

            await userManager.UpdateSecurityStampAsync(user);

            var authClaims = new List<Claim>();
            var roles = new List<string>();

            var rolesFromDb = await userManager.GetRolesAsync(user);

            foreach (var roleFromDb in rolesFromDb)
            {
                roles.Add(roleFromDb);
                authClaims.Add(new Claim(ClaimTypes.Role,
                    roleFromDb));
            }

            authClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            //
            // authClaims.AddRange(user.Claims.Select(item => new Claim(item.Type,
            //     item.Value)));

            return new CompleteLoginResponseDto(user.Email,
                roles,
                new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(authClaims)));
        }

        return new CompleteLoginResponseDto();
    }

    private static Tuple<string, string> ExtractValidationToken(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        var tokenDetails = Encoding.UTF8.GetString(base64EncodedBytes);
        var separatorIndex = tokenDetails.IndexOf(':');

        return new Tuple<string, string>(tokenDetails[..separatorIndex],
            tokenDetails[(separatorIndex + 1)..]);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!));

        var token = new JwtSecurityToken(issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            expires: DateTime.Now.AddMinutes(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey,
                SecurityAlgorithms.HmacSha256));

        return token;
    }
}