using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Data.ModelsConfiguration
{
	public class SizeConfiguration: IEntityTypeConfiguration<Size>
	{
		public void Configure(EntityTypeBuilder<Size> builder)
		{
			builder.Property(size => size.SizeName)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasIndex(size => size.SizeName)
				.IsUnique();
		}
	}
}
