using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Presentation.Menus
{
    public class UserMenu
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== User Menu =====");
                Console.WriteLine("1. View Categories");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. View My Borrowed Books");
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
