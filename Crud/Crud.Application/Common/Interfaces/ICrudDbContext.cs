using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Application.Common.Interfaces;
public interface ICrudDbContext
{
	DbSet<Member> Members { get; set; }
	DbSet<Category> Categories { get; set; }
	DbSet<Picture> Pictures { get; set; }
	DbSet<Article> Articles { get; set; }
}
