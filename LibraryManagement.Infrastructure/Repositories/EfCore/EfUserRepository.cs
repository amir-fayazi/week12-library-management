using LibraryManagement.Domain.Contracts.Repositories.Interfaces;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Repositories.EfCore
{
    public class EfUserRepository : IUserRepository
    {
        public User Add(User newUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Update(User updatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
