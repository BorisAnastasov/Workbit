using MediatR;
using Microsoft.AspNetCore.Identity;
using Workbit.Application.Interfaces;
using Workbit.Domain.Constants;
using Workbit.Domain.Entities.Account;
using Workbit.Domain.Interfaces;

namespace Workbit.Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;

        public RegisterCommandHandler(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            await userManager.CreateAsync(user,request.Password);

            switch (request.Role) {
                case RoleConstants.EmployeeRoleName:
                    await unitOfWork.EmployeeRepository.AddAsync(new Employee { ApplicationUser = user });
                    break;
                case RoleConstants.ManagerRoleName:
                    await unitOfWork.ManagerRepository.AddAsync(new Manager { ApplicationUser = user});
                    break;
                case RoleConstants.CeoRoleName:
                    await unitOfWork.CeoRepository.AddAsync(new Ceo { ApplicationUser = user });
                    break;
                default:
                    throw new Exception();
            }

            await userManager.AddToRoleAsync(user, request.Role);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var roles = await userManager.GetRolesAsync(user);
            var tokenResult = await tokenService.GenerateTokenAsync(user, cancellationToken);

            return new RegisterResult {
                UserId = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList(),
                Token = tokenResult.Token,
                Expires = tokenResult.Expires
            };
        }
    }
}
