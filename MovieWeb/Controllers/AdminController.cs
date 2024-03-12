using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Data;
using MovieWeb.Entity;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	public class AdminController : Controller
	{
		private readonly MovieContext _context;

		public AdminController(MovieContext context)
		{
			_context = context;
		}

		// Movie

		[HttpGet]
		public IActionResult MovieCreate()
		{
			ViewBag.Genres = _context.Genres.ToList();
			return View(new AdminCreateMovieViewModel());
		}

		[HttpPost]
		public IActionResult MovieCreate(AdminCreateMovieViewModel model)
		{
			if (model.Title != null && model.Title.Contains("@"))
			{
				ModelState.AddModelError("", "Film başlığı '@' işareti içeremez!");
			}

			if (ModelState.IsValid)
			{
				var entity = new Movie
				{
					Title = model.Title,
					Description = model.Description,
					ImageUrl = "default.jpg"
				};

				foreach (var id in model.GenreIds)
				{
					entity.Genres.Add(_context.Genres.FirstOrDefault(i => i.GenreId == id));
				}

				_context.Movies.Add(entity);
				_context.SaveChanges();
				return RedirectToAction("MovieList", "Admin");
			}
			ViewBag.Genres = _context.Genres.ToList();
			return View(model);
		}

		[HttpPost]
		public IActionResult MovieDelete(int movieIds)
		{
			var entity = _context.Movies.Find(movieIds);

			if (entity != null)
			{
				_context.Movies.Remove(entity);
				_context.SaveChanges();
			}
			return RedirectToAction("MovieList");
		}

		public IActionResult MovieUpdate(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var entity = _context.Movies.Select(m => new AdminEditMovieViewModel
			{
				MovieId = m.MovieId,
				Title = m.Title,
				Description = m.Description,
				ImageUrl = m.ImageUrl,
				GenreIds = m.Genres.Select(i => i.GenreId).ToArray()
			}).FirstOrDefault(m => m.MovieId == id);

			ViewBag.Genres = _context.Genres.ToList();

			if (entity == null)
			{
				return NotFound();
			}
			return View(entity);
		}

		[HttpPost]
		public async Task<IActionResult> MovieUpdate(AdminEditMovieViewModel model, int[] genreIds, IFormFile file)
		{
			if (ModelState.IsValid)
			{
				var entity = _context.Movies.Include("Genres").FirstOrDefault(m => m.MovieId == model.MovieId);

				if (entity == null)
				{
					return NotFound();
				}

				entity.Title = model.Title;
				entity.Description = model.Description;
				// yüklenilen dosyanın resim formatını tanımasını sağlıyoruz
				if (file != null)
				{
					var extension = Path.GetExtension(file.FileName);  // .jpg , .png  uzantılarını aldık
					var fileName = Path.GetFileName(file.FileName); // Dosya adı uzantısı ile birlikte
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
					entity.ImageUrl = fileName;
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
				}

				// türlerin güncellemesini sağlıyoruz
				entity.Genres = genreIds.Select(id => _context.Genres.FirstOrDefault(i => i.GenreId == id)).ToList();

				_context.SaveChanges();

				return RedirectToAction("MovieList", "Admin");
			}
			ViewBag.Genres = _context.Genres.ToList();
			return View(model);
		}

		public IActionResult MovieList()
		{
			var movies = new AdminMoviesViewModel
			{
				Movies = _context.Movies
							.Include(m => m.Genres)
							.Select(m => new AdminMovieViewModel
							{
								MovieId = m.MovieId,
								Title = m.Title,
								ImageUrl = m.ImageUrl,
								Genres = m.Genres.ToList()
							}).ToList()
			};

			return View(movies);
		}

		// Genre

		private AdminGenresViewModel GetGenres()
		{
			var genres = new AdminGenresViewModel
			{
				Genres = _context.Genres.Select(g => new AdminGenreViewModel
				{
					GenreId = g.GenreId,
					Name = g.Name,
					Count = g.Movies.Count()
				}).ToList()
			};
			return genres;
		}

		public IActionResult GenreList()
		{
			return View(GetGenres());
		}

		[HttpPost]
		public IActionResult GenreCreate(AdminGenresViewModel model)
		{
			ModelState.Remove("Genres");  // Genres özelliği ModalState'den kaldırılır

			if (model.Name != null && model.Name.Length < 3)
			{
				ModelState.AddModelError("Name", "Tür adı minimum 3 karakterli olmalıdır!");
			}

			if (ModelState.IsValid)
			{
				_context.Genres.Add(new Genre { Name = model.Name });
				_context.SaveChanges();
				return RedirectToAction("GenreList");
			}

			return View("GenreList", GetGenres());
		}

		public IActionResult GenreUpdate(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var entity = _context
				.Genres
				.Select(g => new AdminGenreEditViewModel
				{
					GenreId = g.GenreId,
					Name = g.Name,
					Movies = g.Movies.Select(i => new AdminMovieViewModel
					{
						MovieId = i.MovieId,
						Title = i.Title,
						ImageUrl = i.ImageUrl
					}).ToList()
				})
				.FirstOrDefault(g => g.GenreId == id);

			if (entity == null)
			{
				return NotFound();
			}

			return View(entity);
		}

		[HttpPost]
		public IActionResult GenreUpdate(AdminGenreEditViewModel model, int[] movieIds)
		{
			if (ModelState.IsValid)
			{
				var entity = _context.Genres.Include("Movies").FirstOrDefault(i => i.GenreId == model.GenreId);

				if (model == null)
				{
					return NotFound();
				}

				entity.Name = model.Name;

				foreach (var id in movieIds)
				{
					entity.Movies.Remove(entity.Movies.FirstOrDefault(m => m.MovieId == id));
				}

				_context.SaveChanges();

				return RedirectToAction("GenreList");
			}
			return View(model);
		}

		[HttpPost]
		public IActionResult GenreDelete(int genreId)
		{
			var entity = _context.Genres.Find(genreId);

			if (entity != null)
			{
				_context.Genres.Remove(entity);
				_context.SaveChanges();
			}
			return RedirectToAction("GenreList");
		}

	}
}
