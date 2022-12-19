namespace Crud.Persistance;
public class CrudContextFactory : DesignTimeDbContextFactoryBase<CrudDbContext>
{
	protected override CrudDbContext CreateNewInstance(DbContextOptions<CrudDbContext> options)
	{
		return new CrudDbContext(options);
	}
}