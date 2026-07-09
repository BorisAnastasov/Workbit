using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workbit.Application.Common.Models;
using Workbit.Application.Interfaces;
using Workbit.Domain.Entities;
using Workbit.Domain.Entities.Account;
using Workbit.Domain.Interfaces;

namespace Workbit.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings jwtSettings;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public TokenService(
            IOptions<JwtSettings> jwtOptions,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            jwtSettings = jwtOptions.Value;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TokenResult> GenerateTokenAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.GivenName, user.FirstName),
                new(ClaimTypes.Surname, user.LastName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(jwtSettings.ExpiredMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            await UpsertRefreshTokenAsync(user.Id, tokenString, expires, cancellationToken);

            return new TokenResult
            {
                UserId = user.Id,
                Token = tokenString,
                Expires = expires
            };
        }

        private async Task UpsertRefreshTokenAsync(Guid userId, string tokenString, DateTime expires, CancellationToken cancellationToken)
        {
            var existingToken = unitOfWork.RefreshTokenRepository
                                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (existingToken == null)
            {
                var refreshToken = new RefreshToken
                {
                    Token = tokenString,
                    UserId = userId,
                    Expires = expires,
                    Created = DateTime.UtcNow
                };

                await unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            }
            else
            {
                existingToken.Result!.Token = tokenString;
                existingToken.Result!.Expires = expires;
                existingToken.Result!.Created = DateTime.UtcNow;
                unitOfWork.RefreshTokenRepository.Update(existingToken.Result);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
