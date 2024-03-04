using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Fish class.
    /// </summary>
    public abstract class Fish : Animal
    {
        /// <summary>
        /// The fish constructor.
        /// </summary>
        /// <param name="name">The fish name.</param>
        /// <param name="age"> The fish age.</param>
        /// <param name="weight"> The fish weight.</param>
        public Fish(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        protected override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }

        /// <summary>
        /// The fish swim.
        /// </summary>
        public override void Move()
        {
            // Swim
        }
    }
}
