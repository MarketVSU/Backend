using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.DTOs
{
	public class ItemFilterDTO
	{
		public string Color { get; set; } = string.Empty;
		public int StartPrice { get; set; } = 0;
		public int? EndPrice { get; set; }
	}
}
