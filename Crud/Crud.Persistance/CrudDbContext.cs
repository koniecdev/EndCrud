using Crud.Domain.Common;
using Crud.Domain.Entities;
using System.Reflection;

namespace Crud.Persistance;
public class CrudDbContext : DbContext, ICrudDbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _userService;
	public CrudDbContext()
	{
    }
    public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
    {
    }
	public CrudDbContext(
		DbContextOptions<CrudDbContext> options, IDateTime dateTime, ICurrentUserService userService) : base(options)
	{
		_dateTime = dateTime;
		_userService = userService;
	}

    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Picture> Pictures { get; set; }
    public virtual DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _userService.Email;
                    entry.Entity.Created = _dateTime.Now;
                    entry.Entity.StatusId = 1;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _userService.Email;
                    entry.Entity.Modified = _dateTime.Now;
                    break;
                case EntityState.Deleted:
                    entry.Entity.ModifiedBy = _userService.Email;
                    entry.Entity.Modified = _dateTime.Now;
                    entry.Entity.Inactivated = _dateTime.Now;
                    entry.Entity.InactivatedBy = _userService.Email;
                    entry.Entity.StatusId = 0;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}