using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject_HongSun
{
    internal class DeviceList
    {
        private List<Device> _deviceList = new();
        private CurrencyExchange ce = new();

        public void AddMobilePhone()
        {
            Utilities.PrintLineColor("Add Mobile Phone: ", ConsoleColor.White);
            while (true)
            {
                // QUIT enter product if 'q' is entered
                Console.Write("Input the BRAND of a mobile phone, enter 'q' to quit:   ");
                string brand = Console.ReadLine().Trim();
                if (this.IsQ(brand))
                    return;

                Console.Write("Input the MODEL of the mobile phone, enter 'q' to quit:   ");
                string model = Console.ReadLine().Trim();
                if (this.IsQ(model))
                    return;

                Console.Write("Input the PURCHASE DATE of the mobile phone, format 'yyyy-MM-dd', enter 'q' to quit: ");
                string date = Console.ReadLine().Trim();
                if (this.IsQ(date))
                    return;


                Console.Write("Input the PRICE of the mobile phone in USD, enter 'q' to quit:   ");
                string price = Console.ReadLine().Trim();
                if (this.IsQ(price))
                    return;

                Console.WriteLine("Select the OFFICE LOCATION of the mobile phone:");
                Console.WriteLine(" q - Quit Input ");
                Console.WriteLine(" 1 - Denmark ");
                Console.WriteLine(" 2 - Sweden ");
                Console.WriteLine(" 3 - Norway ");
                Console.WriteLine(" 4 - Finland ");
                Console.WriteLine(" Other input - Other");
                Console.Write("Enter your selection: ");
                string selection = Console.ReadLine().Trim();
                if (this.IsQ(selection))
                    return;
                Location location = this.GetLocation(selection);
                

                    Console.Write("Input the NUMBER of the mobile phone, enter 'q' to quit:   ");
                string number = Console.ReadLine().Trim();
                if (this.IsQ(number))
                    return;

                // Add product to the list.
                try
                {
                    MobilePhone mobile = new MobilePhone(brand.ToUpper(), model.ToUpper(), date, double.Parse(price), number, location);
                    this._deviceList.Add(mobile);
                    Utilities.PrintLineColor("The mobile phone has been added to the product list.", ConsoleColor.Green);
                    Console.WriteLine();
                }
                catch (FormatException e)
                {
                    Utilities.PrintLineColor(e.Message, ConsoleColor.Red);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Utilities.PrintLineColor(e.Message, ConsoleColor.Red);
                }
            }
        }

        public void AddLaptop()
        {
            Utilities.PrintLineColor("Add Laptop: ", ConsoleColor.White);
            while (true)
            {
                // QUIT enter product if 'q' is entered
                Console.Write("Input the BRAND of a laptop, enter 'q' to quit:   ");
                string brand = Console.ReadLine().Trim();
                if (this.IsQ(brand))
                    return;

                Console.Write("Input the MODEL of the laptop, enter 'q' to quit:   ");
                string model = Console.ReadLine().Trim();
                if (this.IsQ(model))
                    return;

                Console.Write("Input the PURCHASE DATE of the laptop, format 'yyyy-MM-dd', enter 'q' to quit: ");
                string date = Console.ReadLine().Trim();
                if (this.IsQ(date))
                    return;


                Console.Write("Input the PRICE of the laptop in USD, enter 'q' to quit:   ");
                string price = Console.ReadLine().Trim();
                if (this.IsQ(price))
                    return;

                Console.WriteLine("Select the OFFICE LOCATION of the laptop:");
                Console.WriteLine(" q - Quit Input ");
                Console.WriteLine(" 1 - Denmark ");
                Console.WriteLine(" 2 - Sweden ");
                Console.WriteLine(" 3 - Norway ");
                Console.WriteLine(" 4 - Finland ");
                Console.WriteLine(" Other input - Other Location");
                Console.Write("Enter your selection: ");
                string selection = Console.ReadLine().Trim();
                if (this.IsQ(selection))
                    return;
                Location location = this.GetLocation(selection);


                Console.Write("Input the SCREEN SIZE of the laptop, enter 'q' to quit:   ");
                string number = Console.ReadLine().Trim();
                if (this.IsQ(number))
                    return;

                // Add product to the list.
                try
                {
                    Laptop laptop = new Laptop(brand.ToUpper(), model.ToUpper(), date, double.Parse(price), Int32.Parse(number), location);
                    this._deviceList.Add(laptop);
                    Utilities.PrintLineColor("The laptop has been added to the product list.", ConsoleColor.Green);
                    Console.WriteLine();
                }
                catch (FormatException e)
                {
                    Utilities.PrintLineColor(e.Message, ConsoleColor.Red);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Utilities.PrintLineColor(e.Message, ConsoleColor.Red);
                }
            }
        }

        

        private Location GetLocation(string selection)
        {
            Location location;
            switch (selection.Trim())
            {
                case "1":
                    location = Location.Denmark;
                    break;
                case "2":
                    location = Location.Sweden;
                    break;
                case "3":
                    location = Location.Norway;
                    break;
                case "4":
                    location = Location.Finland;
                    break;
                default:
                    location = Location.Other;
                    break;
            }
            return location;
        }

        private bool IsQ(string input)
        {
            if (input.ToLower().Trim() == "q")
            {
                Utilities.PrintLineColor("User quit input.", ConsoleColor.Yellow);
                Console.WriteLine();
                this.PrintList();
                return true;
            }
            else
                return false;
        }

        private List<Device> Sort()
        {
            List<Device> sortedList = new();

            // sort office location first
            List<Location> sortedLocations = new List<Location>{
                Location.Sweden,
                Location.Norway,
                Location.Denmark,
                Location.Other,
                Location.Finland
            }.OrderBy(location => location.ToString()).ToList();

            // then sort purchase date        
            foreach (Location location in sortedLocations)
            {
                List<Device> temp = this._deviceList.Where(device => device.Location == location).ToList();
                temp = temp.OrderBy(device => device.PurchaseDate.Second).ToList();
                foreach (Device device in temp)
                {
                    sortedList.Add(device);
                }
            }

            return sortedList;
        }

        public void PrintList()
        {
            Utilities.PrintLineColor("Print All Products: ", ConsoleColor.White);
            DateTime current = DateTime.Now;
            if (this._deviceList.Count > 0)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine(
                    "|" + "Type".PadRight(15) + 
                    "|" + "Brand".PadRight(15) +
                    "|" + "Model".PadRight(15) +
                    "|" + "Price".PadRight(15) +
                    "|" + "Purch Date".PadRight(15) +
                    "|" + "Office Loca".PadRight(15) + " | "
                );
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                List<Device> sortedList = this.Sort();

                foreach (Device device in sortedList)
                {
                    TimeSpan ts = current - device.PurchaseDate;
                    if (ts.Days > 365 * 3 - 90)
                        Utilities.PrintLineColor((
                            "|" + device.GetType().ToString().Split(".")[1].PadRight(15) +
                            "|" + device.Brand.PadRight(15) +
                            "|" + device.Model.PadRight(15) +
                            "|" + ce.Amount(device.Location, device.Price).PadRight(15) +
                            "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                            "|" + device.Location.ToString().PadRight(15) + " |")
                            , ConsoleColor.Red);
                    else if (ts.Days > 365 * 3 - 180)
                        Utilities.PrintLineColor((
                            "|" + device.GetType().ToString().Split(".")[1].PadRight(15) +
                            "|" + device.Brand.PadRight(15) +
                            "|" + device.Model.PadRight(15) +
                            "|" + ce.Amount(device.Location, device.Price).PadRight(15) +
                            "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                            "|" + device.Location.ToString().PadRight(15) + " |")
                            , ConsoleColor.Yellow);
                    else
                        Utilities.PrintLineColor((
                            "|" + device.GetType().ToString().Split(".")[1].PadRight(15) +
                            "|" + device.Brand.PadRight(15) +
                            "|" + device.Model.PadRight(15) +
                            "|" + ce.Amount(device.Location, device.Price).PadRight(15) +
                            "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                            "|" + device.Location.ToString().PadRight(15) + " |")
                            , ConsoleColor.White);
                }

                Console.WriteLine("--------------------------------------------------------------------------------------------------");
            }
            else
            {
                Utilities.PrintLineColor("No Product Added To The List.", ConsoleColor.Yellow);
            }
            Console.WriteLine();
        }
    }
}
