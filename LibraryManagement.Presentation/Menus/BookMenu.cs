using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Presentation.Menus
{
    public class BookMenu
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BookMenu(
            IBookService bookService,
            ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }


        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Manage Books =====");
                Console.WriteLine("1. View Books");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Edit Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("0. Back");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowBooks();
                        break;

                    case "2":
                        CreateBook();
                        break;

                    case "3":
                        ShowEditMenu();
                        break;

                    case "4":
                        DeleteBook();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");

                        if (!AskTryAgain())
                            return;

                        break;
                }
            }
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


        private void CreateBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Add Book =====");

                Console.Write("Book title: ");
                var title = Console.ReadLine();

                if (!title.IsValidText())
                {
                    Console.WriteLine("Title is required.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.WriteLine();
                ShowCategoriesTable();

                Console.WriteLine();
                Console.Write("Category Id: ");

                var categoryIdInput = Console.ReadLine();

                if (!int.TryParse(categoryIdInput, out int categoryId))
                {
                    Console.WriteLine("Invalid category id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _bookService.CreateBook(title, categoryId);

                    Console.WriteLine("Book created successfully.");
                    Console.ReadKey();

                    return;
                }
                catch (DuplicateException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
            }
        }


        private void ShowEditMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Edit Book =====");
                Console.WriteLine("1. Change Title");
                Console.WriteLine("2. Change Category");
                Console.WriteLine("0. Back");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ChangeTitle();
                        break;

                    case "2":
                        ChangeCategory();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");

                        if (!AskTryAgain())
                            return;

                        break;
                }
            }
        }


        private void ChangeTitle()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Change Book Title =====");

                ShowBooksTable();

                Console.WriteLine();
                Console.Write("Book Id: ");

                var bookIdInput = Console.ReadLine();

                if (!int.TryParse(bookIdInput, out int bookId))
                {
                    Console.WriteLine("Invalid book id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.Write("New title: ");
                var newTitle = Console.ReadLine();

                if (!newTitle.IsValidText())
                {
                    Console.WriteLine("Title is required.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _bookService.ChangeTitle(bookId, newTitle);

                    Console.WriteLine("Book title changed successfully.");
                    Console.ReadKey();

                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
            }
        }


        private void ChangeCategory()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Change Book Category =====");

                ShowBooksTable();

                Console.WriteLine();
                Console.Write("Book Id: ");

                var bookIdInput = Console.ReadLine();

                if (!int.TryParse(bookIdInput, out int bookId))
                {
                    Console.WriteLine("Invalid book id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.WriteLine();
                ShowCategoriesTable();

                Console.WriteLine();
                Console.Write("New Category Id: ");

                var categoryIdInput = Console.ReadLine();

                if (!int.TryParse(categoryIdInput, out int categoryId))
                {
                    Console.WriteLine("Invalid category id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _bookService.ChangeCategory(bookId, categoryId);

                    Console.WriteLine("Book category changed successfully.");
                    Console.ReadKey();

                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
            }
        }


        private void DeleteBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Delete Book =====");

                ShowBooksTable();

                Console.WriteLine();
                Console.Write("Book Id: ");

                var bookIdInput = Console.ReadLine();

                if (!int.TryParse(bookIdInput, out int bookId))
                {
                    Console.WriteLine("Invalid book id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.Write(
                    "Are you sure you want to delete this book? (y/n): ");

                var confirmation = Console.ReadLine();

                if (confirmation?.ToLower() == "n")
                    return;

                if (confirmation?.ToLower() != "y")
                {
                    Console.WriteLine("Invalid option.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _bookService.DeleteBook(bookId);

                    Console.WriteLine("Book deleted successfully.");
                    Console.ReadKey();

                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (BusinessRuleException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    if (AskTryAgain())
                        continue;

                    return;
                }
            }
        }


        private void ShowBooksTable()
        {
            var books = _bookService.GetAllBooks();

            ConsolePainter.WriteTable(
                books,
                ConsoleColor.Blue,
                ConsoleColor.White);
        }


        private void ShowCategoriesTable()
        {
            var categories = _categoryService.GetAllCategories();

            ConsolePainter.WriteTable(
                categories,
                ConsoleColor.Blue,
                ConsoleColor.White);
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