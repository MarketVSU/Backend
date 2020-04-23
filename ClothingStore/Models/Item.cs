using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Color { get; set; }
		public int Price { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? ItemCount 
		{
			get
			{
				return ItemCount;
			}
			set
			{
				if(ItemSizes.Count() != 0)
				{
					foreach(var item in ItemSizes)
					{
						value += item.CountOfItem;
					}
				}
				ItemCount = value;
			}
		}
		public ICollection<ItemSize> ItemSizes { get; set; }
	}
}
