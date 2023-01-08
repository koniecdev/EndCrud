using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Crud.Shared.Pictures.Commands;
public class CreatePicturesCommand : IRequest<Unit>
{
	public CreatePicturesCommand()
	{
		Files = new List<IFormFile>();
	}
	public List<IFormFile> Files { get; set; }
}