using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Data.ModelsConfiguration
{
	public class ItemSizeConfiguration: IEntityTypeConfiguration<ItemSize>
	{
		public void Configure(EntityTypeBuilder<ItemSize> builder)
		{
			builder.HasOne(a => a.Item)
				.WithMany(a => a.ItemSizes)
				.HasForeignKey(a => a.ItemId);

			builder.HasOne(a => a.Size)
				.WithMany(a => a.ItemSizes)
				.HasForeignKey(a => a.SizeId);
		}
	}
}
