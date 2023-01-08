using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Persistance.Configurations;
public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
	public void Configure(EntityTypeBuilder<Article> builder)
	{
		builder.Property(m => m.Header).HasMaxLength(300).IsRequired();
		builder.Property(m => m.Content).HasMaxLength(20000).IsRequired();
		builder.Property(m => m.CategoryId).IsRequired();
		builder.Property(m => m.MemberId).IsRequired();
		builder.HasMany(m => m.Pictures).WithMany(m => m.Articles);
	}
}