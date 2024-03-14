using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Entity;

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

		[HttpGet]
		public IActionResult PersonCreate()
		{
			return View();
		}

		[HttpPost]
		public IActionResult PersonCreate(Person model)
		{
			_context.People.Add(model);
			return RedirectToAction("PersonList","People");
		}

		public IActionResult PersonList()
		{
			var people = _context.People.ToList();
			return View(people);
		}





	}
}
