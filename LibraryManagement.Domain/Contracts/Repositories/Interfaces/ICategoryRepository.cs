

using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category Add(Category newCategory);

        Category? GetById(int id);
        bool ExistsByName(string name);

        List<Category> GetAll();
        List<Book> GetBooksByCategoryName(string categoryName);

        void Update(Category updatedCategory);
        void Delete(int id);


    }
}
