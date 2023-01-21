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
    Task<GetAllPicturesVm> GetAllPictures();
    Task CreatePictures(CreatePicturesCommand command);
    Task DeletePicture(int id);

    Task<GetAllCategoriesVm> GetAllCategories();
    Task<GetCategoryVm> GetCategory(int id);
    Task<int> CreateCategory(CreateCategoryCommand command);
    Task UpdateCategory(UpdateCategoryCommand command);
    Task DeleteCategory(int id);

    Task<GetAllArticlesVm> GetAllArticles();
    Task<GetArticleVm> GetArticle(int id);
    Task<GetCategoriesVm> GetArticleCategories();
    Task<int> CreateArticle(CreateArticleCommand command);
    Task UpdateArticle(UpdateArticleCommand command);
    Task DeleteArticle(int id);

    Task<GetAllMembersVm> GetAllMembers();

}
