using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly MovieContext _context;

		public HomeController(MovieContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var model = new HomePageViewModel
			{
				PopularMovies = _context.Movies.ToList()
			};
			return View(model);
		}

		public IActionResult About()
		{
			return View();
		}

	}
}
