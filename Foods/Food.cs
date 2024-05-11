using System;
using CagedItems;
using Utilities;

namespace Foods
{
    /// <summary>
    /// The class which is used to represent food.
    /// </summary>
    [Serializable]
    public class Food : ICageable
    {
        /// <summary>
        /// The weight of the food (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Food class.
        /// </summary>
        /// <param name="weight">The weight of the food (in pounds).</param>
        public Food(double weight)
        {
            this.weight = weight;
            this.XPosition = 150;
            this.YPosition = 100;
        }

        /// <summary>
        /// Gets the display size of the food.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.5;
            }
        }

        /// <summary>
        /// Gets or sets the resource key of the food item.
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// Gets the weight of the food (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }

        /// <summary>
        /// Gets the X-Direction of the food object in the cage.
        /// </summary>
        public HorizontalDirection XDirection { get; }

        /// <summary>
        /// Gets the X-Position of the food object in the cage.
        /// </summary>
        public int XPosition { get; }

        /// <summary>
        /// Gets the Y-Direction of the food object in the cage.
        /// </summary>
        public VerticalDirection YDirection { get; }

        /// <summary>
        /// Gets the Y-Position of the food object in the cage.
        /// </summary>
        public int YPosition { get; }

        public HungerState HungerState { get; }

        public Action<ICageable> OnImageUpdate { get; set; }

        public bool IsActive { get; set; }
    }
}