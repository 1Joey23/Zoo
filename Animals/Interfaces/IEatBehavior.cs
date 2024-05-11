using Foods;

namespace Animals
{
    /// <summary>
    /// The interface that defines a contract for all eating behaviors.
    /// </summary>
    public interface IEatBehavior
    {
        /// <summary>
        /// Has an animal eat food.
        /// </summary>
        /// <param name="eater">The animal with which to interact.</param>
        /// <param name="food">The food for the animal to eat.</param>
        void Eat(IEater eater, Food food);
    }
}