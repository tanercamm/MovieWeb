using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Entity;
using MovieWeb.Models;

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
			return View(new PersonCreateViewModel());
		}

		[HttpPost]
		public IActionResult PersonCreate(PersonCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var person = new Person
				{
					Name = model.Name,
					Biography = model.Biography,
					Imdb = model.Imdb,
					PlaceOfBirth = model.PlaceOfBirth,
					ImageUrl = "icon.jpg"
				};

				_context.Persons.Add(person);
				_context.SaveChanges();

				return RedirectToAction("PersonList");
			}
			return View(model);
		}


		//public IActionResult PersonList()
		//{
		//	var people = _context.Persons.ToList();
		//	var viewModel = new PersonsViewModel
		//	{
		//		Persons = people
		//	};
		//	return View(viewModel);
		//}

		public IActionResult PersonList(bool? isDirector)
		{
			var people = _context.Persons.ToList();
			if (isDirector.HasValue)
			{
				if (isDirector == true)
				{
					var viewModel = new PersonsViewModel
					{
						Persons = people,
						isDirector = true
					};
					return View(viewModel);
				}
				else
				{
					var filteredPeople = people.Where(p => p.isDirector == false).ToList();
					var viewModel = new PersonsViewModel
					{
						Persons = filteredPeople,
						isDirector = false
					};
					return View(viewModel);
				}
			}
			else
			{
				var viewModel = new PersonsViewModel
				{
					Persons = people
				};
				return View(viewModel);
			}
		}

		public IActionResult DirectorList()
		{
			var directorList = _context.Persons.Where(p => p.isDirector == true).ToList();
			var viewModel = new DirectorListViewModel
			{
				Directors = directorList
			};
			return View(viewModel);
		}

		public IActionResult CastList()
		{
			var castList = _context.Persons.Where(c => c.isDirector == false).ToList();
			var viewModel = new CastListViewModel
			{
				Casts = castList
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult PersonDelete(int personId)
		{
			var entity = _context.Persons.Find(personId);

			if (entity != null)
			{
				_context.Persons.Remove(entity);
				_context.SaveChanges();
			}
			return RedirectToAction("PersonList");
		}

		[HttpGet]
		public IActionResult PersonUpdate(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var entity = _context.Persons.Select(p => new PersonEditViewModel
			{
				PersonId = p.PersonId,
				Name = p.Name,
				Biography = p.Biography,
				Imdb = p.Imdb,
				ImageUrl = p.ImageUrl,
				PlaceOfBirth = p.PlaceOfBirth,
				isDirector = p.isDirector
			}).FirstOrDefault(p => p.PersonId == id);

			if (entity == null)
			{
				return NotFound();
			}
			return View(entity);
		}

		[HttpPost]
		public async Task<IActionResult> PersonUpdate(PersonEditViewModel model, IFormFile file)
		{
			if (ModelState.IsValid)
			{
				var entity = _context.Persons.FirstOrDefault(g => g.PersonId == model.PersonId);

				if (entity == null)
				{
					return NotFound();
				}

				entity.Name = model.Name;
				entity.Biography = model.Biography;
				entity.Imdb = model.Imdb;
				entity.PlaceOfBirth = model.PlaceOfBirth;
				entity.isDirector = model.isDirector;

				if (file != null)
				{
					var extension = Path.GetExtension(file.FileName);  // .jpg , .png  uzantılarını aldık
					var fileName = Path.GetFileName(file.FileName); // Dosya adı uzantısı ile birlikte
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\persons", fileName);
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

				return RedirectToAction("PersonList");
			}

			return View(model);
		}

	}
}
