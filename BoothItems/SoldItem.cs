using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The Items that cost money.
    /// </summary>
    public class SoldItem : Item
    {
        /// <summary>
        /// Price of the sold item.
        /// </summary>
        private decimal price;

        /// <summary>
        /// Sold item constructor.
        /// </summary>
        /// <param name="price">price of the sold item</param>
        /// <param name="weight">weight of the sold item</param>
        public SoldItem(decimal price, double weight)
            : base(weight)
        { 
            this.price = price;
        }

        /// <summary>
        /// getter for the price.
        /// </summary>
        public decimal Price
        {
            get { return price; }
        }
    }
}
