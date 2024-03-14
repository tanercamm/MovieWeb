using System.ComponentModel.DataAnnotations;

namespace MovieWeb.Entity
{
	public class Person
	{

		public int PersonId { get; set; }

		public string Name { get; set; }

		public string Biography { get; set; }

		public string Imdb { get; set; }

		public string ImageUrl { get; set; }

		public string PlaceOfBirth { get; set; }

	}
}
