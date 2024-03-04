using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;

namespace Accounts
{
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// MoneyBalance in the account.
        /// </summary>
        private decimal moneyBalance;

        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }
        }

        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }

        public decimal RemoveMoney(decimal amount)
        {
            this.moneyBalance -= amount;
            return amount;
        }
    }
}
