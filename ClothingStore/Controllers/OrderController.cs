using ClothingStore.Data;
using ClothingStore.DTOs;
using ClothingStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClothingStore.Controllers
{
	[Route("api/[controller]/")]
	public class OrderController
	{
		UserDataProvider userDataProvider;
		private protected ApplicationContext _db;

		public OrderController(UserDataProvider dp, ApplicationContext context)
		{
			userDataProvider = dp;
			_db = context;
		}

		[HttpPost("CreateOrder")]
		public async Task<HttpResponseMessage> CreateOrder(BascketDTO bascket, int userId, string address)
		{
			var ordNumb = Guid.NewGuid();

			var a = new Order() { Address = address, UserId = userId, OrderNumber = ordNumb};

			_db.Orders.Add(a);
			await _db.SaveChangesAsync();

			var orderId = _db.Orders.First(or => or.OrderNumber == ordNumb).Id;

			var q = new List<ItemInOrder>();

			foreach(var s in bascket.ItemSizeIds)
			{
				q.Add(new ItemInOrder { OrderId = orderId, ItemSizeId = s.Id });
			}

			_db.ItemInOrders.AddRange(q);
			await _db.SaveChangesAsync();

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		[HttpPost("UpdateOrderStatus")]
		public async Task<HttpResponseMessage> UpdateStatus(OrderDTO order)
		{
			var q = _db.Orders.First(a => a.Id == order.Id);

			if(q.OrderStatus == Enums.OrderStatusEnum.New && order.OrderStatus == Enums.OrderStatusEnum.Transfering)
			{
				foreach(var p in order.Bascket.ItemSizeIds)
				{
					await UpdateItemCount(p.Count, 0, p.Id);
				}
			}
			else if((q.OrderStatus == Enums.OrderStatusEnum.Closed || q.OrderStatus == Enums.OrderStatusEnum.Transfering) && order.OrderStatus == Enums.OrderStatusEnum.Return)
			{
				foreach (var p in order.Bascket.ItemSizeIds)
				{
					await UpdateItemCount(p.Count, 1, p.Id);
				}
			}

			q.OrderStatus = order.OrderStatus;
			q.PaymentOrderStatus = order.PaymentOrderStatus;

			await _db.SaveChangesAsync();

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		private async Task UpdateItemCount(int count, int status, int id)
		{


			switch (status)
			{
				case 0:
					var u = _db.ItemSizes.First(a => a.Id == id);
					u.CountOfItem -= count;
					await _db.SaveChangesAsync();
					break;
				case 1:
					var h = _db.ItemSizes.First(a => a.Id == id);
					h.CountOfItem += count;
					await _db.SaveChangesAsync();
					break;
			}
		}
	}
}
