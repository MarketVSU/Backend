using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Configuration.AuthTokenConfig
{
	public class AuthOptions
	{
		public const string ISSUER = "ClothingStoreServer";
		public const string AUDIENCE = "ClothingStoreClient";
		const string _key = "12312SADCXZC-1SDASCXC-1231SDAD-GFDDG";
		public const int Lifetime = 45;
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
		}
	}
}
