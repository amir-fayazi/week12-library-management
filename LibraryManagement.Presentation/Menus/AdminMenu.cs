using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;

namespace LibraryManagement.Presentation.Menus
{
    public class AdminMenu
    {
        private readonly CategoryMenu _categoryMenu;
        private readonly BookMenu _bookMenu;
        private readonly AdminReviewMenu _adminReviewMenu;
        private readonly IBookLoansService _bookLoansService;

        public AdminMenu(
            CategoryMenu categoryMenu,
            BookMenu bookMenu,
            AdminReviewMenu adminReviewMenu,
            IBookLoansService bookLoansService)
        {
            _categoryMenu = categoryMenu;
            _bookMenu = bookMenu;
            _adminReviewMenu = adminReviewMenu;
            _bookLoansService = bookLoansService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Admin Menu =====");
                Console.WriteLine("1. Manage Categories");
                Console.WriteLine("2. Manage Books");
                Console.WriteLine("3. Manage Reviews");
                Console.WriteLine("4. View Active Loans");
                Console.WriteLine("0. Logout");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _categoryMenu.Show();
                        break;

                    case "2":
                        _bookMenu.Show();
                        break;

                    case "3":
                        _adminReviewMenu.Show();
                        break;

                    case "4":
                        ShowActiveLoans();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowActiveLoans()
        {
            Console.Clear();

            Console.WriteLine("===== Active Loans =====");

            var activeLoans = _bookLoansService.GetActiveLoans();

            ConsolePainter.WriteTable(
                activeLoans,
                ConsoleColor.Blue,
                ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }
    }
}