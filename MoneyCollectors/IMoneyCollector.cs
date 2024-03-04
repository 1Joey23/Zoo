using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    public interface IMoneyCollector
    {
        decimal MoneyBalance
        {
            get;
        }

        void AddMoney(decimal amount);

        decimal RemoveMoney(decimal amount);
    }
}
