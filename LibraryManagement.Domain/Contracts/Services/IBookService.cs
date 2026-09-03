using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IBookService
    {
        Book CreateBook(string title, int categoryId);

        Book GetBookById(int bookId);
        List<Book> GetAllBooks();
        List<Book> GetAllAvailableBooks();
        List<Book> GetBooksByCategory(int categoryId);
        void ChangeTitle(int bookId, string title);
        void ChangeCategory(int bookId, int categoryId);
        void DeleteBook(int bookId);
        BookResultDto GetBookDetails(int bookId);
    }
}
