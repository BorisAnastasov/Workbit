using MediatR;
using Workbit.Application.Common.Models;

namespace Workbit.Application.Features.Auth.Refresh
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResult>;
}
