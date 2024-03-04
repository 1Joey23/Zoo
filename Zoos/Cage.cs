using Animals;
using CagedItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    public class Cage
    {
        /// <summary>
        /// Take the animals from the animal class.
        /// </summary>
        private List<ICageable> cagedItems;
        public Cage(int height, int width, Type animalType) 
        {
            this.cagedItems = new List<ICageable>();
            this.Height = height;
            this.Width = width;
            this.AnimalType = animalType;
        }

        public Type AnimalType
        {
            get; protected set;
        }

        public int Height
        {
            get; protected set;
        }

        public int Width
        {
            get; protected set;
        }

        public List<ICageable> CagedItems
        {
            get
            {
                return cagedItems;
            }
        }

        /// <summary>
        /// Add animal to the cage.
        /// </summary>
        /// <param name="animal"></param>
        public void AddAnimal(ICageable cagedItem) 
        {
            this.cagedItems.Add(cagedItem);
        }

        /// <summary>
        /// Remove animal from the cage.
        /// </summary>
        /// <param name="animal"></param>
        public void RemoveAnimal(ICageable cagedItem) 
        {
            this.cagedItems.Remove(cagedItem);
        }
    }
}
