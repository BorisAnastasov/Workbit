using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workbit.Application.Features.Auth.Register;
using Workbit.WebApi.Contracts.Auth;

namespace Workbit.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            SetJWTIntoCookie(result.Token, result.Expires);

            var response = new AuthResponseDto
            {
                UserId = result.UserId,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Roles = result.Roles
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken) {
            var result = await mediator.Send(command, cancellationToken);

            SetJWTIntoCookie(result.Token, result.Expires);

            var response = new AuthResponseDto
            {
                UserId = result.UserId,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Roles = result.Roles
            };

            return Ok(response);

        }

        private void SetJWTIntoCookie(string token, DateTime expires)
        {
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,           
                SameSite = SameSiteMode.Strict,
                Expires = expires
            });
        }
    }
}
