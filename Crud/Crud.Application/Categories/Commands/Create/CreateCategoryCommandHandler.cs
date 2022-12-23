using Crud.Shared.Categories.Commands;

namespace Crud.Application.Categories.Commands;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public CreateCategoryCommandHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		var mapped = _mapper.Map<Category>(request);
		if(mapped == null)
		{
			throw new MappingException(nameof(CreateCategoryCommand), new Exception());
		}
		_db.Categories.Add(mapped);
		if(mapped.Id == 0)
		{
			throw new DatabaseException("Category cant be added", new Exception());
		}
		await _db.SaveChangesAsync(cancellationToken);
		return mapped.Id;
	}
}