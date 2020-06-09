using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class ItemInOrder
	{
		private int _itemPrice = 0;

		public int Id { get; set; }
		public int ItemSizeId { get; set; }
		public ItemSize ItemSize { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public int ItemPrice
		{
			get
			{
				return _itemPrice;
			}
			set
			{
				_itemPrice = ItemSize.Item.Price * ItemInOrderCount;
			}
		}
		public int ItemInOrderCount { get; set; } = 1;
	}
}
