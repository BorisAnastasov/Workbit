using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workbit.Application.Features.Employee.GetEmployeeById;

namespace Workbit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public EmployeeController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        //[Authorize(Roles ="Manager,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetEmployeeByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

    }
}
