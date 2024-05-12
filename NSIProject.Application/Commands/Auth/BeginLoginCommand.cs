using MediatR;
using NSIProject.Application.Common.Dto.Auth;
using NSIProject.Application.Common.Interfaces;

namespace NSIProject.Application.Commands.Auth;

public record BeginLoginCommand(string EmailAddress) : IRequest<BeginLoginResponseDto>;

public class BeginLoginHandler(IAuthService authService) : IRequestHandler<BeginLoginCommand, BeginLoginResponseDto>
{
    public async Task<BeginLoginResponseDto> Handle(BeginLoginCommand request, CancellationToken cancellationToken) =>
        await authService.BeginLoginAsync(request.EmailAddress);
}