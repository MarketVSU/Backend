using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Data.ModelsConfiguration
{
	public class ItemConfiguration: IEntityTypeConfiguration<Item>
	{
		public void Configure(EntityTypeBuilder<Item> builder)
		{
			builder.Property(item => item.Id)
				.ValueGeneratedOnAdd();

			builder.Property(item => item.Color)
				.HasMaxLength(200);

			builder.Property(item => item.PicturePath)
				.IsRequired();

			builder.Property(item => item.Name)
				.IsRequired()
				.HasMaxLength(200);

			builder.HasIndex(item => item.Name)
				.IsUnique(true);

			builder.Ignore(item => item.ItemCount);
		}
	}
}
