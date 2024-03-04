using System;
using System.Collections.Generic;
using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth.
    /// </summary>
    public abstract class Booth : MoneyCollector
    {
        /// <summary>
        /// The employee currently assigned to be the attendant of the booth.
        /// </summary>
        private Employee attendant;

        /// <summary>
        /// The list of items in the booth.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// Initializes a new instance of the Booth class.
        /// </summary>
        /// <param name="attendant">The employee to be the booth's attendant.</param>
        /// <param name="ticketPrice">The price of a ticket.</param>
        public Booth(Employee attendant)
        {
            this.items = new List<Item>();
            this.attendant = attendant;
        }

        protected Employee Attendant
        {
            get
            {
                return this.attendant;
            }
        }

        protected List<Item> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}