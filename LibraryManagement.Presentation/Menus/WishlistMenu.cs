using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Presentation.Menus
{
    public class WishlistMenu
    {
        private readonly IWishListService _wishlistService;
        private readonly IBookService _bookService;
        private readonly int _userId;

        public WishlistMenu(
            int userId,
            IWishListService wishlistService,
            IBookService bookService)
        {
            _userId = userId;
            _wishlistService = wishlistService;
            _bookService = bookService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Manage Wishlist =====");
                Console.WriteLine("1. Add Book To Wishlist");
                Console.WriteLine("2. View My Wishlist");
                Console.WriteLine("3. Remove Book From Wishlist");
                Console.WriteLine("0. Back");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddToWishlist();
                        break;

                    case "2":
                        ShowMyWishlist();
                        break;

                    case "3":
                        RemoveFromWishlist();
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

        private void AddToWishlist()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Add To Wishlist =====");

                ShowBooksTable();

                Console.WriteLine();
                Console.Write("Book Id: ");

                if (!int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Console.WriteLine("Invalid book id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _wishlistService.CreateWishlist(
                        _userId,
                        bookId);

                    Console.WriteLine(
                        "Book added to wishlist successfully.");

                    Console.ReadKey();
                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (BusinessRuleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (!AskTryAgain())
                    return;
            }
        }

        private void ShowMyWishlist()
        {
            Console.Clear();

            Console.WriteLine("===== My Wishlist =====");

            ShowWishlistTable();

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private void RemoveFromWishlist()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Remove From Wishlist =====");

                ShowWishlistTable();

                Console.WriteLine();
                Console.Write("Wishlist Id: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int wishlistId))
                {
                    Console.WriteLine("Invalid wishlist id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _wishlistService.DeleteWishlist(
                        _userId,
                        wishlistId);

                    Console.WriteLine(
                        "Book removed from wishlist successfully.");

                    Console.ReadKey();
                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (BusinessRuleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (!AskTryAgain())
                    return;
            }
        }

        private void ShowWishlistTable()
        {
            var wishlist =
                _wishlistService.GetUserWishlist(_userId);

            ConsolePainter.WriteTable(
                wishlist,
                ConsoleColor.Blue,
                ConsoleColor.White);
        }

        private void ShowBooksTable()
        {
            var books = _bookService.GetAllBooks();

            ConsolePainter.WriteTable(
                books,
                ConsoleColor.Blue,
                ConsoleColor.White);
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