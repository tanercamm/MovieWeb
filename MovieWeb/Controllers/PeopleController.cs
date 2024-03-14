using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;

namespace MovieWeb.Controllers
{
	public class PeopleController : Controller
	{
		private readonly MovieContext _context;

        public PeopleController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
		{
			return View();
		}



	}
}
