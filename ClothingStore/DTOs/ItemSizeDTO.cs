using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.DTOs
{
	public class ItemSizeDTO
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public int SizeId { get; set; }
		public int Count { get; set; }
	}
}
