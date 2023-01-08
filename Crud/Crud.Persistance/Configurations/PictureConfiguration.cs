using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Persistance.Configurations;
public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
	public void Configure(EntityTypeBuilder<Picture> builder)
	{
		builder.HasMany(m => m.ThumbnailArticles)
			.WithOne(m=>m.Thumbnail)
			.HasForeignKey(m => m.ThumbnailId);
		builder.Property(m => m.RelativePath).IsRequired();
	}
}