using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Presentation.Menus
{
    public class UserReviewMenu
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;
        private readonly int _userId;

        public UserReviewMenu(
            int userId,
            IReviewService reviewService,
            IBookService bookService)
        {
            _userId = userId;
            _reviewService = reviewService;
            _bookService = bookService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Manage Reviews =====");
                Console.WriteLine("1. Add Review");
                Console.WriteLine("2. View My Reviews");
                Console.WriteLine("3. Change Rating");
                Console.WriteLine("4. Change Comment");
                Console.WriteLine("5. Delete Review");
                Console.WriteLine("0. Back");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddReview();
                        break;

                    case "2":
                        ShowMyReviews();
                        break;

                    case "3":
                        ChangeRating();
                        break;

                    case "4":
                        ChangeComment();
                        break;

                    case "5":
                        DeleteReview();
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

        private void AddReview()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Add Review =====");

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

                Console.Write("Rating (1-5): ");

                if (!int.TryParse(Console.ReadLine(), out int rating))
                {
                    Console.WriteLine("Invalid rating.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.Write("Comment (optional): ");
                var commentInput = Console.ReadLine();

                string? comment =
                    !commentInput.IsValidText()
                        ? null
                        : commentInput;

                try
                {
                    _reviewService.CreateReview(
                        _userId,
                        bookId,
                        comment,
                        rating);

                    Console.WriteLine("Review added successfully.");
                    Console.ReadKey();

                    return;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DuplicateException ex)
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

        private void ShowMyReviews()
        {
            Console.Clear();

            Console.WriteLine("===== My Reviews =====");

            ShowMyReviewsTable();

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private void ChangeRating()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Change Rating =====");

                ShowMyReviewsTable();

                Console.WriteLine();
                Console.Write("Review Id: ");

                if (!int.TryParse(Console.ReadLine(), out int reviewId))
                {
                    Console.WriteLine("Invalid review id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.Write("New Rating (1-5): ");

                if (!int.TryParse(Console.ReadLine(), out int rating))
                {
                    Console.WriteLine("Invalid rating.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _reviewService.ChangeRating(
                        _userId,
                        reviewId,
                        rating);

                    Console.WriteLine("Rating changed successfully.");
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

        private void ChangeComment()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Change Comment =====");

                ShowMyReviewsTable();

                Console.WriteLine();
                Console.Write("Review Id: ");

                if (!int.TryParse(Console.ReadLine(), out int reviewId))
                {
                    Console.WriteLine("Invalid review id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                Console.Write("New Comment (leave empty to remove): ");
                var commentInput = Console.ReadLine();

                string? comment =
                    string.IsNullOrWhiteSpace(commentInput)
                        ? null
                        : commentInput;

                try
                {
                    _reviewService.ChangeComment(
                        _userId,
                        reviewId,
                        comment);

                    Console.WriteLine("Comment changed successfully.");
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

        private void DeleteReview()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Delete Review =====");

                ShowMyReviewsTable();

                Console.WriteLine();
                Console.Write("Review Id: ");

                if (!int.TryParse(Console.ReadLine(), out int reviewId))
                {
                    Console.WriteLine("Invalid review id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _reviewService.DeleteReview(
                        _userId,
                        reviewId);

                    Console.WriteLine("Review deleted successfully.");
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

        private void ShowMyReviewsTable()
        {
            var reviews =
                _reviewService.GetAllUserReviews(_userId);

            ConsolePainter.WriteTable(
                reviews,
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