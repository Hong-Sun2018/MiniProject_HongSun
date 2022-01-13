using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject_HongSun
{
    internal class MobilePhone : Device
    {
        private string _telephoneNumber;
        public MobilePhone(string brand, string model, string purchaseDate, double price, string telephoneNumber, Location location)
            : base(brand, model, purchaseDate, price, location)
        {
            this.TelephoneNumber = telephoneNumber;
        }

        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set {
                foreach (char chr in value.ToCharArray())
                {
                    if (!char.IsDigit(chr))
                        throw new FormatException("Telephone number can only contains number.");
                    this._telephoneNumber = value;
                }
            }
        }
    }
}
