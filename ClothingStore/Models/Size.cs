using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class Size
	{
		public int Id { get; set; }
		public string SizeName { get; set; }
		public ICollection<ItemSize> ItemSizes { get; set; }
	}
}
