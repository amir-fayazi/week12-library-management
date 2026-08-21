
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
            Console.WriteLine("Login selected");
            break;

        case "2":
            Console.WriteLine("Register selected");
            break;

        case "3":
            return;

        default:
            Console.WriteLine("Invalid option");
            break;
    }

    Console.WriteLine("Press any key...");
    Console.ReadKey();
}