

using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public RoleEnum Role { get; private set; }
        public List<BookLoan> BorrowedBooks { get; private set; } = [];

        public User(string username, string password, RoleEnum role)
        {
            ValidateUsername(username);
            ValidatePassword(password);
            ValidateRole(role);

            Username = username;
            Password = password;
            Role = role;
        }




        //---------------------------username
        public void ChangeUsername(string username)
        {
            if (username == Username)
                throw new ValidationException("New username must be different from the current username.");
            ValidateUsername(username);
            Username = username;

        }


        //---------------------------passsword
        public void ChangePassword(string newPassword)
        {
            
            if (newPassword == Password)
                throw new ValidationException("New password must be different from the current password.");

            ValidatePassword(newPassword);

            Password = newPassword;

        }


        //---------------------------Validate
        private void ValidateUsername(string username)
        {
            if (!username.IsValidText())
                throw new ValidationException("Username cannot be empty.");
            if (username.Length < 3)
                throw new ValidationException("Username must be at least 3 characters long.");
            if (username.Length > 30)
                throw new ValidationException("Username cannot exceed 30 characters.");
        }

        private void ValidatePassword(string newPasssword)
        {
            if (!newPasssword.IsValidText())
                throw new ValidationException("newPasssword cannot be empty.");
            if (newPasssword.Length < 6)
                throw new ValidationException("Password must be at least 6 characters long.");
            if (newPasssword.Length > 50)
                throw new ValidationException("Password cannot exceed 50 characters.");
        }
        private void ValidateRole(RoleEnum role)
        {
            if (!Enum.IsDefined(typeof(RoleEnum), role))
                throw new ValidationException("Invalid user role.");
        }
    }

}
