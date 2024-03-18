using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Entity;
using MovieWeb.Models;

namespace MovieWeb.Data
{
	public class MovieContext : DbContext
	{

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

		public DbSet<Movie> Movies { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Person> Persons { get; set; }

		// entity'lerin özellik tanımlamasını burada işleyebiliriz 
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>().Property(b => b.Title).IsRequired();
			modelBuilder.Entity<Movie>().Property(b => b.Title).HasMaxLength(500);

			modelBuilder.Entity<Genre>().Property(b => b.Name).IsRequired();
			modelBuilder.Entity<Genre>().Property(b => b.Name).HasMaxLength(50);
		}

	}
}
