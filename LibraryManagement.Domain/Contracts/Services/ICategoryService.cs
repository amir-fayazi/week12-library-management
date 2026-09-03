

using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface ICategoryService
    {
        Category CreateCategory(string name);
        void ChangeName(int categoryId, string name);
        IEnumerable<CategoryListDto> GetAllCategories();
        IEnumerable<CategoryWithBookCountDto> GetCategoriesWithBookCount();
        Category GetCategoryById(int id);
       
        void DeleteCategory(int categoryId);

    }
}
