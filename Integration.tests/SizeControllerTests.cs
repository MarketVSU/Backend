using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Controllers;
using ClothingStore.Data;
using ClothingStore.DTOs;
using ClothingStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestSharp;

namespace Integration.tests
{
	[TestFixture]
	public class SizeControllerTests
	{
		private object itemsController;

		[Test]
		public async Task GetAllItemsTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				var dataProvider = new DataProvider(context);

				var sizeController = new SizeController(dataProvider);

				var response = (await sizeController.GetAllSizes()).Value.ToList();

				Assert.That(response.Count, Is.EqualTo(0));

				context.Sizes.Add(new Size() { SizeName = "NewSize" });

				context.SaveChanges();

				response = (await sizeController.GetAllSizes()).Value.ToList();

				Assert.That(response.Count, Is.EqualTo(1));
			}
		}

		[Test]
		public async Task AddSizeTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			string token = "";

			using (var context = new ApplicationContext(options))
			{

				var a = new AccountController(new UserDataProvider(context));

				var responseToken = (await a.Token("admin", "admin")) as JsonResult;

				var resp = responseToken.Value;

				Type myType = resp.GetType();
				IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

				var lst = new List<string>();

				foreach (PropertyInfo prop in props)
				{
					lst.Add(prop.GetValue(resp, null).ToString());
				}

				token = lst[0];
			}

			var client = new RestClient("http://localhost");

			var request = new RestRequest("api/Size/AddSize");
			request.AddParameter("auth_token", token);
			request.AddParameter("sizeDTO", new SizeDTO() {SizeName = "newSize"});

			var response = client.Post(request);

			var content = response.Content;

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public async Task DeleteSizeTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				var dataProvider = new DataProvider(context);

				context.Sizes.Add(new Size() {SizeName = "NewSize"});

				context.SaveChanges();
			}

			string token = "";

			using (var context = new ApplicationContext(options))
			{

				var a = new AccountController(new UserDataProvider(context));

				var responseToken = (await a.Token("admin", "admin")) as JsonResult;

				var resp = responseToken.Value;

				Type myType = resp.GetType();
				IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

				var lst = new List<string>();

				foreach (PropertyInfo prop in props)
				{
					lst.Add(prop.GetValue(resp, null).ToString());
				}

				token = lst[0];
			}

			var client = new RestClient("http://localhost");

			var request = new RestRequest("api/Size/AddSize");
			request.AddParameter("auth_token", token);
			request.AddParameter("sizeId", 0);

			var response = client.Post(request);

			var content = response.Content;

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}
