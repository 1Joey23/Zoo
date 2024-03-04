using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;

namespace Animals
{
    /// <summary>
    /// Champanzee class.
    /// </summary>
    public class Chimpanzee : Mammal, IMoneyCollector
    {
        /// <summary>
        /// The Champanzee constructor.
        /// </summary>
        /// <param name="name"> Champanzee name</param>
        /// <param name="age"> Champanzee age</param>
        /// <param name="weight"> Champanzee weight</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
        }

        /// <summary>
        /// Champanzee move.
        /// </summary>
        public override void Move()
        {
            base.Move();
        }

        public decimal MoneyBalance
        {
            get;
        }

        public void AddMoney(decimal amount)
        {
            // Buy bananas.
        }

        public decimal RemoveMoney(decimal amount)
        {
            return 0;
        }
    }
}
