using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;


namespace LibraryManagement.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User Login(string username, string password)
        {
            var user = _userRepo.GetByUsername(username);

            if (user is null || user.Password != password)
                throw new InvalidCredentialsException(
                    "Username or password is invalid.");

            return user;
        }

        public void Register(string username, string password)
        {
            var newUser = new User(username, password, RoleEnum.User);

            if (_userRepo.ExistsByUsername(username))
                throw new DuplicateException("Username is already exists");

            _userRepo.Add(newUser);
        }
    }
}
