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
	public class ItemControllerTests
	{
		[Test]
		public async Task GetAllItemsTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context =  new ApplicationContext(options))
			{
				var dataProvider = new DataProvider(context);

				var itemsController = new ItemController(dataProvider);

				var response = (await itemsController.GetAllItems()).Value.ToList();

				Assert.That(response.Count, Is.EqualTo(0));

				context.Items.Add(
					new Item() {Color = "color", Name = "Name", PicturePath = "C://asd/dasd/asd.png", Price = 100});

				context.SaveChanges();

				response = (await itemsController.GetAllItems()).Value.ToList();

				Assert.That(response.Count, Is.EqualTo(1));
			}
		}

		[Test]
		public async Task CreateNewItemTest()
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

			var request = new RestRequest("api/Item/CreateNewItem");
			request.AddParameter("auth_token", token);
			request.AddParameter("item",
				new ItemDTO() {Color = "Black", Name = "BlackItem", PicturePath = "asdasd", Price = 123});

			var response = client.Post(request);

			var content = response.Content;

			Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public async Task UpdateItemTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				var dataProvider = new DataProvider(context);

				context.Items.Add(
					new Item() { Color = "color", Name = "Name", PicturePath = "C://asd/dasd/asd.png", Price = 100 });

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

			var request = new RestRequest("api/Item/Update");
			request.AddParameter("auth_token", token);
			request.AddParameter("item", new ItemUpdateDTO(){Color = "Black",Name = "eee"});

			var response = client.Put(request);

			var content = response.Content;

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public async Task DeleteItemTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				var dataProvider = new DataProvider(context);

				context.Items.Add(
					new Item() { Color = "color", Name = "Name", PicturePath = "C://asd/dasd/asd.png", Price = 100 });

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

			var request = new RestRequest("api/Item/DeleteItem");
			request.AddParameter("auth_token", token);
			request.AddParameter("itemId", 0);

			var response = client.Post(request);

			var content = response.Content;

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}
