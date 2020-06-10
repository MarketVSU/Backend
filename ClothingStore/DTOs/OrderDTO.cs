using ClothingStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.DTOs
{
	public class OrderDTO
	{
		public OrderDTO(string address)
		{
			Address = address;
		}
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Address { get; set; }
		public BascketDTO Bascket { get; set; }
		public PaymentOrderStatus PaymentOrderStatus { get; private set; } = PaymentOrderStatus.Unpaid;
		public OrderStatusEnum OrderStatus { get; private set; } = OrderStatusEnum.New;
	}
}
