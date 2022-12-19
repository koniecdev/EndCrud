using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Persistance.Configurations;
public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
	public void Configure(EntityTypeBuilder<Member> builder)
	{
		builder.Property(m => m.UserId).IsRequired();
		builder.Property(m => m.Email).HasMaxLength(100).IsRequired();
		builder.Property(m => m.Username).HasMaxLength(100).IsRequired();
	}
}