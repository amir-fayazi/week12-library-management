using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Presentation.Menus
{
    public class LoginMenu
    {
        public void Show()
        {
            Console.Clear();

            Console.WriteLine("===== Login =====");
            Console.WriteLine("1. Username");
            Console.WriteLine("2. Password");
            Console.WriteLine("0. Back");

            Console.Write("Select: ");

            var input = Console.ReadLine();

            if (input == "0")
                return;

            // بعداً اینجا AuthService صدا زده می‌شود
        }
    }
}
