using ClothingStore.Data;
using ClothingStore.DTOs;
using ClothingStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

		[Authorize(Roles = "admin")]
		[HttpPost("AddSize")]
		public async Task<HttpResponseMessage> AddSize(SizeDTO sizeDTO)
		{

			try
			{
				await dp.CreateMapped<Size>(sizeDTO);
			}
			catch(Exception ex)
			{
				var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
				errorResponse.Content = new StringContent(ex.Message);
				return errorResponse;
			}

			return new HttpResponseMessage(HttpStatusCode.Created);
		}

		[Authorize(Roles = "admin")]
		[HttpPost("DeleteSize")]
		public async Task<HttpResponseMessage> DeleteSize(int sizeId)
		{

			try
			{
				await dp.DeleteMapped<Size>((await dp.GetIEnumerable<Size>()).ToList().First(q => q.Id == sizeId));
			}
			catch (Exception ex)
			{
				var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
				errorResponse.Content = new StringContent(ex.Message);
				return errorResponse;
			}

			return new HttpResponseMessage(HttpStatusCode.Created);
		}

		[HttpGet("GetAllSizes")]
		public async Task<ActionResult<IEnumerable<SizeDTO>>> GetAllSizes()
		{
			return (await dp.GetIEnumerableMapped<Size, SizeDTO>()).ToList();
		}
	}
}
