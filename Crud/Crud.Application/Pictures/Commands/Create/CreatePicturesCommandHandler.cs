using Crud.Shared.Pictures.Commands;
using Microsoft.Extensions.Hosting;

namespace Crud.Application.Pictures.Commands;
public class CreatePicturesCommandHandler : IRequestHandler<CreatePicturesCommand, Unit>
{
	private readonly ICrudDbContext _db;
	private readonly IDirectoryWrapper _dir;
	private readonly IHostEnvironment _hostEnvironment;
	public CreatePicturesCommandHandler(ICrudDbContext db, IDirectoryWrapper dir, IHostEnvironment hostEnvironment)
	{
		_db = db;
		_dir = dir;
		_hostEnvironment = hostEnvironment;
	}
	public async Task<Unit> Handle(CreatePicturesCommand request, CancellationToken cancellationToken)
	{
		if (request.Files == null || request.Files.Count == 0)
		{
			throw new NotFoundException("Image not provided");
		}

		var filePathBase = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Images");

		foreach (var formFile in request.Files)
		{
			if (formFile.Length > 0)
			{
				var yearMonth = Path.Combine(DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
				string fullYearMonth = Path.Combine(filePathBase, yearMonth);
				_dir.CreateDirectory(fullYearMonth);
				string picname = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
				string outputFile = Path.Combine(fullYearMonth, picname);
				if (File.Exists(outputFile))
				{
					outputFile = Path.Combine(fullYearMonth, string.Concat("_", Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName)));
				}
				using (var stream = new FileStream(outputFile, FileMode.Create))
				{
					try
					{
						await formFile.CopyToAsync(stream, cancellationToken);
					}
					catch
					{
					}
					finally
					{
						stream.Close();
					}
				}
				_db.Pictures.Add(new Picture() { RelativePath = Path.Combine("Images", yearMonth, picname) });
				//todo
			}
		}
		await _db.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}