using Crud.Application.Pictures.Commands;
using Crud.Shared.Pictures.Commands;
using System.Collections.Generic;
using System.IO;

namespace Crud.UnitTests.Pictures.Commands;
public class CreatePicturesCommandHandlerTest : CommandTestBase
{
	//private readonly CreatePicturesCommandHandler _handler;
	//public CreatePicturesCommandHandlerTest()
	//{
	//	_handler = new(_db, _mapper);
	//}

	//[Fact]
	//public async Task CreatePicture()
	//{
	//	var filePathBase = "C:\\Users\\Ariteku\\source\\repos\\crud\\Crud\\Crud\\wwwroot\\Images\\http.png";
	//	var memStr = new MemoryStream(File.ReadAllBytes(filePathBase));
	//	FormFile file1 = new(memStr, 0, memStr.Length, "image-1.png", "image-1.png");
	//	FormFile file2 = new(memStr, 0, memStr.Length, "image-2.png", "image-2.png");
	//	FormFile file3 = new(memStr, 0, memStr.Length, "image-3.png", "image-3.png");
	//	CreatePicturesCommand command = new()
	//	{
	//		Files = new List<IFormFile>()
	//		{
	//			file1, file2, file3
	//		}
	//	};
	//	var returnedIds = await _handler.Handle(command, _token);
	//	var fromDb = await _db.Pictures.ToListAsync();
	//	fromDb.Count.ShouldBe(3);
	//}
}