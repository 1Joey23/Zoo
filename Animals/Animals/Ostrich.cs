using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Osrich class.
    /// </summary>
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// The Osrich constructor.
        /// </summary>
        /// <param name="name"> Osrich name</param>
        /// <param name="age"> Osrich age</param>
        /// <param name="weight"> Osrich weight</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 30.0;
        }

        /// <summary>
        /// Reset the Ostrich adult size and baby size.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.4 : 0.8;
            }
        }

        /// <summary>
        /// Osrich move.
        /// </summary>
        public override void Move()
        {
            // Osrich moves on two legs
        }
    }
}
