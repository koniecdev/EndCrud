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
		mapped.MemberId = await _db.Members.Where(m => m.UserId == request.UserId).Select(x => x.Id).SingleOrDefaultAsync(cancellationToken);
		_db.Articles.Add(mapped);
		if(request.Gallery.Count > 0)
		{
			if (mapped.Pictures == null)
			{
				mapped.Pictures = new List<Picture>();
			}
			foreach (var id in request.Gallery)
			{
				var photo = await _db.Pictures.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
				if(photo != null)
				{
					mapped.Pictures.Add(photo);
				}
			}
		}
		await _db.SaveChangesAsync(cancellationToken);
		if(mapped.Id == 0)
		{
			throw new DatabaseException("Article cant be added", new Exception());
		}
		return mapped.Id;
	}
}