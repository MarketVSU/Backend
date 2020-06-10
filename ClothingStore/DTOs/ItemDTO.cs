using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.DTOs
{
	public class ItemDTO
	{
		public int Id { get; set; }
		public int Price { get; set; }
		public string Name { get; set; }
		public string PicturePath { get; set; }
		public string Color { get; set; }
		public string Description { get; set; }
		public int ItemCount { get; set; }
	}
}
