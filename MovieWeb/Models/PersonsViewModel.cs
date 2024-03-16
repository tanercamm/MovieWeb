using System.ComponentModel.DataAnnotations;
using MovieWeb.Entity;

namespace MovieWeb.Models
{
	public class PersonsViewModel
	{
		public List<Person> Persons { get; set; }
		public bool isDirector { get; set; }
	}

	public class PersonCreateViewModel
	{
		[Required]
		[StringLength(50, MinimumLength = 6, ErrorMessage = "İsim için 6-50 karakter aralığında girmelisiniz!")]
		public string Name { get; set; }

		[Required]
		[StringLength(3000, MinimumLength = 100, ErrorMessage = "Film açıklaması için 10-3000 karakter  aralığında girmelisiniz!")]
		public string Biography { get; set; }

		[Required]
		[DataType(DataType.Url)]
		public string Imdb { get; set; }

		[Display(Name = "City ​​of Birth")]
		[Required]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "Doğum yeri için 2-25 karakter aralığında girmelisiniz!")]
		public string PlaceOfBirth { get; set; }
	}

	public class PersonEditViewModel
	{
		public int PersonId { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 6, ErrorMessage = "İsim için 6-50 karakter aralığında girmelisiniz!")]
		public string Name { get; set; }

		[Required]
		[StringLength(3000, MinimumLength = 100, ErrorMessage = "Film açıklaması için 100-3000 karakter  aralığında girmelisiniz!")]
		public string Biography { get; set; }

		[Required]
		[DataType(DataType.Url)]
		public string Imdb { get; set; }

		public string ImageUrl { get; set; }

		[Display(Name = "City ​​of Birth")]
		[Required]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "Doğum yeri için 2-25 karakter aralığında girmelisiniz!")]
		public string PlaceOfBirth { get; set; }

		[Display(Name = "Job")]
		public bool isDirector { get; set; }
	}

	public class DirectorListViewModel
	{
		public List<Person> Directors { get; set; }
	}

	public class CastListViewModel
	{
		public List<Person> Casts { get; set; }
	}

}

