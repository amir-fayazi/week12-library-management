using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Presentation.Menus
{
    public class UserMenu
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IBookLoansService _bookLoansService;
        private readonly UserReviewMenu _userReviewMenu;
        private readonly WishlistMenu _wishlistMenu;
        private readonly IUserService _userService;

        private readonly int _userId;

        public UserMenu(
            int userId,
            ICategoryService categoryService,
            IBookService bookService,
            IBookLoansService bookLoansService,
            UserReviewMenu userReviewMenu,
            WishlistMenu wishlistMenu,
            IUserService userService)
        {
            _userId = userId;
            _categoryService = categoryService;
            _bookService = bookService;
            _bookLoansService = bookLoansService;
            _userReviewMenu = userReviewMenu;
            _wishlistMenu = wishlistMenu;
            _userService = userService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== User Menu =====");
                Console.WriteLine("1. View Categories");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. View My Loans");
                Console.WriteLine("4. Borrow Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. Manage Reviews");
                Console.WriteLine("7. Manage Wishlist");
                Console.WriteLine("8. View Profile");
                Console.WriteLine("0. Logout");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowCategories();
                        break;

                    case "2":
                        ShowBooks();
                        break;

                    case "3":
                        ShowMyLoans();
                        break;

                    case "4":
                        BorrowBook();
                        break;

                    case "5":
                        ReturnBook();
                        break;

                    case "6":
                        _userReviewMenu.Show();
                        break;

                    case "7":
                        _wishlistMenu.Show();
                        break;
                    case "8":
                        ShowProfile();
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

        private void ShowCategories()
        {
            Console.Clear();

            var categories =
                _categoryService.GetAllCategories();

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

            ShowBooksTable();

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private void BorrowBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Borrow Book =====");

                ShowAvailableBooksTable();

                Console.WriteLine();
                Console.Write("Book Id: ");

                var bookIdInput = Console.ReadLine();

                if (!int.TryParse(
                    bookIdInput,
                    out int bookId))
                {
                    Console.WriteLine("Invalid book id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _bookLoansService.CreateLoanBook(
                        _userId,
                        bookId);

                    Console.WriteLine(
                        "Book borrowed successfully.");

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

        private void ShowMyLoans()
        {
            Console.Clear();

            ShowUserLoansTable();

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private void ReturnBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Return Book =====");

                ShowUserLoansTable();

                Console.WriteLine();
                Console.Write("Book Loan Id: ");

                var loanIdInput = Console.ReadLine();

                if (!int.TryParse(
                    loanIdInput,
                    out int bookLoanId))
                {
                    Console.WriteLine(
                        "Invalid book loan id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _bookLoansService.ReturnBook(
                        _userId,
                        bookLoanId);

                    Console.WriteLine(
                        "Book returned successfully.");

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

        private void ShowAvailableBooksTable()
        {
            var availableBooks =
                _bookService.GetAllAvailableBooks();

            ConsolePainter.WriteTable(
                availableBooks,
                ConsoleColor.Blue,
                ConsoleColor.White);
        }

        private void ShowUserLoansTable()
        {
            var loans =
                _bookLoansService.GetUserLoans(_userId);

            ConsolePainter.WriteTable(
                loans,
                ConsoleColor.Blue,
                ConsoleColor.White);
        }

        private void ShowProfile()
        {
            Console.Clear();

            Console.WriteLine("===== My Profile =====");

            var profile = _userService.GetProfile(_userId);

            Console.WriteLine($"User Id: {profile.UserId}");
            Console.WriteLine($"Username: {profile.Username}");
            Console.WriteLine($"Role: {profile.Role}");
            Console.WriteLine($"Unpaid Penalty: {profile.PenaltyAmount:N0} Toman");

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private bool AskTryAgain()
        {
            Console.WriteLine();
            Console.WriteLine("1. Try again");
            Console.WriteLine("0. Back");
            Console.Write("Select: ");

            return Console.ReadLine() == "1";
        }
    }
}