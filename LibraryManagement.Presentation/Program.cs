using LibraryManagement.Application.Contracts.Services;
using LibraryManagement.Application.Services.Implementations;
using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories.EfCore;
using LibraryManagement.Presentation.Menus;

var context = new AppDbContext();

IBookRepository bookRepository = new EfBookRepository(context);
ICategoryRepository categoryRepository = new EfCategoryRepository(context);
IUserRepository userRepository = new EfUserRepository(context);
IBookLoanRepository bookLoanRepository = new EfBookLoanRepository(context);


IBookService bookService = new BookService(
    bookRepository,
    categoryRepository);

ICategoryService categoryService = new CategoryService(
    categoryRepository);

IBookLoansService bookLoansService = new BookLoansService(
    bookLoanRepository,
    userRepository,
    bookRepository);

IAuthService authService = new AuthService(
    userRepository);


var loginMenu = new LoginMenu(
    authService,
    categoryService,
    bookService,
    bookLoansService);

var registerMenu = new RegisterMenu(authService);

while (true)
{
    Console.Clear();

    Console.WriteLine("===== Library Management =====");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");
    Console.WriteLine("3. Exit");

    Console.Write("Select an option: ");

    var input = Console.ReadLine();


    switch (input)
    {
        case "1":
            loginMenu.Show();
            break;


        case "2":
            registerMenu.Show();
            Console.ReadKey();
            break;


        case "3":
            return;


        default:
            Console.WriteLine("Invalid option");
            Console.ReadKey();
            break;
    }
}