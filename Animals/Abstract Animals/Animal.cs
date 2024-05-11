using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// A random object used to randomize the movement of each animal.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// A list of the animal's children.
        /// </summary>
        private List<Animal> children;

        /// <summary>
        /// The gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// A timer that moves the animal when it goes off.
        /// </summary>
        [NonSerialized]
        private Timer moveTimer;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        [NonSerialized]
        private Action<Animal> onTextChangeAnimal;

        /// <summary>
        /// Time until they starve and die.
        /// </summary>
        [NonSerialized]
        private Timer hungerTimer;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal.</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, double weight, Gender gender)
            : this(name, 0, weight, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.gender = gender;
            this.name = name;
            this.weight = weight;

            this.YPositionMax = 400;
            this.XPositionMax = 800;

            this.MoveDistance = Animal.random.Next(5, 16);

            this.XPosition = Animal.random.Next(0, this.XPositionMax + 1);
            this.YPosition = Animal.random.Next(0, this.YPositionMax + 1);
            this.XDirection = Animal.random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = Animal.random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;

            this.children = new List<Animal>();
            this.CreateTimers();
        }

        /// <summary>
        /// Gets or sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 100.");
                }

                this.age = value;
                if (OnTextChangeAnimal != null)
                {
                    OnTextChangeAnimal(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            protected set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Gets a list of the animal's children.
        /// </summary>
        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets the gender of the animal.
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
        /// Gets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }
            /// Was originally added to show the change in pregnancy status.
            /*set
            {
                this.isPregnant = value;
                if (OnTextChangeAnimal != null)
                {
                    OnTextChangeAnimal(this);
                }
            }*/
        }

        public Action<Animal> OnTextChangeAnimal
        {
            get
            {
                return this.onTextChangeAnimal;
            }
            set
            {
                this.onTextChangeAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets the animal's move behavior.
        /// </summary>
        public IMoveBehavior MoveBehavior { get; set; }

        /// <summary>
        /// Gets or sets the animal's eat behavior.
        /// </summary>
        public IEatBehavior EatBehavior { get; set; }

        /// <summary>
        /// Gets or sets the animal's reproduce behavior.
        /// </summary>
        public IReproduceBehavior ReproduceBehavior { get; set; }

        /// <summary>
        /// Gets or sets the food that the animal will eat.
        /// </summary>
        public Food Food { get; set; }

        /// <summary>
        /// Gets or sets the distance of one step.
        /// </summary>
        public int MoveDistance { get; set; }

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
                    throw new ArgumentException("Names can contain only upper- and lowercase letters A-Z and spaces.");
                }

                this.name = value;
                if (OnTextChangeAnimal != null)
                {
                    OnTextChangeAnimal(this);
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
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException("weight", "Weight must be between 0 and 1000 lbs.");
                }

                this.weight = value;
                if (OnTextChangeAnimal != null)
                {
                    OnTextChangeAnimal(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the horizontal direction.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the horizontal position.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the position at which an animal will change direction.
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the vertical direction.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets the vertical position.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the position at which an animal will change direction.
        /// </summary>
        public int YPositionMax { get; set; }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Gets the proportion at which to display the animal.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.5 : 1;
            }
        }

        /// <summary>
        /// Gets the resource key of the animal.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return this.GetType().Name + (this.Age == 0 ? "Baby" : "Adult");
            }
        }

        public HungerState HungerState
        {
            get;
            set;
        }

        public Action OnHunger
        {
            get;
            set;
        }

        public Action<ICageable> OnImageUpdate
        {
            get;
            set;
        }

        public Action<IReproducer> OnPregnant
        {
            get;
            set;
        }

        public bool IsActive
        {
            get
            {
                return this.moveTimer.Enabled;
            }
            set
            {
                this.moveTimer.Enabled = value;
            }
        }

        /// <summary>
        /// Converts an animal type enumeration value to a .NET type.
        /// </summary>
        /// <param name="animalType">The animal type to convert.</param>
        /// <returns>The associated .NET type.</returns>
        public static Type ConvertAnimalTypeToType(AnimalType animalType)
        {
            Type result = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    result = typeof(Chimpanzee);
                    break;
                case AnimalType.Dingo:
                    result = typeof(Dingo);
                    break;
                case AnimalType.Eagle:
                    result = typeof(Eagle);
                    break;
                case AnimalType.Hummingbird:
                    result = typeof(Hummingbird);
                    break;
                case AnimalType.Kangaroo:
                    result = typeof(Kangaroo);
                    break;
                case AnimalType.Ostrich:
                    result = typeof(Ostrich);
                    break;
                case AnimalType.Platypus:
                    result = typeof(Platypus);
                    break;
                case AnimalType.Shark:
                    result = typeof(Shark);
                    break;
                case AnimalType.Squirrel:
                    result = typeof(Squirrel);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Adds a child to the animal's family tree.
        /// </summary>
        /// <param name="animal">The child to be added to the family.</param>
        public void AddChild(Animal animal)
        {
            if (animal != null)
            {
                this.children.Add(animal);
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            this.Food = food;
            this.EatBehavior.Eat(this, food);

            // Reset the counter and set the hungerstate to satisfied.
            this.HungerState = HungerState.Satisfied;
            this.i = 0;

            // Reset the timer.
            this.hungerTimer.Stop();
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            /// Was originally added to show the change in pregnancy status. IsPregnant activates the setter when caps.
            //this.IsPregnant = true;
            this.isPregnant = true;
            if (OnPregnant != null)
            {
                OnPregnant(this);
            }
            this.MoveBehavior = new NoMoveBehavior();
        }

        /// <summary>
        /// Moves the animal.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);
            if (OnImageUpdate != null)
            {
                OnImageUpdate(this);
            }
        }

        /// <summary>
        /// Gives birth to a baby animal and feeds it.
        /// </summary>
        /// <returns>The baby animal.</returns>
        public IReproducer Reproduce()
        {
            // Make mother animal to be no longer pregnant.
            this.isPregnant = false;

            IReproducer baby = this.ReproduceBehavior.Reproduce(this);

            if (baby is Animal)
            {
                this.AddChild(baby as Animal);
            }

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            return this.name + ": " + this.GetType().Name + " (" + this.age + ", " + this.Weight + ")" + (this.IsPregnant ? " P" : string.Empty);
        }

        /// <summary>
        /// Initializes the animal's timers.
        /// </summary>
        private void CreateTimers()
        {
            /// Timer is 100 instead of 1000 so they move faster.
            this.moveTimer = new Timer(100);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();

            // The hunger timer. 20,000 = 20 sec.
            this.hungerTimer = new Timer(20000);
            this.hungerTimer.Elapsed += this.HandleHungerStateChange;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Handles the event of the move timer going off.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
#if DEBUG
            //this.moveTimer.Enabled = false;
#endif
            this.Move();
#if DEBUG
            //this.moveTimer.Enabled = true;
#endif
        }

        /// <summary>
        /// Creates timers when the animal is deserialized.
        /// </summary>
        /// <param name="context">The streaming context.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        /// Declare counter variable for HungerStateChange Method
        int i = 0;

        /// <summary>
        /// Handles the change in hunger state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleHungerStateChange(object sender, ElapsedEventArgs e)
        {
            switch (i)
            {
                case 0:
                    this.HungerState = HungerState.Satisfied;
                    break;

                case 1:
                    this.HungerState = HungerState.Hungry;
                    break;

                case 2:
                    this.HungerState = HungerState.Starving;
                    break;

                case 3:
                    this.HungerState = HungerState.Unconscious;
                    if (this.OnHunger != null)
                    {
                        this.OnHunger();
                    }
                    break;
            }
            i++;
        }
    }
}