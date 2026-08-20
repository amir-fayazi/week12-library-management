using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IBookService
    {
        Book CreateBook(string title, int categoryId);

        Book GetBookById(int bookId);
        List<Book> GetAllBook();
        List<Book> GetBooksByCategory(int categoryId);
        void ChangeTitle(int bookId, string title);
        void ChangeCategory(int bookId, int categoryId);
        void DeleteBook(int bookId);
        
    }
}
