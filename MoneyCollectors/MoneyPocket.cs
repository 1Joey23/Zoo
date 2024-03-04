using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    public class MoneyPocket : MoneyCollector
    {
        private void Unfold()
        {
            // Unfold
        }

        public override decimal RemoveMoney(decimal amount)
        {
            Unfold();
            base.RemoveMoney(amount);
            Fold();
            return amount;
            
        }

        private void Fold()
        {
            // Fold
        }
    }
}
