using Workbit.Core.Models.Account;
using Workbit.Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Models.Account;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.RoleConstants;

namespace Workbit.App.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IRepository repository;

        public UserController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
			IRepository _repository)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            repository = _repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterManager()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCeo()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterEmployee()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = new ApplicationUser()
            {
				FirstName = model.FirstName,
				LastName = model.LastName,
				NormalizedEmail = model.Email.ToUpper(),
				Email = model.Email,
				UserName = model.Email,
				NormalizedUserName = model.Email.ToUpper(),
			};

            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, EmployeeRoleName);

				var student = new Employee
				{
					ApplicationUserId = user.Id,
					ApplicationUser = user
				};

				await repository.AddAsync(student);
				await repository.SaveChangesAsync();

				return RedirectToAction("Login", "User");
            }


			foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> RegisterManager(RegisterManagerViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}


			var user = new ApplicationUser()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				NormalizedEmail = model.Email.ToUpper(),
				Email = model.Email,
				UserName = model.Email,
				NormalizedUserName = model.Email.ToUpper(),
                DateOfBirth = model.DateOfBirth,
			};

			var result = await userManager.CreateAsync(user, model.Password);


			if (result.Succeeded)
			{
				await userManager.AddToRoleAsync(user, ManagerRoleName);

				var manager = new Manager
				{
					ApplicationUserId = user.Id,
					ApplicationUser = user,
				};
				await repository.AddAsync(manager);
				await repository.SaveChangesAsync();

				return RedirectToAction("Login", "User");
			}

			foreach (var item in result.Errors)
			{
				ModelState.AddModelError("", item.Description);
			}

			return View(model);
		}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCeo(RegisterCeoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                NormalizedEmail = model.Email.ToUpper(),
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                DateOfBirth = model.DateOfBirth,
            };

            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, CeoRoleName);

                var ceo = new Manager
                {
                    ApplicationUserId = user.Id,
                    ApplicationUser = user,
                };
                await repository.AddAsync(ceo);
                await repository.SaveChangesAsync();

                return RedirectToAction("Login", "User");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LogInViewModel();


            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(AdminRoleName));

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> AddUserToRoles()
        {
            string email = "admin@gmail.com";

            var user = await userManager.FindByEmailAsync(email);

            await userManager.AddToRoleAsync(user, AdminRoleName);

            return RedirectToAction("Index", "Home");
        }


    }
}
