using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject_HongSun
{
    internal class Device
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Location Location { get; set; }


        private double _price;
        
        public double Price { 
            get { return this._price; }
            set {
                if (value > 0)
                {
                    // round the price to 2 decimal places
                    this._price = Math.Round(value, 2);
                }
                else
                {   
                    // if price <= 0 throw Exception
                    throw new ArgumentOutOfRangeException(nameof(Price), "Price of eletronic devices must be greater than 0.");
                }
            }
        }

        public Device(string brand, string model, string purchaseDate, double price, Location location)
        {
            this.Brand = brand;
            this.Model = model;
            try
            {
                this.PurchaseDate = DateTime.ParseExact(purchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (DateTime.Compare(this.PurchaseDate, DateTime.Now) > 0 )
                {
                    throw new ArgumentOutOfRangeException(nameof(PurchaseDate), "Purchase date cannot be later than today");
                }
            } catch (FormatException e)
            {
                throw e;
            }
            this.Price = price;
            this.Location = location;
        }
    }
}
