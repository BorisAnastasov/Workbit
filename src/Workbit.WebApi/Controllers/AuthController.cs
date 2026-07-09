using AutoMapper;
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

            SetJWTIntoCookie(result.Token, result.Expires);

            var response = mapper.Map<AuthResponseDto>(result);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            SetJWTIntoCookie(result.Token, result.Expires);

            var response = mapper.Map<AuthResponseDto>(result);

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
