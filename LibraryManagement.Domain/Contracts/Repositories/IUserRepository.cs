using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        User Add(User newUser);

        User GetById(int id);
        User? GetByUsername(string username);
        bool ExistsByUsername(string username);
        List<User> GetAll();

        void Update(User updatedUser);

        void Delete(int id);
    }
}
