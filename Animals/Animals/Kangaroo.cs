using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Kangaroo class.
    /// </summary>
    public class Kangaroo : Mammal
    {
        /// <summary>
        /// The Kangaroo constructor.
        /// </summary>
        /// <param name="name"> Kangaroo name</param>
        /// <param name="age"> Kangaroo age</param>
        /// <param name="weight"> Kangaroo weight</param>
        public Kangaroo(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 13.0;
        }

        /// <summary>
        /// Kangaroo move.
        /// </summary>
        public override void Move()
        {
            base.Move();
        }
    }
}
