using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;


namespace LibraryManagement.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public void ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = GetById(userId);

            if (user.Password != currentPassword)
                throw new ValidationException("The current password is incorrect.");

            user.ChangePassword(newPassword);
            _userRepo.Update(user);
        }

        public void ChangeUsername(int userId, string username)
        {
            var user = GetById(userId);
            user.ChangeUsername(username);
            _userRepo.Update(user);
        }

        public void Delete(int id)
        {
             var user = GetById(id);
            if (user.BookLoans.Any())
                throw new BusinessRuleException("Cannot delete user because loan history exists.");

            _userRepo.Delete(id);
        }

        public List<User> GetAll()
        {
            return _userRepo.GetAll();
        }

        public User GetById(int userId)
        {
            var user =  _userRepo.GetById(userId);
            return user is null ? throw new NotFoundException("User not found") : user;
        }

        public User GetByUsername(string username)
        {
            var user = _userRepo.GetByUsername(username);
            
           return  user is null ? throw new NotFoundException("User not found") : user;
            
        }
    }
}
