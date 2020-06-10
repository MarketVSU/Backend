using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.DTOs
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Address { get; set; }
		public string TelephoneNumber { get; set; }
	}
}
