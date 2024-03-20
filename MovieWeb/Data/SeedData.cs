using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Entity;

namespace MovieWeb.Data
{
	public class SeedData
	{
		// default olarak admin bilgilerini tanımlıyoruz
		private const string adminUser = "admin";
		private const string adminPassword = "Admin_123";

		public static async void IdentityTestUser(IApplicationBuilder app)
		{

			var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MovieContext>();

			// proje her çalıştırıldığında migrate edilme komutunu hazır tutuyoruz
			if (!context.Database.GetAppliedMigrations().Any())
			{
				context.Database.Migrate();  //  <=>  'database update'
			}

			var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();


			var user = await userManager.FindByNameAsync(adminUser);

			if (user == null)
			{
				user = new AppUser
				{
					Fullname = "Admin MovieWeb",
					UserName = adminUser,
					Email = "info@admin.com",
					ImageUrl = "admin.png"
				};

				await userManager.CreateAsync(user, adminPassword);
			}

		}


	}
}
