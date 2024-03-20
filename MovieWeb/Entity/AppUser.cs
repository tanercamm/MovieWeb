using Microsoft.AspNetCore.Identity;

namespace MovieWeb.Entity
{
	public class AppUser : IdentityUser
	{
		public string? Fullname { get; set; }
        public string? ImageUrl { get; set; }
    }
}
