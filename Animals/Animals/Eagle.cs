using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Eagle class.
    /// </summary>
    public class Eagle : Bird
    {
        /// <summary>
        /// The Eagle constructor.
        /// </summary>
        /// <param name="name"> Eagle name</param>
        /// <param name="age"> Eagle age</param>
        /// <param name="weight"> Eagle weight</param>
        public Eagle(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 25.0;
        }

        /// <summary>
        /// Eagle move.
        /// </summary>
        public override void Move()
        {
            // Eagle moves.
        }
    }
}
