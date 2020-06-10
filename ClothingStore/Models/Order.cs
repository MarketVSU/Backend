using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.Enums;

namespace ClothingStore.Models
{
	public class Order
	{
		private int _totalPrice = 0;

		public Order()
		{
			ItemInOrders = new HashSet<ItemInOrder>();
		}

		public int Id { get; set; }
		public int TotalPrice 
		{
			get 
			{
				return _totalPrice;
			}
			set
			{
				if (ItemInOrders.Any())
				{
					_totalPrice = 0;
					foreach (var item in ItemInOrders)
					{
						_totalPrice += item.ItemPrice;
					}
				}
			}
		}
		public int UserId { get; set; }
		public string Address { get; set; }
		public User User { get; set; }
		public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.New;
		public PaymentOrderStatus PaymentOrderStatus { get; set; } = PaymentOrderStatus.Unpaid;
		public ICollection<ItemInOrder> ItemInOrders { get; private set; }
	}
}
