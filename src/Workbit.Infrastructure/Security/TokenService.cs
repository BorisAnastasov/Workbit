using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Workbit.Application.Common.Models;
using Workbit.Application.Exceptions;
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
            var (accessToken, accessExpires) = await CreateAccessTokenAsync(user);
            var (refreshToken, refreshExpires) = await CreateRefreshTokenAsync(user.Id, cancellationToken);

            return new TokenResult
            {
                UserId = user.Id,
                Token = accessToken,
                Expires = accessExpires,
                RefreshToken = refreshToken,
                RefreshTokenExpires = refreshExpires
            };
        }

        public async Task<TokenResult> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var existingToken = await unitOfWork.RefreshTokenRepository
                                    .FirstOrDefaultAsync(r => r.Token == refreshToken);

            if (existingToken == null)
                throw new UnauthorizedException("Invalid refresh token.");

            if (existingToken.Expires < DateTime.UtcNow)
                throw new UnauthorizedException("Refresh token expired.");

            var user = await userManager.FindByIdAsync(existingToken.UserId.ToString());
            if (user == null)
                throw new UnauthorizedException("User not found.");

            return await GenerateTokenAsync(user, cancellationToken);
        }

        private async Task UpsertRefreshTokenAsync(Guid userId, string tokenString, DateTime expires, CancellationToken cancellationToken)
        {
            var existingToken = await unitOfWork.RefreshTokenRepository
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
                existingToken.Token = tokenString;
                existingToken.Expires = expires;
                existingToken.Created = DateTime.UtcNow;
                unitOfWork.RefreshTokenRepository.Update(existingToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task<(string Token, DateTime Expires)> CreateAccessTokenAsync(ApplicationUser user)
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
            var expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiredMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expires);
        }

        private async Task<(string Token, DateTime Expires)> CreateRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            var refreshTokenString = GenerateRefreshTokenString();
            var refreshExpires = DateTime.UtcNow.AddDays(7);

            await UpsertRefreshTokenAsync(userId, refreshTokenString, refreshExpires, cancellationToken);

            return (refreshTokenString, refreshExpires);
        }

        private static string GenerateRefreshTokenString()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
