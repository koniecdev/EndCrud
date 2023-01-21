using Crud.Shared.Articles.Commands;
using Crud.Shared.Articles.Queries;
using Crud.Shared.Categories.Commands;
using Crud.Shared.Categories.Queries;
using Crud.Shared.Members.Queries;
using Crud.Shared.Pictures.Commands;
using Crud.Shared.Pictures.Queries;

namespace CrudApp;

public interface ICrudClient
{
    Task RefreshToken(string refreshToken);

    Task<GetAllPicturesVm> GetAllPictures(string accessToken);
    Task CreatePictures(CreatePicturesCommand command, string accessToken);
    Task DeletePicture(int id, string accessToken);

    Task<GetAllCategoriesVm> GetAllCategories(string accessToken);
    Task<GetCategoryVm> GetCategory(int id, string accessToken);
    Task<int> CreateCategory(CreateCategoryCommand command, string accessToken);
    Task UpdateCategory(UpdateCategoryCommand command, string accessToken);
    Task DeleteCategory(int id, string accessToken);

    Task<GetAllArticlesVm> GetAllArticles(string accessToken);
    Task<GetArticleVm> GetArticle(int id, string accessToken);
    Task<GetCategoriesVm> GetArticleCategories(string accessToken);
    Task<int> CreateArticle(CreateArticleCommand command, string accessToken);
    Task UpdateArticle(UpdateArticleCommand command, string accessToken);
    Task DeleteArticle(int id, string accessToken);

    Task<GetAllMembersVm> GetAllMembers(string accessToken);

}
