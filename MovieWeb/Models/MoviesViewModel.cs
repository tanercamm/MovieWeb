using MovieWeb.Entity;

namespace MovieWeb.Models
{
	public class MoviesViewModel
	{
        public List<Movie> Movies { get; set; }

		public PageInfo PageInfo { get; set; } = new();
	}

	
}
