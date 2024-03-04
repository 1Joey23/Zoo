using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Shark class.
    /// </summary>
    public class Shark : Fish
    {
        /// <summary>
        /// The shark constructor
        /// </summary>
        /// <param name="name"> shark name</param>
        /// <param name="age">shark age</param>
        /// <param name="weight"> shark weight</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 18.0;
        }

        /// <summary>
        /// Reset the shark adult size and baby size.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 1.0 : 1.5;
            }
        }

        public override void Move()
        {
            // Swim
        }
    }
}
