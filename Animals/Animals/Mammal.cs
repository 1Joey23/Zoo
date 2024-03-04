using Animals;
using Foods;
using Reproducers;
using Utilites;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a mammal.
    /// </summary>
    public abstract class Mammal : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Mammal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Mammal(string name, int age, double weight, Gender gender)
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
                return 15.0;
            }
        }

        /// <summary>
        /// Moves by pacing.
        /// </summary>
        public override void Move()
        {
            /// If the mammal goes to the right, and the horzonal position and move distance are greater than -
            /// the max distance allowed, set the horizontal position to the max position and change the active -
            /// direction to go left. If the max isn't reached, the mammal goes to the right by increments of the -
            /// move distance value.
            if (this.XDirection == HorizontalDirection.Right)
            {
                if (this.XPosition + this.MoveDistance > this.XPositionMax)
                {
                    this.XPosition = this.XPositionMax;
                    this.XDirection = HorizontalDirection.Left;
                }
                else
                {
                    this.XPosition += this.MoveDistance;
                }
            }
            /// If the horizontal position subtracted by the move distance of the mammal is less than -
            /// 0, set the horizonal position to 0 and change the active direction to go right. If the -
            /// mammal hasn't reached the max, make the mammal move to the left by increments of the -
            /// move distance value.
            else
            {
                if (this.XPosition - this.MoveDistance < 0)
                {
                    this.XPosition = 0;
                    this.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    this.XPosition -= this.MoveDistance;
                }
            }
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public override IReproducer Reproduce()
        {
            // Create a baby reproducer.
            IReproducer baby = base.Reproduce();

            // If the animal is not a platypus and baby is an eater...
            if (this.GetType() != typeof(Platypus) && baby is IEater)
            {
                // Feed the baby.
                this.FeedNewborn(baby as IEater);
            }

            return baby;
        }

        /// <summary>
        /// Feeds a baby eater.
        /// </summary>
        /// <param name="newborn">The eater to feed.</param>
        private void FeedNewborn(IEater newborn)
        {
            // Determine milk weight.
            double milkWeight = this.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            newborn.Eat(milk);

            // Reduce parent's weight.
            this.Weight -= milkWeight;
        }
    }
}