using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Entity;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	public class UsersController : Controller
	{
		private readonly MovieContext _context;

		public UsersController(MovieContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult UserList()
		{
			var users = _context.Users.ToList();

			var model = new UsersViewModel
			{
				Users = users
			};

			return View(model);
		}

		public IActionResult UserCreate(UserCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User
				{
					Name = model.Name,
					Username = model.Username.ToLower(),
					Email = model.Email,
					Password = model.Password,
					ConfirmPassword = model.ConfirmPassword,
					ImageUrl = "default.jpg"
				};

				_context.Users.Add(user);
				_context.SaveChanges();

				return RedirectToAction("UserList");
			}

			return View(model);
		}

		public IActionResult UserUpdate(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var entity = _context.Users.Select(u => new UserEditViewModel
			{
				UserId = u.UserId,
				Name = u.Name,
				Username = u.Username,
				Email = u.Email,
				Password = u.Password,
				ConfirmPassword = u.ConfirmPassword,
				ImageUrl = u.ImageUrl
			}).FirstOrDefault(p => p.UserId == id);

			if (entity == null)
			{
				return NotFound();
			}
			return View(entity);
		}

		[HttpPost]
		public async Task<IActionResult> UserUpdate(UserEditViewModel model, IFormFile file)
		{
			if(ModelState.IsValid)
			{
				var entity = _context.Users.FirstOrDefault(g => g.UserId == model.UserId);

				if (entity == null)
				{
					return NotFound();
				}

				entity.Name = model.Name;
				entity.Username = model.Username;
				entity.Email = model.Email;
				entity.Password = model.Password;
				entity.ConfirmPassword = model.ConfirmPassword;

				if (file != null)
				{
					var extension = Path.GetExtension(file.FileName);  // .jpg , .png  uzantılarını aldık
					var fileName = Path.GetFileName(file.FileName); // Dosya adı uzantısı ile birlikte
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\users", fileName);
					entity.ImageUrl = fileName;
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
				}
				else // yeni dosya yüklenmemişse, mevcut resmi tanımlarız
				{
					entity.ImageUrl = model.ImageUrl;
				}

				_context.SaveChanges();

				return RedirectToAction("UserList");
			}

			return View(model);
		}

	}
}
