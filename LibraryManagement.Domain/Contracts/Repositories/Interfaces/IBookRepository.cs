using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Domain.Contracts.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Book Add(Book newBook);


        Book? GetById(int id);
        bool ExistsByTitle(string title);
        List<Book> GetAll();
        List<Book> GetAllAvailable();
        List<Book> GetAllBorrowed();

        void Delete(int id);
        void Update(Book updatedBook);
        
    }
}
