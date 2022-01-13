using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject_HongSun
{
    internal class Laptop : Device
    {
        private int _screenSize;
        public Laptop(string brand, string model, string purchaseDate, double price, int screenSize, Location location)
            : base(brand, model, purchaseDate, price, location)
        {
            this.ScreenSize = screenSize;
        }

        public int ScreenSize {
            get{return this._screenSize;}
            set{
                if (value >= 8 && value <= 19)
                    this._screenSize = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(ScreenSize), "Ivalid screen size.");
            }
        }
            
    }
}
