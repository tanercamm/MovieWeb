﻿namespace MovieWeb.Entity
{
	public class User
	{

		public int UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public string ImageUrl { get; set; }
		
	}
}
