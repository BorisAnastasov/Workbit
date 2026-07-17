using AutoMapper;
using MediatR;
using Workbit.Application.Exceptions;
using Workbit.Domain.Interfaces;

namespace Workbit.Application.Features.Employee.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GetEmployeeByIdResult> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.EmployeeRepository.FirstOrDefaultAsync(e => e.ApplicationUserId == request.UserId, asNoTracking: false,e=>e.Job!,e=>e.Country, e=>e.ApplicationUser);

            if (employee == null) {
                throw new NotFoundException(nameof(Employee),request.UserId);
            }
            var result = mapper.Map<GetEmployeeByIdResult>(employee);

            return result;
        }
    }
}
