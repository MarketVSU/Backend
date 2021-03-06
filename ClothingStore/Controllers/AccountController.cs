﻿using ClothingStore.Configuration.AuthTokenConfig;
using ClothingStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClothingStore.DTOs;
using ClothingStore.Data;

namespace ClothingStore.Controllers
{
	[Route("api/[controller]/")]
	public class AccountController : Controller
	{
		DataProvider _dataProvider;

		public AccountController(DataProvider dp)
		{
			_dataProvider = dp;
		}

		[HttpPost("signin")]
		//[SwaggerResponse]
		public async Task<IActionResult> Token(string userName, string password)
		{
			var identity = await GetIdentity(userName, password);
			if (identity == null)
			{
				return BadRequest(new { errorText = "Invalid username or password." });
			}

			var now = DateTime.UtcNow;

			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					notBefore: now,
					claims: identity.Claims,
					expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new
			{
				access_token = encodedJwt,
				username = identity.Name
			};

			return Json(response);
		}

		private async Task<ClaimsIdentity> GetIdentity(string username, string password)
		{
			User person = (await _dataProvider.GetIEnumerable<User>()).ToList().FirstOrDefault(x => x.Name == username && x.Password == password);

			if (person != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, person.IsAdmin ? "admin":"consumer")
				};
				ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}

			// если пользователя не найдено
			return null;
		}
	}
}
