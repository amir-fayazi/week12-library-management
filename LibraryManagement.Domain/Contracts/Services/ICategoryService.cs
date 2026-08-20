

using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface ICategoryService
    {
        void CreateCategory(string name);
        void ChangeName(int categoryId, string name);
        List<Category> GetAllCategories();
        List<Category> GetCategoriesWithBookCount();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        void DeleteCategory(int categoryId);
    }
}
