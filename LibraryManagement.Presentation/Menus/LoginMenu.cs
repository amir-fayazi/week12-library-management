using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Presentation.Menus
{
    public class LoginMenu
    {
        private readonly IAuthService _authService;

        public LoginMenu(IAuthService authService)
        {
            _authService = authService;
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
                        var adminMenu = new AdminMenu();
                        adminMenu.Show();
                    }
                    else
                    {
                        var userMenu = new UserMenu();
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
