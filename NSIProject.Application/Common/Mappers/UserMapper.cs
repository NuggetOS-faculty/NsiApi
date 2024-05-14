using NSIProject.Application.Common.Dto.Post;
using NSIProject.Application.Common.Dto.User;
using Riok.Mapperly.Abstractions;

namespace NSIProject.Application.Common.Mappers;

[Mapper]
public static partial class UserMapper
{
    public static UserDetailsDto MapToUserDetailsDto(this Domain.Entities.ApplicationUser user)
    {
        return new UserDetailsDto(user.Id, user.UserName, user.Email, user.FirstName!,
            user.LastName, user.PhoneNumber);
    }
}