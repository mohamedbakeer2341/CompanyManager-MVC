using Company.Demo.DAL.Models;
using Company.Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
		[HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Check if username already exists
				var userByName = await _userManager.FindByNameAsync(model.UserName);
				if (userByName != null)
				{
					ModelState.AddModelError(string.Empty, "Username already exists.");
					return View(model);
				}

				// Check if email already exists
				var userByEmail = await _userManager.FindByEmailAsync(model.Email);
				if (userByEmail != null)
				{
					ModelState.AddModelError(string.Empty, "Email already exists.");
					return View(model);
				}

				// Create new user if username and email do not exist
				var user = new AppUser()
				{
					UserName = model.UserName,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					IsAgreed = model.IsAgreed
				};

				// Attempt to create the user
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}

				// Add any errors from the user creation process
				foreach (var item in result.Errors)
				{
                    Console.WriteLine(item.Description);
                    ModelState.AddModelError(string.Empty, item.Description);
				}
			}

			// Return the model with validation errors if creation fails
			return View(model);
		}
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if(user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), "Home");
						}
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}
	}
}
