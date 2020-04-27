using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.DTOs
{
	public class BascketDTO
	{
		public List<ItemSizeDTO> ItemSizeIds { get; set; }
		public int FullPrice { get; private set; }
	}
}
