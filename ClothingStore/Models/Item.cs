using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class Item
	{
		public Item()
		{
			ItemSizes = new HashSet<ItemSize>();
		}

		public int Id { get; set; }
		public string Color { get; set; }
		public string PicturePath { get; set; }
		public int Price { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? ItemCount { get; set; }
		public ICollection<ItemSize> ItemSizes { get; private set; }
	}
}
