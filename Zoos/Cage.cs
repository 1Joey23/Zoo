using System;
using System.Collections.Generic;
using Animals;
using CagedItems;

namespace Zoos
{
    /// <summary>
    /// The class that represents a cage.
    /// </summary>
    [Serializable]
    public class Cage
    {
        /// <summary>
        /// The cage's occupants.
        /// </summary>
        private List<ICageable> cagedItems = new List<ICageable>();

        private Action<ICageable> onImageUpdate;

        /// <summary>
        /// Initializes a new instance of the Cage class.
        /// </summary>
        /// <param name="height">The height of the cage.</param>
        /// <param name="width">The width of the cage.</param>
        public Cage(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Gets the type of animal the cage holds.
        /// </summary>
        public Type AnimalType { get; private set; }

        /// <summary>
        /// Gets the height of the cage.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the width of the cage.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the cage's occupants.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        public Action<ICageable> OnImageUpdate
        {
            get
            {
                return this.onImageUpdate;
            }
            set
            {
                this.onImageUpdate = value;
            }
        }

        /// <summary>
        /// Adds an occupant to the cage.
        /// </summary>
        /// <param name="cagedItem">The occupant to add.</param>
        public void Add(ICageable cagedItem)
        {
            this.cagedItems.Add(cagedItem);
            cagedItem.OnImageUpdate += HangleImageUpdate;
            if (OnImageUpdate != null)
            {
                OnImageUpdate(cagedItem);
            }
        }

        /// <summary>
        /// Removes an occupant from the cage.
        /// </summary>
        /// <param name="cagedItem">The occupant to remove.</param>
        public void Remove(ICageable cagedItem)
        {
            this.cagedItems.Remove(cagedItem);
            cagedItem.OnImageUpdate -= HangleImageUpdate;
            if (OnImageUpdate != null)
            {
                OnImageUpdate(cagedItem);
            }
        }

        /// <summary>
        /// Displays the cage as a string.
        /// </summary>
        /// <returns>A string representation of the cage.</returns>
        public override string ToString()
        {
            string result = $"{this.AnimalType.Name} cage ([{this.Width}]x[{this.Height}])";

            foreach (ICageable c in this.cagedItems)
            {
                result += $"{Environment.NewLine}{c.ToString()} {c.XPosition}x{c.YPosition}";
            }

            return result;
        }

        private void HangleImageUpdate(ICageable item)
        {
            if (OnImageUpdate != null)
            {
                OnImageUpdate(item);
            }  
        }
    }
}