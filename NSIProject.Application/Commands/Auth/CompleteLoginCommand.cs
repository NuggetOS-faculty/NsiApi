using MediatR;
using NSIProject.Application.Common.Dto.Auth;
using NSIProject.Application.Common.Interfaces;

namespace NSIProject.Application.Commands.Auth;

public record CompleteLoginCommand(string ValidationToken) : IRequest<CompleteLoginResponseDto>;

public class CompleteLoginHandler
    (IAuthService authService) : IRequestHandler<CompleteLoginCommand, CompleteLoginResponseDto>
{
    public async Task<CompleteLoginResponseDto> Handle(CompleteLoginCommand request,
        CancellationToken cancellationToken) => await authService.CompleteLoginAsync(request.ValidationToken);
}