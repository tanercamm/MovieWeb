using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Entity;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	public class AccountController : Controller
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		// REGISTER
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUser
				{
					Fullname = model.Fullname,
					UserName = model.Username.ToLower(),
					Email = model.Email,
					ImageUrl = "default.jpg"
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					// Başarılı kayıt işlemi
					return RedirectToAction("Login", "Account");
				}

				// Başarısız kayıt sonrası hatayı göster
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

			}

			return View(model);
		}

		// LOGIN
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if(ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user != null)
				{

					await _signInManager.SignOutAsync();

					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

					if (result.Succeeded)
					{
						await _userManager.ResetAccessFailedCountAsync(user);
						await _userManager.SetLockoutEndDateAsync(user, null);  // süreler sıfırlandı

						return RedirectToAction("Index", "Home");
					}
					else if (result.IsLockedOut)
					{
						var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
						var timeLeft = lockoutDate.Value - DateTime.UtcNow;
						ModelState.AddModelError("", $"Hesabınız kitlendi, Lütfen {timeLeft.Minutes} dakika sonra tekrar deneyiniz.");
					}
					else
					{
						ModelState.AddModelError("", "Parolanız hatalı!");
					}
				}
				else
				{
					ModelState.AddModelError("", "Bu Email adresi ile bir hesap bulunamadı!");
				}
			}

			return View(model);
		}

		// LOGOUT
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}



	}
}
