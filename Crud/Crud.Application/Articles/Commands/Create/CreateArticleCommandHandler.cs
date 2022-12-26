using Crud.Shared.Articles.Commands;

namespace Crud.Application.Articles.Commands;
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	public CreateArticleCommandHandler(ICrudDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
	{
		var mapped = _mapper.Map<Article>(request);
		if(mapped == null)
		{
			throw new MappingException(nameof(CreateArticleCommand), new Exception());
		}
		_db.Articles.Add(mapped);
		await _db.SaveChangesAsync(cancellationToken);
		if(mapped.Id == 0)
		{
			throw new DatabaseException("Article cant be added", new Exception());
		}
		return mapped.Id;
	}
}