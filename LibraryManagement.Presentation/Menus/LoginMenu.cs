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
        private readonly AdminMenu _adminMenu;
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IBookLoansService _bookLoansService;

        public LoginMenu(
            IAuthService authService,
            AdminMenu adminMenu,
            ICategoryService categoryService,
            IBookService bookService,
            IBookLoansService bookLoansService)
        {
            _authService = authService;
            _adminMenu = adminMenu;
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

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    User user = _authService.Login(username, password);

                    if (user.Role == RoleEnum.Admin)
                    {
                        _adminMenu.Show();
                    }
                    else
                    {
                        var userMenu = new UserMenu(
                            user.Id,
                            _categoryService,
                            _bookService,
                            _bookLoansService);

                        userMenu.Show();
                    }

                    return;
                }
                catch (InvalidCredentialsException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
            }
        }

        private bool AskTryAgain()
        {
            Console.WriteLine();
            Console.WriteLine("1. Try again");
            Console.WriteLine("0. Back");
            Console.Write("Select: ");

            var option = Console.ReadLine();

            return option == "1";
        }
    }
}