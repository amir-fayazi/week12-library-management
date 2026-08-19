using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IBookService
    {
        Book Create(string title, Category category);

        Book GetById(int bookId);
        List<Book> GetAll();
        List<Book> GetBooksByCategory(int categoryId);
        void ChangeTitle(int bookId, string title);
        void ChangeCategory(int bookId, int categoryId);
        void Delete(int bookId);
    }
}
