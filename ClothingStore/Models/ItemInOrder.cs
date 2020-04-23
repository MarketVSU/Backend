using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class ItemInOrder
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public Item Item { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public int ItemInOrderCount { get; set; }
	}
}
