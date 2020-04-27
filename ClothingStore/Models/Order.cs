using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.Enums;

namespace ClothingStore.Models
{
	public class Order
	{
		public Order()
		{
			ItemInOrders = new HashSet<ItemInOrder>();
		}

		public int Id { get; set; }
		public int TotalPrice { get; set; }
		public int UserId { get; set; }
		public string Address { get; set; }
		public User User { get; set; }
		public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.New;
		public PaymentOrderStatus PaymentOrderStatus { get; set; } = PaymentOrderStatus.Unpaid;
		public ICollection<ItemInOrder> ItemInOrders { get; private set; }
	}
}
