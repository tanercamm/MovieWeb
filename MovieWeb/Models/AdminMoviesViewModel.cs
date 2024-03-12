using System.ComponentModel.DataAnnotations;
using MovieWeb.Entity;

namespace MovieWeb.Models
{
	public class AdminMoviesViewModel
	{
        public List<AdminMovieViewModel> Movies { get; set; }
    }

	// ViewModel'den her bilginin gelmesini istemiyoruz bu yüzden;
	// List<Movie> değil de List<AdminMovieViewModel> veri tipi ile Movies parametresine ulaşıyoruz!
	// böylelikle Description tipini çağırmayı engelledik!

	// description olmadan yeni bir model
	public class AdminMovieViewModel
	{
		public int MovieId { get; set; }

		public string Title { get; set; }

		public string ImageUrl { get; set; }

		public List<Genre> Genres { get; set; }
	}

	// create için belirlenen kurallar ile model
	public class AdminCreateMovieViewModel
	{
		[Display(Name = "Film Adı")]
		[Required(ErrorMessage = "Film Adı Girmelisiniz!")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Film adı için 3-50 karakter girmelisiniz!")]
		public string Title { get; set; }

		[Display(Name = "Film Açıklaması")]
		[Required(ErrorMessage = "Film Açıklaması Girmelisiniz!")]
		[StringLength(3000, MinimumLength = 10, ErrorMessage = "Film açıklaması için 10-3000 karakter girmelisiniz!")]
		public string Description { get; set; }

		[Required(ErrorMessage = "En az bir tür seçmelisiniz!")]
		public int[] GenreIds { get; set; }
	}

	// edit için belirlenen kurallar ile model
	public class AdminEditMovieViewModel
	{
		public int MovieId { get; set; }

		[Display(Name = "Film Adı")]
		[Required(ErrorMessage = "Film Adı Girmelisiniz!")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Film adı için 3-50 karakter girmelisiniz!")]
		public string Title { get; set; }

		[Display(Name = "Film Açıklaması")]
		[Required(ErrorMessage = "Film Açıklaması Girmelisiniz!")]
		[StringLength(3000, MinimumLength = 10, ErrorMessage = "Film açıklaması için 10-3000 karakter girmelisiniz!")]
		public string Description { get; set; }

		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "En az bir tür seçmelisiniz!")]
		public int[] GenreIds { get; set; }
	}


}
