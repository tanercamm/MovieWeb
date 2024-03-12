using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Data;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	public class MoviesController : Controller
	{
		private readonly MovieContext _context;

		public MoviesController(MovieContext context)
		{
			_context = context;
		}

		public IActionResult List(int? id, string q)
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
				Movies = movies.ToList()
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
