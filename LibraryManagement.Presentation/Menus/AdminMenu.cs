using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Presentation.Menus
{
    public class AdminMenu
    {
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
                Console.WriteLine("0. Logout");

                Console.Write("Select: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        break;

                    case "2":
                        break;

                    case "3":
                        break;

                    case "4":
                        break;

                    case "0":
                        return;
                }
            }
        }
    }
}
