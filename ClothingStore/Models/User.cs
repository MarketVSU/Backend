using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Models
{
	public class User
	{
		public User()
		{
			Orders = new HashSet<Order>();
		}

		public int Id { get; set; }
		public bool IsAdmin { get; set; } = false;
		public string Name { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Address { get; set; }
		public string TelephoneNumber { get; set; }
		public ICollection<Order> Orders { get; private set; }
	}
}
