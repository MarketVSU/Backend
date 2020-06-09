using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.ModelsConfiguration
{
	public class ItemInOrderConfiguration: IEntityTypeConfiguration<ItemInOrder>
	{
		public void Configure(EntityTypeBuilder<ItemInOrder> builder)
		{
			builder.HasOne(a => a.Order)
				.WithMany(u => u.ItemInOrders)
				.HasForeignKey(a => a.OrderId);

			builder.HasOne(a => a.ItemSize)
				.WithMany(u => u.ItemInOrders)
				.HasForeignKey(a => a.ItemSizeId);
		}
	}
}
