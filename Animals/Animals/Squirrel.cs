using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Squirrel class.
    /// </summary>
    public class Squirrel : Mammal
    {
        /// <summary>
        /// The squirrel constructor.
        /// </summary>
        /// <param name="name"> squirrel name</param>
        /// <param name="age"> squirrel age</param>
        /// <param name="weight"> squirrel weight</param>
        public Squirrel(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17.0;
        }

        /// <summary>
        /// Squirrel move.
        /// </summary>
        public override void Move()
        {
            base.Move();
        }
    }
}
