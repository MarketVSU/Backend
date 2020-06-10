﻿using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Data.ModelsConfiguration
{
	public class OrderConfiguration: IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasOne(a => a.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(a => a.UserId);

			builder.HasMany(a => a.ItemInOrders)
				.WithOne(i => i.Order)
				.HasForeignKey(i => i.OrderId);

			builder.Property(a => a.Address)
				.HasMaxLength(300)
				.IsRequired();

			builder.Property(a => a.UserId)
				.IsRequired();
		}
	}
}
