﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using MovieWeb.Entity;
using MovieWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MovieWeb.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;

		public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View(_userManager.Users);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return RedirectToAction("Index");
			}

			var user = await _userManager.FindByIdAsync(id);

			if (user != null)
			{
				ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();
				return View(new UsersEditViewModel
				{
					Id = user.Id,
					Fullname = user.Fullname,
					Username = user.UserName,
					Email = user.Email,
					ImageUrl = user.ImageUrl,
					SelectedRoles = await _userManager.GetRolesAsync(user)
				});
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, UsersEditViewModel model)
		{
			if (id != model.Id)
			{
				return RedirectToAction("Index");
			}
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(model.Id);
				if (user != null)
				{
					user.Email = model.Email;
					user.Fullname = model.Fullname;
					user.ImageUrl = model.ImageUrl;
					var result = await _userManager.UpdateAsync(user);

					if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
					{
						// admin olduğumuzdan şifreyi silip yeni atıyoruz
						await _userManager.RemovePasswordAsync(user);
						await _userManager.AddPasswordAsync(user, model.Password);
					}

					if (result.Succeeded)
					{
						await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
						if (model.SelectedRoles != null)
						{
							await _userManager.AddToRolesAsync(user, model.SelectedRoles);
						}

						return RedirectToAction("Index");
					}

					foreach (IdentityError err in result.Errors)
					{
						ModelState.AddModelError("", err.Description);
					}
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user != null)
			{
				await _userManager.DeleteAsync(user);
			}
			return RedirectToAction("Index");
		}

	}
}
