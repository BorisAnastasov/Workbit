using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workbit.Application.Common.Models;
using Workbit.Application.Features.Auth.Refresh;
using Workbit.Application.Features.Auth.Register;
using Workbit.WebApi.Contracts.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Workbit.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            SetTokensIntoCookies(result.Token, result.Expires, result.RefreshToken, result.RefreshTokenExpires);

            var response = mapper.Map<AuthResponseDto>(result);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            SetTokensIntoCookies(result.Token, result.Expires, result.RefreshToken, result.RefreshTokenExpires);

            var response = mapper.Map<AuthResponseDto>(result);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();

            var result = await mediator.Send(new RefreshTokenCommand(refreshToken), cancellationToken);

            SetTokensIntoCookies(result.Token, result.Expires, result.RefreshToken, result.RefreshTokenExpires);

            return NoContent();
        }
        private void SetTokensIntoCookies(string accessToken, DateTime accessExpires, string refreshToken, DateTime refreshExpires)
        {
            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = accessExpires
            });

            Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshExpires,
                Path = "/api/auth/refresh"
            });
        }
    }
}
