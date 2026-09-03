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
        private readonly IReviewService _reviewService;
        private readonly Func<int, UserMenu> _userMenuFactory;
        public LoginMenu(
            IAuthService authService,
            AdminMenu adminMenu,
            Func<int, UserMenu> userMenuFactory
            )
        {
            _authService = authService;
            _adminMenu = adminMenu;
            _userMenuFactory = userMenuFactory;
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
                    Console.WriteLine(
                        "Username and password are required.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    User user =
                        _authService.Login(username, password);

                    if (user.Role == RoleEnum.Admin)
                    {
                        _adminMenu.Show();
                    }
                    else
                    {
                        var userMenu = _userMenuFactory(user.Id);
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