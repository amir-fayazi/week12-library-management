using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;


namespace LibraryManagement.Presentation.Menus
{
    public class LoginMenu
    {
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IBookLoansService _bookLoansService;
        public LoginMenu(
            IAuthService authService, ICategoryService categoryService, IBookService bookService, IBookLoansService bookLoansService
            )
        {
            _authService = authService;
            _categoryService = categoryService;
            _bookService = bookService;
            _bookLoansService = bookLoansService;
        }
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Login =====");

                Console.Write("Username: ");
                var username = Console.ReadLine();

                Console.Write("Password: ");
                var password = Console.ReadLine();

                if (!username.IsValidText() || !password.IsValidText())
                {
                    Console.WriteLine("Username and password are required.");
                    Console.WriteLine();
                    Console.WriteLine("1. Try again");
                    Console.WriteLine("0. Back");

                    var option = Console.ReadLine();

                    if (option == "0")
                        return;

                    continue;
                }

                try
                {
                    User user = _authService.Login(username, password);

                    Console.Clear();

                    if (user.Role == RoleEnum.Admin)
                    {
                        var adminMenu = new AdminMenu(_categoryService, _bookService);
                        adminMenu.Show();
                    }
                    else
                    {
                        var userMenu = new UserMenu(user.Id, _categoryService, _bookService, _bookLoansService);
                        userMenu.Show();
                    }

                    return;
                }
                catch (InvalidCredentialsException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();

                    Console.WriteLine("1. Try again");
                    Console.WriteLine("0. Back");

                    var option = Console.ReadLine();

                    if (option == "0")
                        return;
                }
            }
        }
    }
}
