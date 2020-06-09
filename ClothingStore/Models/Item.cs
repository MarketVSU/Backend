using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class Item
	{
		private int _itemCount = 0;

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
		public int ItemCount 
		{ 
			get 
			{
				if (ItemSizes.Any())
				{
					_itemCount = 0;

					foreach (var itemSize in ItemSizes)
					{
						_itemCount += itemSize.CountOfItem;
					}
				}

				return 0;
			} 
		}
		public ICollection<ItemSize> ItemSizes { get; private set; }
	}
}
