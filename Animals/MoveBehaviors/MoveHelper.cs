using Utilities;

namespace Animals
{
    /// <summary>
    /// The class used to help caged objects move.
    /// </summary>
    public static class MoveHelper
    {
        /// <summary>
        /// Moves an animal horizontally.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        /// <param name="moveDistance">The distance to move.</param>
        public static void MoveHorizontally(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case CagedItems.HungerState.Hungry:
                    moveDistance /= 4;
                    break;

                case CagedItems.HungerState.Starving:
                    moveDistance = 0;
                    break;

                case CagedItems.HungerState.Unconscious:
                    moveDistance = 0;
                    break;
            }

            // If the animal is moving to the right
            if (animal.XDirection == HorizontalDirection.Right)
            {
                // If the animal's next movement would take it off the right edge of the window
                if (animal.XPosition + moveDistance > animal.XPositionMax)
                {
                    // Set the animal's position to the right edge of the window
                    animal.XPosition = animal.XPositionMax;

                    // Make the animal move to the left
                    animal.XDirection = HorizontalDirection.Left;
                }
                else
                {
                    // Move the animal the distance of one step to the right
                    animal.XPosition += moveDistance;
                }
            }
            else
            {
                // If the animal's next movement would move it off the left edge of the window
                if (animal.XPosition - moveDistance < 0)
                {
                    // Set the animal's position to the left edge of the window
                    animal.XPosition = 0;

                    // Make the animal move to the right
                    animal.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    // Move the animal the distance of one step to the left
                    animal.XPosition -= moveDistance;
                }
            }
        }

        /// <summary>
        /// Moves an animal vertically.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        /// <param name="moveDistance">The distance to move.</param>
        public static void MoveVertically(Animal animal, int moveDistance)
        {
            // If the animal is currently moving down.
            if (animal.YDirection == VerticalDirection.Down)
            {
                // If the next movement would take the animal beyond the max position on the bottom side.
                if (animal.YPosition + moveDistance > animal.YPositionMax)
                {
                    // Sets the position of the mammal to the maximum and switches direction.
                    animal.YPosition = animal.YPositionMax;
                    animal.YDirection = VerticalDirection.Up;
                }
                else
                {
                    // Moves down on the Y axis as determined by Move Distance.
                    animal.YPosition += moveDistance;
                }
            }
            else
            {
                // If the next movement would take the animal beyond the max position on the top side.
                if (animal.YPosition - moveDistance < 0)
                {
                    // Sets the position of the mammal to 0 and switches direction.
                    animal.YPosition = 0;
                    animal.YDirection = VerticalDirection.Down;
                }
                else
                {
                    // Moves up on the Y axis as determined by Move Distance.
                    animal.YPosition -= moveDistance;
                }
            }
        }
    }
}