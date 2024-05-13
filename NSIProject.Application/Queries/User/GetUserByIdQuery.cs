using MediatR;
using NSIProject.Application.Common.Dto.User;
using NSIProject.Application.Common.Exceptions;
using NSIProject.Application.Common.Interfaces;
using NSIProject.Application.Common.Mappers;

namespace NSIProject.Application.Queries.User;

public record GetUserByIdQuery(string Id) : IRequest<UserDetailsDto>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindById(request.Id);
        if (user == null) throw new NotFoundException("User not found");
        return user.MapToUserDetailsDto();
    }
}