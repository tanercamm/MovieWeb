namespace MovieWeb.Entity
{
	public class Genre
	{
		public int GenreId { get; set; }

		public string Name { get; set; }

		public List<Movie> Movies { get; set; }

	}

	// 1-1 , 1-2 , 1-3 , 2-1  => bir tür birden fazla filme sahip olabilir
}
