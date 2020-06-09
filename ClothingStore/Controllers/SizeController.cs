using ClothingStore.Data;
using ClothingStore.DTOs;
using ClothingStore.Models;
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
	public class SizeController: Controller
	{
		private DataProvider dp;

		public SizeController(DataProvider dataProvider)
		{
			dp = dataProvider;
		}

		[HttpPost("AddSize")]
		public HttpResponseMessage AddSize(SizeDTO sizeDTO)
		{

			try
			{
				dp.CreateMapped<Size>(sizeDTO);
			}
			catch(Exception ex)
			{
				var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
				errorResponse.Content = new StringContent(ex.Message);
				return errorResponse;
			}

			return new HttpResponseMessage(HttpStatusCode.Created);
		}
	}
}
