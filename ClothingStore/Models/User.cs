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
		private string _pass;

		public User()
		{
			Orders = new HashSet<Order>();
		}

		public int Id { get; set; }
		public bool IsAdmin { get; set; } = false;
		public string Name { get; set; }
		public string Login { get; set; }
		public string Password 
		{
			get
			{
				return _pass;
			}
			set
			{
				if (value.Length >= 5)
					_pass = SetHash(value);
				else
					new Exception();
			}
		}
		public string Address { get; set; }
		public string TelephoneNumber { get; set; }
		public ICollection<Order> Orders { get; private set; }

		private string SetHash(string password)
		{
			var hash = Encoding.ASCII.GetBytes(password);

			var sha = new SHA1CryptoServiceProvider();
			var shaHash = sha.ComputeHash(hash);

			var asciiEnc = new ASCIIEncoding();
			var hashedPass = asciiEnc.GetString(shaHash);

			return hashedPass;
		}
	}
}
