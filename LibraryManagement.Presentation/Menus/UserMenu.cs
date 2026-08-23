using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Presentation.Menus
{
    public class UserMenu
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IBookLoansService _bookLoansService;

        private readonly int _userId;


        public UserMenu(
            int userId,
            ICategoryService categoryService,
            IBookService bookService,
            IBookLoansService bookLoansService)
        {
            _userId = userId;
            _categoryService = categoryService;
            _bookService = bookService;
            _bookLoansService = bookLoansService;
        }


        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== User Menu =====");
                Console.WriteLine("1. View Categories");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. View My Loans");
                Console.WriteLine("5. Return Book");
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
                        BorrowBook();
                        break;

                    case "4":
                        ShowMyLoans();
                        break;
                    case "5":
                        ReturnBook();
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


        private void BorrowBook()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Borrow Book =====");


                var availableBooks = _bookService.GetAllAvailableBooks();

                ConsolePainter.WriteTable(
                    availableBooks,
                    ConsoleColor.Blue,
                    ConsoleColor.White);


                Console.WriteLine();

                Console.Write("Book Id: ");
                var bookIdInput = Console.ReadLine();


                if (!int.TryParse(bookIdInput, out int bookId))
                {
                    Console.WriteLine("Invalid book id.");
                    Console.ReadKey();
                    continue;
                }


                try
                {
                    _bookLoansService.CreateLoanBook(_userId, bookId);

                    Console.WriteLine("Book borrowed successfully.");
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


        private void ShowMyLoans()
        {
            Console.Clear();

            var loans = _bookLoansService.GetUserLoans(_userId);


            ConsolePainter.WriteTable(
                loans,
                ConsoleColor.Blue,
                ConsoleColor.White);


            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private void ReturnBook()
        {
            Console.Clear();

            Console.WriteLine("===== Return Book =====");

            var loans = _bookLoansService.GetUserLoans(_userId);

            ConsolePainter.WriteTable(
                loans,
                ConsoleColor.Blue,
                ConsoleColor.White);

            Console.WriteLine();

            Console.Write("Book Loan Id: ");
            var loanIdInput = Console.ReadLine();

            if (!int.TryParse(loanIdInput, out int bookLoanId))
            {
                Console.WriteLine("Invalid book loan id.");
                Console.ReadKey();
                return;
            }

            try
            {
                _bookLoansService.ReturnBook(_userId, bookLoanId);

                Console.WriteLine("Book returned successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}

