using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Workbit.Application.Exceptions;
using Workbit.Application.Features.Auth.Shared;
using Workbit.Application.Interfaces;
using Workbit.Domain.Entities.Account;

namespace Workbit.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            Console.WriteLine(request.Email);
            if (user == null) throw new UnauthorizedException("Invalid email or password.");

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!signInResult.Succeeded) throw new UnauthorizedException("Invalid email or password.");

            var roles = await userManager.GetRolesAsync(user);

            var tokenResult = await tokenService.GenerateTokenAsync(user, cancellationToken);

            var result = mapper.Map<AuthResult>(user);

            result.Roles = roles.ToList();
            result.Token = tokenResult.Token;
            result.Expires = tokenResult.Expires;
            result.RefreshToken = tokenResult.RefreshToken;
            result.RefreshTokenExpires = tokenResult.RefreshTokenExpires;

            return result;
        }
    }
}
