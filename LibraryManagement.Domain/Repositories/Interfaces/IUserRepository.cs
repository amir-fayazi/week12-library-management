using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Add(User newUser);

        User? GetById(int id);
        User? GetByUsername(string username);
        bool ExistsByUsername(string username);
        List<User> GetAll();
        //List<Book> GetLoanBooksByUsername(string username); 

        void Update(User updatedUser);

        void Delete(int id);
    }
}
