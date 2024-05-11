using System;
using Foods;

namespace Animals
{
    /// <summary>
    /// The behavior of the animal to consume food.
    /// </summary>
    [Serializable]
    public class ConsumeBehavior : IEatBehavior
    {
        /// <summary>
        /// Animal consumes the food.
        /// </summary>
        /// <param name="eater">The eater who will consume the food.</param>
        /// <param name="food">The food that will be ate.</param>
        public void Eat(IEater eater, Food food)
        {
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
        }
    }
}