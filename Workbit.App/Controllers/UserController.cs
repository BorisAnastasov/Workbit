using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.User;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
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
        public async Task<IActionResult> RegisterManager()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterCeoViewModel
            {
                Countries = await userService.GetCountries(),
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCeo()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterCeoViewModel
            {
                Countries = await userService.GetCountries(),
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterEmployee()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterCeoViewModel
            {
                Countries = await userService.GetCountries(),
            };

            return View(model);
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
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber
			};

            var result = await userService.RegisterUserAsync(user, model.Password);


            if (result!.Succeeded)
            {
				var employee = new Employee
				{
					ApplicationUserId = user.Id,
					ApplicationUser = user
				};

                await userService.RegisterEmployeeAsync(employee);

				return RedirectToAction(nameof(Login),"User");
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
                PhoneNumber = model.PhoneNumber
            };

            var result = await userService.RegisterUserAsync(user, model.Password);


			if (result!.Succeeded)
			{
                var manager = new Manager
                {
                    ApplicationUserId = user.Id,
                    ApplicationUser = user,
                };
                
                await userService.RegisterManagerAsync(manager);

				return RedirectToAction(nameof(Login), "User");
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
                PhoneNumber = model.PhoneNumber
            };

            var result = await userService.RegisterUserAsync(user, model.Password);

            if (result!.Succeeded)
            {
                var ceo = new Ceo
                {
                    ApplicationUserId = user.Id,
                    ApplicationUser = user,
                };

                await userService.RegisterCeoAsync(ceo);

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

            var user = await userService.FindUserByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userService.LoginAsync(user, model.Password);

                if (result)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
