﻿using System;
using System.Collections.Generic;
using People;
using Reproducers;

namespace BirthingRooms
{
    /// <summary>
    /// The class which is used to represent a birthing room.
    /// </summary>
    [Serializable]
    public class BirthingRoom
    {
        /// <summary>
        /// The minimum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MinTemperature = 35.0;

        /// <summary>
        /// The maximum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MaxTemperature = 95.0;

        /// <summary>
        /// The initial temperature of the birthing room.
        /// </summary>
        private readonly double initialTemperature = 77.0;

        /// <summary>
        /// The current temperature of the birthing room.
        /// </summary>
        private double temperature;

        /// <summary>
        /// The employee currently assigned to be the vet of the birthing room.
        /// </summary>
        private Employee vet;

        /// <summary>
        /// Initializes a new instance of the BirthingRoom class.
        /// </summary>
        /// <param name="vet">The employee to be the birthing room's vet.</param>
        public BirthingRoom(Employee vet)
        {
            this.temperature = this.initialTemperature;
            this.vet = vet;
            this.PregnantAnimals = new Queue<IReproducer>();
        }

        /// <summary>
        /// Gets or sets the birthing room's temperature.
        /// </summary>
        public double Temperature
        {
            get
            {
                return this.temperature;
            }

            set
            {
                // If the value is above the maximum temperature.
                if (value > BirthingRoom.MaxTemperature)
                {
                    throw new ArgumentOutOfRangeException("temperature", string.Format("The temperature must be below {0}° degrees", BirthingRoom.MaxTemperature));                 
                }
                else if (value < BirthingRoom.MinTemperature)
                {
                    throw new ArgumentOutOfRangeException("temperature", string.Format("The temperature must be above {0}° degrees.", BirthingRoom.MinTemperature));
                }
                else
                {
                    // Set temperature.
                    double priorTemp = this.temperature;
                    this.temperature = value;
                    if (this.OnTemperatureChange != null)
                    {
                        OnTemperatureChange(priorTemp, value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the queue of pregnant animals.
        /// </summary>
        public Queue<IReproducer> PregnantAnimals { get; private set; }

        public Action<double, double> OnTemperatureChange
        {
            get;
            set;
        }

        /// <summary>
        /// Births a reproducer.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer BirthAnimal()
        {
            IReproducer baby = null;

            // Have the vet deliver the reproducer.
            if (this.PregnantAnimals.Count != 0)
            {
                baby = this.vet.DeliverAnimal(this.PregnantAnimals.Dequeue());

                // Increase the temperature due to the heat generated from birthing.
                this.Temperature += 0.5;
            }

            return baby;
        }
    }
}