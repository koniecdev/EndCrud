using Crud.Shared.Pictures.Commands;
using Crud.Shared.Pictures.Queries;

namespace CrudApp;

public interface ICrudClient
{
    Task<GetAllPicturesVm> GetAllPictures(string accessToken);
    Task CreatePictures(CreatePicturesCommand command);
}
