using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class ItemSize
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public Item Item { get; set; }
		public string SizeName { get; set; } 
		public Size Size { get; set; }
		public int CountOfItem { get; set; }
	}
}
