using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The money in the moneycollector.
        /// </summary>
        private decimal moneyBalance;

        public decimal MoneyBalance
        {
            get { return moneyBalance; }
        }

        /// <summary>
        /// Adds a specified amount of money to the object.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the object.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the object...
            if (this.moneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.moneyBalance;
            }

            // Subtract the amount removed from the object's money balance.
            this.moneyBalance -= amountRemoved;
            return amountRemoved;
        }

    }
}
