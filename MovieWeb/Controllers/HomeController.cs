using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

	}
}
