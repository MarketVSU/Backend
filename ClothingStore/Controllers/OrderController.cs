using ClothingStore.Data;
using ClothingStore.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClothingStore.Controllers
{
	[Route("api/[controller]/")]
	public class OrderController
	{
		DataProvider dataProvider;
		UserDataProvider userDataProvider;

		public OrderController(DataProvider dp)
		{
			dataProvider = dp;
		}

		[HttpPost("CreateOrder")]
		public HttpResponseMessage CreateOrder(BascketDTO bascket, string userId)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
	}
}
