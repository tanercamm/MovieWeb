using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    [Authorize]
	public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public int pageSize = 10;

        public IActionResult Index(int page = 1)
        {
            var model = new HomePageViewModel
            {
                PopularMovies = _context.Movies.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageInfo = new PageInfo()
                {
                    ItemsPerPage = pageSize,
                    TotalItems = _context.Movies.Count()
                }
            };
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
