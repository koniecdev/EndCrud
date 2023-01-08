using Crud.Shared.Pictures.Queries;
using Microsoft.Extensions.Hosting;

namespace Crud.Application.Pictures.Commands;
public class GetPictureQueryHandler : IRequestHandler<GetPictureQuery, GetPictureVm>
{
	private readonly ICrudDbContext _db;
	private readonly IMapper _mapper;
	private readonly IHostEnvironment _hostEnvironment;

	public GetPictureQueryHandler(ICrudDbContext db, IMapper mapper, IHostEnvironment hostEnvironment)
	{
		_db = db;
		_mapper = mapper;
		_hostEnvironment = hostEnvironment;
	}
	public async Task<GetPictureVm> Handle(GetPictureQuery query, CancellationToken cancellationToken)
	{
		var pth = query.ImagePath.Replace("%2F", @"\");
		var fromDb = await _db.Pictures.FirstOrDefaultAsync(m => m.RelativePath.Equals(pth) && m.StatusId != 0,
			cancellationToken);
		if(fromDb == null)
		{
			throw new NotFoundException(query.ImagePath);
		}
		GetPictureVm mapped = new() { Picture = _mapper.Map<GetPictureDto>(fromDb) };
		if (mapped == null)
		{
			throw new MappingException(nameof(GetPictureDto), new Exception());
		}

		var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", pth);
		using(FileStream fs = new(filePath, FileMode.Open, FileAccess.Read))
		{
			byte[] fileBytes = new byte[fs.Length];
			fs.Read(fileBytes, 0, fileBytes.Length);
			mapped.Picture.FileStream = fileBytes;
		}
		var extension = Path.GetExtension(pth);
		mapped.Picture.ContentType = extension.Replace(".", "image/");
		return mapped;
	}
}