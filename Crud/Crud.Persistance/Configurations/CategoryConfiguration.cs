using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Persistance.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
	}
}