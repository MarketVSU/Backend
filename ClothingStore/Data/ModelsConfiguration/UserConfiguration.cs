using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ClothingStore.Data.ModelsConfiguration
{
	public class UserConfiguration: IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(user => user.Name)
				.IsRequired();

			builder.Property(user => user.Login)
				.IsRequired(true);
			builder.HasIndex(user => user.Login)
				.IsUnique(true);

			builder.HasData(
				new User() { Id = 1, IsAdmin = true, Login = "admin", Password = SetHash("admin"), Name = "admin" });
		}

		private string SetHash(string password)
		{
			var hash = Encoding.UTF8.GetBytes(password);

			var sha = new SHA1CryptoServiceProvider();
			var shaHash = sha.ComputeHash(hash);

			var hashedPass = Convert.ToBase64String(shaHash);

			return hashedPass;
		}
	}
}
