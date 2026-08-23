using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Presentation.Menus
{
    public class AdminMenu
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IBookLoansService _bookLoansService;

        public AdminMenu(ICategoryService categoryService, IBookService bookService, IBookLoansService bookLoansService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
            _bookLoansService = bookLoansService;
        }
        
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Admin Menu =====");
                Console.WriteLine("1. Add Category");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. View Categories");
                Console.WriteLine("4. View Books");
                Console.WriteLine("5. View Active Loans");
                Console.WriteLine("0. Logout");

                Console.Write("Select: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateCategory();
                        break;

                    case "2":
                        CreateBook();
                        break;

                    case "3":
                        ShowCategories();
                        break;

                    case "4":
                        ShowBooks();
                        break;
                    case "5":
                        ShowActiveLoans();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option");
                        Console.ReadKey();
                        break;
                }
            }
        }


        private void CreateCategory()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Create Category =====");
                Console.Write("Category name: ");

                var name = Console.ReadLine();


                if (!name.IsValidText())
                {
                    Console.WriteLine("Name is required.");
                    Console.WriteLine("1. Try again");
                    Console.WriteLine("0. Back");

                    var option = Console.ReadLine();

                    if (option == "0")
                        return;

                    continue;
                }


                try
                {
                    _categoryService.CreateCategory(name);

                    Console.WriteLine("Category created successfully.");
                    Console.ReadKey();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    return;
                }
            }
        }


        private void CreateBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Create Book =====");

                Console.Write("Book title: ");
                var title = Console.ReadLine();


                if (!title.IsValidText())
                {
                    Console.WriteLine("Title is required.");
                    Console.WriteLine("1. Try again");
                    Console.WriteLine("0. Back");

                    var option = Console.ReadLine();

                    if (option == "0")
                        return;

                    continue;
                }


                Console.Write("Category Id: ");
                var categoryIdInput = Console.ReadLine();


                if (!int.TryParse(categoryIdInput, out int categoryId))
                {
                    Console.WriteLine("Invalid category id.");
                    Console.ReadKey();
                    continue;
                }


                try
                {
                    _bookService.CreateBook(title, categoryId);

                    Console.WriteLine("Book created successfully.");
                    Console.ReadKey();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    return;
                }
            }
        }


        private void ShowCategories()
        {
            Console.Clear();

            var categories = _categoryService.GetAllCategories();

            ConsolePainter.WriteTable(
                categories,
                ConsoleColor.Blue,
                ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }


        private void ShowBooks()
        {
            Console.Clear();

            var books = _bookService.GetAllBooks();

            ConsolePainter.WriteTable(
                books,
                ConsoleColor.Blue,
                ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
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
