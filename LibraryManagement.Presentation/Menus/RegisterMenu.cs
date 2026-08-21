using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Presentation.Menus;

public class RegisterMenu
{
    private readonly IAuthService _authService;

    public RegisterMenu(IAuthService authService)
    {
        _authService = authService;
    }


    public void Show()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== Register =====");

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
                _authService.Register(username, password);

                Console.WriteLine("Register completed successfully.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                return;
            }
            catch (DuplicateException ex)
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