using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    public class MoneyBox : MoneyCollector
    {
        private void Lock()
        {
            // Lock
        }

        public override decimal RemoveMoney(decimal amount)
        {
            Unlock();
            base.RemoveMoney(amount);
            Lock();
            return amount;
        }

        private void Unlock()
        {
            // Unlock
        }
    }
}
