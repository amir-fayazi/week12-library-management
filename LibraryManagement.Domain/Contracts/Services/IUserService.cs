using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IUserService
    {
        User GetById(int userId);
        User? GetByUsername(string username);
        List<User> GetAll();

        void ChangeUsername(int userId, string username);
        void ChangePassword(int userId, string currentPassword, string newPassword);

        void Delete(int id);
    }
}

