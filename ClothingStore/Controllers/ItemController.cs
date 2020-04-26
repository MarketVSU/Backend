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
	public class ItemController: Controller
	{
		private DataProvider dp;

		public ItemController(DataProvider data)
		{
			dp = data;
		}

		[HttpGet("GetAllItems")]
		public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAllItems()
		{
			return (await dp.GetIEnumerableMapped<Item, ItemDTO>()).ToList();
		}

		[HttpPut("CreateNewItem")]
		public HttpResponseMessage PutItem(ItemDTO item)
		{
			try
			{
				dp.PutMapped<Item>(item);
			}
			catch(Exception ex)
			{
				var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
				errorResponse.Content = new StringContent(ex.Message);
				return errorResponse;
			}

			return new HttpResponseMessage(HttpStatusCode.Created);
		}

		[HttpPost("FilterItems")]
		public async Task<ActionResult<IEnumerable<ItemDTO>>> FilterItems(string filteringValue)
		{
			return (await dp.GetIEnumerableMapped<Item, ItemDTO>())
				.Where(item => item.Name.Contains(filteringValue) 
					|| item.Description.Contains(filteringValue)
					|| item.Color.Contains(filteringValue)).ToList();
		}

		[HttpPost("Update")]
		public HttpResponseMessage UpdateItem(ItemUpdateDTO item)
		{
			try
			{
				dp.UpdateMapped<Item>(item);
			}
			catch (Exception ex)
			{
				var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
				errorResponse.Content = new StringContent(ex.Message);
				return errorResponse;
			}

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		[HttpPost("GetItemsByFilter")]
		public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItemsByFilter(ItemFilterDTO filter)
		{
			if (!string.IsNullOrEmpty(filter.Color))
				return filter.EndPrice == null
					? (await dp.GetIEnumerableMapped<Item, ItemDTO>())
					.Where(x => x.Price >= filter.StartPrice && x.Color.Contains(filter.Color))
					.ToList()
					: (await dp.GetIEnumerableMapped<Item, ItemDTO>())
					.Where(x => x.Price >= filter.StartPrice && x.Price <= filter.EndPrice && x.Color.Contains(filter.Color))
					.ToList();
			else
				return filter.EndPrice == null
					? (await dp.GetIEnumerableMapped<Item, ItemDTO>())
					.Where(x => x.Price >= filter.StartPrice)
					.ToList()
					: (await dp.GetIEnumerableMapped<Item, ItemDTO>())
					.Where(x => x.Price >= filter.StartPrice && x.Price <= filter.EndPrice)
					.ToList();
		}
	}
}
