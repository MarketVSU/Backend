using System;
using System.Collections.Generic;
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
	public class OrderControllerTests
	{
		[Test]
		public async Task CreateOrderTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: "Products Test")
				.Options;

			using (var context = new ApplicationContext(options))
			{
				var dataProvider = new DataProvider(context);

				context.Sizes.Add(new Size() { SizeName = "NewSize" });

				context.SaveChanges();

				context.Items.Add(new Item()
					{Color = "color", Name = "Name", PicturePath = "C://asd/dasd/asd.png", Price = 100});

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
			
			int userId;

			using (var context = new ApplicationContext(options))
			{
				var a = "asdsadda";

				context.Users.Add(new User() {Login = a, Password = "asdasdasd", Name = "asdsadasdads"});

				context.SaveChanges();

				userId = context.Users.First(us => us.Login == a).Id;
			}

			var request = new RestRequest("api/Order/CreateOrder");
			request.AddParameter("auth_token", token);
			request.AddParameter("bascket", new BascketDTO()
			{
				ItemSizeIds = new List<ItemSizeDTO>()
				{
					new ItemSizeDTO(){Count = 1,ItemId = 0,SizeId = 0}
				}
			});
			request.AddParameter("address", "qwwqwqwww");
			request.AddParameter("userId", userId);

			var response = client.Post(request);

			var content = response.Content;

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}
