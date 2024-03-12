namespace MovieWeb.Entity
{
	public class Movie
	{
		public Movie()
		{
			Genres = new List<Genre>();
		}

		public int MovieId { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public List<Genre> Genres { get; set; }

	}

	// 1-1 , 1-2 , 1-3 , 2-1  => bir film birden fazla türe sahip olabilir
}
