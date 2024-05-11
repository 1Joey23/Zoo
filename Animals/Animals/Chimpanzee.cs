using System;
using MoneyCollectors;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a chimpanzee.
    /// </summary>
    [Serializable]
    public class Chimpanzee : Mammal//, IMoneyCollector
    {
        /// <summary>
        /// Initializes a new instance of the Chimpanzee class.
        /// </summary>
        /// <param name="name">The name of the chimpanzee.</param>
        /// <param name="age">The age of the chimpanzee.</param>
        /// <param name="weight">The weight of the chimpanzee (in pounds).</param>
        /// <param name="gender">The gender of the chimpanzee.</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
        }

        /// <summary>
        /// Gets the chimpanzee money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return 0m;
            }
        }

        /// <summary>
        /// Adds money to the chimpanzee's money balance.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void AddMoney(decimal amount)
        {
            // Buy some bananas.
        }

        /// <summary>
        /// Removes the specified amount of money from the chimpanzee's money balance.
        /// </summary>
        /// <param name="amount">The amount to remove.</param>
        /// <returns>The amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return 0m;
        }
    }
}