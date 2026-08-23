using ADO.NetDemoConsoleApp;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Presentation.Menus
{
    public class CategoryMenu
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenu(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Manage Categories =====");
                Console.WriteLine("1. View Categories");
                Console.WriteLine("2. Add Category");
                Console.WriteLine("3. Rename Category");
                Console.WriteLine("4. Delete Category");
                Console.WriteLine("0. Back");

                Console.Write("Select: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowCategories();
                        break;

                    case "2":
                        CreateCategory();
                        break;

                    case "3":
                        RenameCategory();
                        break;

                    case "4":
                        DeleteCategory();
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

            var categories = _categoryService.GetAllCategories();

            ConsolePainter.WriteTable(
                categories,
                ConsoleColor.Blue,
                ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine("Press any key to back...");
            Console.ReadKey();
        }


        private void CreateCategory()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Add Category =====");

                Console.Write("Category name: ");
                var name = Console.ReadLine();

                if (!name.IsValidText())
                {
                    Console.WriteLine("Name is required.");

                    if (AskTryAgain())
                        continue;

                    return;
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

                    if (AskTryAgain())
                        continue;

                    return;
                }
            }
        }


        private void RenameCategory()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Rename Category =====");

                var categories = _categoryService.GetAllCategories();

                ConsolePainter.WriteTable(
                    categories,
                    ConsoleColor.Blue,
                    ConsoleColor.White);

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

                Console.Write("New category name: ");
                var newName = Console.ReadLine();

                if (!newName.IsValidText())
                {
                    Console.WriteLine("Name is required.");

                    if (AskTryAgain())
                        continue;

                    return;
                }

                try
                {
                    _categoryService.ChangeName(categoryId, newName);

                    Console.WriteLine("Category renamed successfully.");
                    Console.ReadKey();

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


        private void DeleteCategory()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== Delete Category =====");

                var categories = _categoryService.GetAllCategories();

                ConsolePainter.WriteTable(
                    categories,
                    ConsoleColor.Blue,
                    ConsoleColor.White);

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

                Console.Write("Are you sure you want to delete this category? (y/n): ");
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
                    _categoryService.DeleteCategory(categoryId);

                    Console.WriteLine("Category deleted successfully.");
                    Console.ReadKey();

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