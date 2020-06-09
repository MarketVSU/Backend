using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

			builder.Property(user => user.Password)
				.IsRequired()
				.HasMaxLength(60);

			builder.HasData(
				new User() { Id = 1, IsAdmin = true, Login = "admin", Password = "admin" });
		}
	}
}
