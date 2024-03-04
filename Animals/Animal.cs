using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilites;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Randomizer for animal position.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The timer for the animals.
        /// </summary>
        private Timer moveTimer;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;
            this.MoveDistance = random.Next(5, 16);
            this.XPosition = random.Next(1, 801);
            this.YPosition = random.Next(1, 401);
            ///this.XDirection = random.NextDouble() < 0.5 ? HorizontalDirection.Left : HorizontalDirection.Right;
            ///this.YDirection = random.NextDouble() < 0.5 ? VerticalDirection.Up : VerticalDirection.Down;
            this.moveTimer = new Timer(1000);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();
        }

        /// <summary>
        /// The overload constructor which chains constructors.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <param name="gender"></param>
        public Animal(string name, double weight, Gender gender)
            : this(name, 0, weight, gender)
        {

        }

        /// <summary>
        /// Gets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new FormatException("Name must be alphatetical letters only without spaces. (i.e. name)");
                }
                else // Runs if valid name is entered.
                {
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    this.age = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The age must be between 0 and 100, inclusive.");
                }
            }
        }


        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value >= 0 && value <= 1000)
                {
                    this.weight = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The weight must be between 0 and 1000, inclusive.");
                }
            }
        }

        /// <summary>
        /// Get or sets the animal's gender.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        protected double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        protected abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Move distance property.
        /// </summary>
        public int MoveDistance { get; set; } = 10;

        /// <summary>
        /// The horizontal direction of the position.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// The X-coordinate of the position.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// The maximum X-coordinate allowed for the position.
        /// </summary>
        public int XPositionMax { get; set; } = 800;

        /// <summary>
        /// The vertical direction of the position.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// The Y-coordinate of the position.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// The maximum Y-coordinate allowed for the position.
        /// </summary>
        public int YPositionMax { get; set; } = 400;

        public virtual double DisplaySize
        {
            get
            {
                return this.age == 0 ? 0.5 : 1.0;
            }
        }

        public string ResourceKey
        {
            get
            {
                string animalType = this.GetType().Name;
                return $"{animalType}{(this.Age == 0 ? "Baby" : "Adult")}";
            }
        }

        public static Type ConvertAnimalTypeToType(AnimalType animalType)
        {
            Type type = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    type = typeof(Chimpanzee);
                    break;
                case AnimalType.Dingo:
                    type = typeof(Dingo);
                    break;
                case AnimalType.Eagle:
                    type = typeof(Eagle);
                    break;
                case AnimalType.Hummingbird:
                    type = typeof(Hummingbird);
                    break;
                case AnimalType.Kangaroo:
                    type = typeof(Kangaroo);
                    break;
                case AnimalType.Ostrich:
                    type = typeof(Ostrich);
                    break;
                case AnimalType.Platypus:
                    type = typeof(Platypus);
                    break;
                case AnimalType.Shark:
                    type = typeof(Shark);
                    break;
                case AnimalType.Squirrel:
                    type = typeof(Squirrel);
                    break;
            }
            return type;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            // Increase animal's weight as a result of eating food.
            this.Weight += food.Weight * (this.WeightGainPercentage / 100);
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.isPregnant = true;
        }

        /// <summary>
        /// Moves about.
        /// </summary>
        public abstract void Move();

        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
/// Honestly I got no fuckin clue if the following conditional directives are doing the right thing.
#if DEBUG
            this.moveTimer.Stop();
            Move();
            this.moveTimer.Start();
#else
            Move();
#endif
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public virtual IReproducer Reproduce()
        {
            // Create a baby reproducer.
            Animal baby = Activator.CreateInstance(this.GetType(), string.Empty, 0, this.Weight * (this.BabyWeightPercentage / 100)) as Animal;

            // Reduce mother's weight by 25 percent more than the value of the baby's weight.
            this.Weight -= baby.Weight * 1.25;

            // Make mother not pregnant after giving birth.
            this.isPregnant = false;

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            return this.name + ": " + this.GetType().Name + " (" + this.age + ", " + this.Weight + ")";
        }
    }
}