using ClothingStore.Controllers;
using ClothingStore.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClothingStore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Integration.tests
{

	[TestFixture]
	public class AccountControllerTests
	{
		[Test]
		public async Task SignInWithAdminRole()
		{

			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				var a = new AccountController(new UserDataProvider(context));

				var response = (await a.Token("admin", "admin")) as JsonResult;

				var resp = response.Value;

				Type myType = resp.GetType();
				IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

				var lst = new List<string>();

				foreach (PropertyInfo prop in props)
				{
					lst.Add(prop.GetValue(resp, null).ToString());
				}

				Assert.That(lst[1], Is.EqualTo("admin"));
			}
		}

		[Test]
		public async Task TryToRegisterUser()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				string login = "newLogin", password = "newPass";

				var a = new AccountController(new UserDataProvider(context));

				var response = await a.Register(new UserDTO()
					{Login = login, Address = "newAddress", Name = "newName", Password = password});

				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

				var signInResponce = (await a.Token(login, password)) as JsonResult;

				var resp = signInResponce.Value;

				Type myType = resp.GetType();
				IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

				var lst = new List<string>();

				foreach (PropertyInfo prop in props)
				{
					lst.Add(prop.GetValue(resp, null).ToString());
				}

				Assert.That(lst[1], Is.EqualTo(login));
			}
		}
	}
}
