using System;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a sold item.
    /// </summary>
    [Serializable]
    public abstract class SoldItem : Item
    {
        /// <summary>
        /// The price of the item.
        /// </summary>
        private decimal price;

        /// <summary>
        /// Initializes a new instance of the SoldItem class.
        /// </summary>
        /// <param name="price">The price of the item.</param>
        /// <param name="weight">The weight of the item.</param>
        public SoldItem(decimal price, double weight)
            : base(weight)
        {
            this.price = price;
        }
    }
}