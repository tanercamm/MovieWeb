using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Data;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	[Authorize]
	public class MoviesController : Controller
	{
		private readonly MovieContext _context;

		public MoviesController(MovieContext context)
		{
			_context = context;
		}

		public int pageSize = 10;

		public IActionResult List(int? id, string q, int page = 1)
		{
			//IEnumerable belleğe atıp, ardından sorgular
			//IQueryable direkt olarak server üzerinden sorgular (hız avantajı)
			var movies = _context.Movies.AsQueryable();

			// tür id göre kimlik atıyoruz
			if (id != null)
			{
				movies = movies.Include(m => m.Genres).Where(m => m.Genres.Any(g => g.GenreId == id));
			}

			if (!string.IsNullOrEmpty(q)) // q parametresi boş olmadığı koşul
			{
				movies = movies.Where(i =>
					i.Title.ToLower().Contains(q.ToLower()) ||
					i.Description.ToLower().Contains(q.ToLower())
				);  // ilgili string ifadeyi Title veya Description içerisinde bulma sorgusu
			}

			var model = new MoviesViewModel()
			{
				Movies = movies.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
				PageInfo = new PageInfo
				{
					ItemsPerPage = pageSize,
					TotalItems = _context.Movies.Count()
				}
			};

			return View("Movies", model);
		}

		public IActionResult Details(int id)
		{
			var movie = _context.Movies.Find(id);
			return View(movie);
		}

	}
}
