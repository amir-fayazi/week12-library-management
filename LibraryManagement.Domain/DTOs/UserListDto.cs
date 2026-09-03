using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.DTOs
{
    public class UserListDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public RoleEnum Role { get; set; }
    }
}