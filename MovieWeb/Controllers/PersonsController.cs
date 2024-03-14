using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Entity;

namespace MovieWeb.Controllers
{
	public class PersonsController : Controller
	{
		private readonly MovieContext _context;

		public PersonsController(MovieContext context)
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
			_context.Persons.Add(model);
			return RedirectToAction("PersonList","People");
		}

		public IActionResult PersonList()
		{
			var people = _context.Persons.ToList();
			return View(people);
		}





	}
}
