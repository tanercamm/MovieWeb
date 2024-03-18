using System.ComponentModel.DataAnnotations;
using MovieWeb.Entity;

namespace MovieWeb.Models
{
	public class UsersViewModel
	{
        public List<User> Users { get; set; }
    }

	public class UserEditViewModel
	{
		public int UserId { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 6, ErrorMessage = "İsim için 6-50 karakter aralığında girmelisin!")]
		public string Name { get; set; }

		[Required]
		[StringLength(15, MinimumLength = 4, ErrorMessage = "Kullanıcı ismi için 4-15 karakter aralığında girmelisin!")]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public string ImageUrl { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Parola eşleşmiyor.")]
		public string ConfirmPassword { get; set; }

	}

	// Login
	public class UserLoginViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Parola eşleşmiyor.")]
		public string ConfirmPassword { get; set; }
	}

	// Register
	public class UserRegisterViewModel
	{
		[Required]
		[StringLength(50, MinimumLength = 6, ErrorMessage = "İsim için 6-50 karakter aralığında girmelisin!")]
		public string Name { get; set; }

		[Required]
		[StringLength(15, MinimumLength = 4, ErrorMessage = "Kullanıcı ismi için 4-15 karakter aralığında girmelisin!")]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Parola eşleşmiyor.")]
		public string ConfirmPassword { get; set; }
	}
}
