using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// Class for booth items being sold.
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// weight of the items
        /// </summary>
        private double weight;

        /// <summary>
        /// item constructor
        /// </summary>
        /// <param name="price">the price of the item</param>
        /// <param name="weight">the weight of the item</param>
        public Item(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// the weight getter
        /// </summary>
        public double Weight
        {
            get 
            {
                return weight;
            }
        }
    }
}
