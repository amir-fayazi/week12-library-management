using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;


namespace LibraryManagement.Infrastructure.Repositories.EfCore
{
    public class EfUserRepository : IUserRepository
    {

        private readonly AppDbContext _context;
        public EfUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User Add(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user is null)
                throw new NotFoundException("User not found for deletion.");
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public bool ExistsByUsername(string username)
        {
            return _context.Users.Any(p => p.Username == username);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(p => p.Username == username);
        }

        public void Update(User updatedUser)
        {
            _context.Users.Update(updatedUser);
            _context.SaveChanges();

        }
    }
}
