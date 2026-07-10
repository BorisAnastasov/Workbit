using MediatR;
using Workbit.Application.Common.Models;
using Workbit.Application.Interfaces;

namespace Workbit.Application.Features.Auth.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResult>
    {
        private readonly ITokenService tokenService;

        public RefreshTokenCommandHandler(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public async Task<TokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await tokenService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
        }
    }
}
