using System.ComponentModel.DataAnnotations;

namespace MovieWeb.Models
{
	public class AdminGenresViewModel
	{
		[Required(ErrorMessage = "Tür bilgisi girmelisiniz!")]
		public string Name { get; set; }

		public List<AdminGenreViewModel> Genres { get; set; }
	}

	// yeni bir parametre 'count' eklendiği için
	// List<Genre> değil de List<AdminGenreViewModel> kullanıyoruz

	public class AdminGenreViewModel
	{
		public int GenreId { get; set; }

		public string Name { get; set; }

		public int Count { get; set; }
	}

	// edit genre model
	public class AdminGenreEditViewModel
	{
		public int GenreId { get; set; }

		[Required(ErrorMessage = "Tür bilgisi girmelisiniz!")]
		public string Name { get; set; }

		public List<AdminMovieViewModel> Movies { get; set; }

	}

}
