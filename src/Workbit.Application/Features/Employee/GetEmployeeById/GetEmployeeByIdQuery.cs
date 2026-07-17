using MediatR;

namespace Workbit.Application.Features.Employee.GetEmployeeById
{
    public record GetEmployeeByIdQuery(Guid UserId):IRequest<GetEmployeeByIdResult>
    {
    }
}
