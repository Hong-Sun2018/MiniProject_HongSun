using System;

namespace MiniProject_HongSun
{
    internal class Program
    {

        static void Main(string[] args)
        {

            DeviceList devices = new();

            string input;
            while (true)
            {
                Console.WriteLine();
                Utilities.PrintLineColor("Choose the options by entering one of the following letters: ", ConsoleColor.White);
                Console.WriteLine(" m - Add Mobile Phone");
                Console.WriteLine(" l - Add Laptop");
                Console.WriteLine(" e - Exit Program");
                Console.Write("Your choice: ");
                input = Console.ReadLine().ToLower().Trim();
                Console.WriteLine();

                switch (input)
                {
                    case "m":
                        devices.AddMobilePhone();
                        break;
                    case "l":
                        devices.AddLaptop();
                        break;
                    case "e":
                        return;
                    default:
                        Utilities.PrintLineColor("Invalid inpout.", ConsoleColor.Red);
                        break;
                }
            }
        }
    }
}
