using MediatR;

namespace Workbit.Application.Features.Auth.Register
{
    public record RegisterCommand
        (Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string Role,
        string Password,
        string Token,
        DateTime Expires)
        : IRequest<RegisterResult>
    { }
}
