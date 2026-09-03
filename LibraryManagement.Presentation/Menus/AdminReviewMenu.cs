using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Presentation.Menus
{
    public class AdminReviewMenu
    {
        private readonly IReviewService _reviewService;

        public AdminReviewMenu(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Manage Reviews =====");
                Console.WriteLine("1. View Pending Reviews");
                Console.WriteLine("2. Approve Review");
                Console.WriteLine("3. Reject Review");
                Console.WriteLine("0. Back");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowPendingReviews();
                        break;

                    case "2":
                        ApproveReview();
                        break;

                    case "3":
                        RejectReview();
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

        private void ShowPendingReviews()
        {
            Console.Clear();

            Console.WriteLine("===== Pending Reviews =====");

            ShowPendingReviewsTable();

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }

        private void ApproveReview()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Approve Review =====");

                ShowPendingReviewsTable();

                Console.WriteLine();
                Console.Write("Review Id: ");

                var reviewIdInput = Console.ReadLine();

                if (!int.TryParse(reviewIdInput, out int reviewId))
                {
                    Console.WriteLine("Invalid review id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _reviewService.ApproveReview(reviewId);

                    Console.WriteLine("Review approved successfully.");
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

        private void RejectReview()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Reject Review =====");

                ShowPendingReviewsTable();

                Console.WriteLine();
                Console.Write("Review Id: ");

                var reviewIdInput = Console.ReadLine();

                if (!int.TryParse(reviewIdInput, out int reviewId))
                {
                    Console.WriteLine("Invalid review id.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _reviewService.RejectReview(reviewId);

                    Console.WriteLine("Review rejected successfully.");
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

        private void ShowPendingReviewsTable()
        {
            var reviews = _reviewService.GetPendingReviews();

            ConsolePainter.WriteTable(
                reviews,
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