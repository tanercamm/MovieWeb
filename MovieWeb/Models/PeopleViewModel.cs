using System.ComponentModel.DataAnnotations;
using MovieWeb.Entity;

namespace MovieWeb.Models
{
	public class PeopleViewModel
	{
		public List<Person> People { get; set; }

	}

	public class PersonCreateViewModel
	{
		[Required]
		[StringLength(50, MinimumLength = 6, ErrorMessage = "İsim için 6-50 karakter aralığında girmelisiniz!")]
		public string Name { get; set; }

		[Required]
		[StringLength(3000, MinimumLength = 100, ErrorMessage = "Film açıklaması için 10-3000 karakter girmelisiniz!")]
		public string Biography { get; set; }

		[Required]
		[DataType(DataType.Url)]
		public string Imdb { get; set; }


		[Required]
		[DataType(DataType.DateTime)]
		public string PlaceOfBirth { get; set; }
	}

}

