using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using CagedItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The zoo's collection of cages.
        /// </summary>
        private Dictionary<Type, Cage> cages = new Dictionary<Type, Cage>();

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's information booth.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperatureChange;

        [NonSerialized]
        private Action<Guest> onAddGuest;

        [NonSerialized]
        private Action<Guest> onRemoveGuest;

        [NonSerialized]
        private Action<Animal> onAddAnimal;

        [NonSerialized]
        private Action<Animal> onRemoveAnimal;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="boothMoneyBalance">The money balance of the money collecting booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.b168.OnTemperatureChange = (previousTemp, currentTemp) =>
            {
                OnBirthingRoomTemperatureChange(previousTemp, currentTemp);
            };
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.informationBooth = new GivingBooth(attendant);
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());
            this.ticketBooth.AddMoney(boothMoneyBalance);

            foreach (AnimalType at in Enum.GetValues(typeof(AnimalType)))
            {
                this.cages.Add(Animal.ConvertAnimalTypeToType(at), new Cage(400, 800));
            }

            // Animals for sorting
            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));

            Shark chum = new Shark("Chum", 14, 377.3, Gender.Male);
            this.AddAnimal(chum);

            Squirrel chip = new Squirrel("Chip", 4, 1.0, Gender.Male);
            this.AddAnimal(chip);
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            // Guests for sorting
            Guest greg = new Guest("Greg", 35, 100.0m, WalletColor.Crimson, Gender.Male, new Account());
            Guest darla = new Guest("Darla", 7, 10.0m, WalletColor.Brown, Gender.Female, new Account());

            this.AddGuest(greg, new Ticket(0m, 0, 0));
            this.AddGuest(darla, new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Anna", 8, 12.56m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Matthew", 42, 10.0m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Doug", 7, 11.10m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Jared", 17, 31.70m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sean", 34, 20.50m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sally", 52, 134.20m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));

            // Adopt the animals.
            greg.AdoptedAnimal = chip;
            darla.AdoptedAnimal = chum;
            
        }

        /// <summary>
        /// Gets a list of the zoo's animals.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

         /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets the zoo's animal snack machine.
        /// </summary>
        public VendingMachine AnimalSnackMachine
        {
            get
            {
                return this.animalSnackMachine;
            }
        }
          
        /// <summary>
        /// Gets a list of the zoo's guests.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                double totalWeight = 0;

                // Loop through the zoo's list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add the current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperatureChange;
            }
            set
            {
                this.onBirthingRoomTemperatureChange = value;
            }
        }

        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }
            set
            {
                this.onAddGuest = value;
            }
        }
        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }
            set
            {
                this.onRemoveGuest = value;
            }
        }

        public Action<Animal> OnAddAnimal
        {
            get
            {
                return this.onAddAnimal;
            }
            set
            {
                this.onAddAnimal = value;
            }
        }
        public Action<Animal> OnRemoveAnimal
        {
            get
            {
                return this.onRemoveAnimal;
            }
            set
            {
                this.onRemoveAnimal = value;
            }
        }

        /// <summary>
        /// Loads a zoo from a file.
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        /// <returns>The loaded zoo.</returns>
        public static Zoo LoadFromFile(string fileName)
        {
            // Define and initialize a result variable
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a Zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            // Return result
            return result;
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>A newly created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

            // Set the initial money balance of the animal snack machine.
            comoZoo.AnimalSnackMachine.AddMoney(42.75m);    

            return comoZoo;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);
            if (OnAddAnimal != null)
            {
                OnAddAnimal(animal);
            }

            animal.OnPregnant = (reproducer) =>
            {
                this.b168.PregnantAnimals.Enqueue(reproducer);
            };
            
            animal.IsActive = true;

            this.cages[animal.GetType()].Add(animal);

            if (animal.IsPregnant)
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            }
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket == null || ticket.IsRedeemed == true)
            {
                throw new NullReferenceException("Guest " + guest.Name + " was not added because they did not have a ticket.");
            }
            else
            {
                ticket.Redeem();
                this.guests.Add(guest);
                if (OnAddAnimal != null)
                {
                    OnAddGuest(guest);
                }

                guest.GetVendingMachine += this.ProvideVendingMachine;
            }
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        public void BirthAnimal()
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal();

            // If the baby is an animal...
            if (baby is Animal)
            {
                // Add the baby to the zoo's list of animals.
                this.AddAnimal(baby as Animal);
            }
        }

        /// <summary>
        /// Finds the first cage that holds the specified type of animal.
        /// </summary>
        /// <param name="animalType">The type of animal whose cage to find.</param>
        /// <returns>The found cage.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage result = null;

            this.cages.TryGetValue(animalType, out result);

            return result;
        }

        /// <summary>
        /// Gets all animals of a specified type.
        /// </summary>
        /// <param name="type">The type of animals to get.</param>
        /// <returns>The collection of animals.</returns>
        public IEnumerable<Animal> GetAnimals(Type type)
        {
            List<Animal> result = new List<Animal>();

            foreach (Animal a in this.animals)
            {
                if (a.GetType() == type)
                {
                    result.Add(a);
                }
            }

            return result;
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="animal">The animal to remove.</param>
        public void RemoveAnimal(Animal animal)
        {
            this.animals.Remove(animal);
            OnRemoveAnimal(animal);
            animal.IsActive = false;

            this.cages[animal.GetType()].Remove(animal);

            foreach (Guest g in this.Guests)
            {
                if (g.AdoptedAnimal == animal)
                {
                    g.AdoptedAnimal = null;
                }
            }
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="guest">The guest to remove.</param>
        public void RemoveGuest(Guest guest)
        {
            this.guests.Remove(guest);
            OnRemoveGuest(guest);
            guest.IsActive = false;

            if (guest.AdoptedAnimal != null)
            {
                Cage cage = this.FindCage(guest.AdoptedAnimal.GetType());

                cage.Remove(guest);
            }
        }

        /// <summary>
        /// Saves the zoo to a file.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of Zoo
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Sells a ticket to a guest.
        /// </summary>
        /// <param name="guest">The guest to whom to sell the ticket.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            // Buy a ticket and a water bottle.
            Ticket ticket = guest.VisitTicketBooth(this.ticketBooth);

            // Get a coupon book and a map.
            guest.VisitInformationBooth(this.informationBooth);

            return ticket;
        }

        /// <summary>
        /// Sort the Animals.
        /// </summary>
        /// <returns></returns>
        public SortResult SortAnimals(string sortType, string sortValue, string sortBy, IList list)
        {
            SortResult sort = SortObjects(sortType, sortValue, sortBy, Animals.ToList());
            return sort;
        }

        /// <summary>
        /// Sort the Guests.
        /// </summary>
        /// <returns></returns>
        public SortResult SortGuests(string sortType, string sortValue, string sortBy, IList list)
        {
            SortResult sort = SortObjects(sortType, sortValue, sortBy, Guests.ToList());
            return sort;
        }

        /// <summary>
        /// Sorts the zoo's list of animals.
        /// </summary>
        /// <param name="sortType">The type of sort to perform.</param>
        /// <param name="sortValue">The value on which to sort.</param>
        /// <returns>The result of sorting (number of swaps and number of comparisons).</returns>
        public SortResult SortObjects(string sortType, string sortValue, string sortBy, IList list)
        {
            SortResult result = null;

            Func<object, object, int> aVariable = null;

            if (sortValue == "weight")
            {
                aVariable = WeightSortCompare;
            }
            else if (sortValue == "animalname")
            {
                aVariable = AnimalNameSortCompare;
            }
            else if (sortValue == "guestname")
            {
                aVariable = GuestNameSortCompare;
            }
            else if (sortValue == "age")
            {
                aVariable = AgeSortCompare;
            }
            else if(sortValue == "guestmoney")
            {
                aVariable = MoneySortCompare;
            }

            switch (sortType)
            {
                case "bubble":
                        result = list.BubbleSort(aVariable);
                    break;

                case "selection":
                        result = list.SelectionSort(aVariable);
                    break;

                case "insertion":
                        result = list.InsertionSort(aVariable);
                    break;

                case "shell":
                        result = list.ShellSort(aVariable);
                    break;

                case "quick":
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    SortResult sortResult = new SortResult();

                    list.QuickSort(0, list.Count - 1, sortResult, aVariable);

                    sw.Stop();
                    sortResult.ElapsedMilliseconds = sw.Elapsed.TotalMilliseconds;

                    result = sortResult;
                    break;

                default:
                    break;
            }

            return result;
        }

        public void OnDeserialized()
        {
            OnBirthingRoomTemperatureChange(this.b168.Temperature, this.b168.Temperature);

            this.guests.ForEach(g => OnAddGuest(g));

            this.animals.ForEach(a =>
            {
                OnAddAnimal(a);
                a.OnPregnant = reproducer => this.b168.PregnantAnimals.Enqueue(reproducer);
            });
        }

        /// <summary>
        /// Adds a list of animals to the zoo.
        /// </summary>
        /// <param name="animals">The list of animals to add to zoo.</param>
        private void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            // loop through passed-in list of animals
            foreach (Animal a in animals)
            {
                // add the animal to the list (use AddAnimal)
                this.AddAnimal(a);

                // using recursion, add the current animal's children to the zoo
                this.AddAnimalsToZoo(a.Children);
            }
        }

        private VendingMachine ProvideVendingMachine()
        {
            return this.animalSnackMachine;
        }

        private static int AnimalNameSortCompare(object object1, object object2)
        {
            // Assuming objects have a property 'Name' of type string
            return string.Compare(((Animal)object1).Name, ((Animal)object2).Name);
        }

        private static int GuestNameSortCompare(object object1, object object2)
        {
            // Assuming objects have a property 'Name' of type string
            return string.Compare(((Guest)object1).Name, ((Guest)object2).Name);
        }

        private static int WeightSortCompare(object object1, object object2)
        {
            // Assuming objects have a property 'Weight' of type int or float
            int result = 0;

            Animal animal1 = object1 as Animal;
            Animal animal2 = object2 as Animal;

            if (animal1.Weight == animal2.Weight)
            {
                result = 0;
            }
            else if (animal1.Weight > animal2.Weight)
            {
                result = 1;
            }
            else if (animal1.Weight < animal2.Weight)
            {
                result = -1;
            }

            return result;
        }

        private static int MoneySortCompare(object object1, object object2)
        {
            // Assuming objects have a property 'Weight' of type int or float
            int result = 0;

            decimal compareSum1 = ((Guest)object1).Wallet.MoneyBalance + ((Guest)object1).CheckingAccount.MoneyBalance;

            decimal compareSum2 = ((Guest)object2).Wallet.MoneyBalance + ((Guest)object2).CheckingAccount.MoneyBalance;

            if (compareSum1 == compareSum2)
            {
                result = 0;
            }
            else if (compareSum1 > compareSum2)
            {
                result = 1;
            }
            else if (compareSum1 < compareSum2)
            {
                result = -1;
            }

            return result;
        }

        private static int AgeSortCompare(object object1, object object2)
        {
            // Assuming objects are of type 'Animal' and have a property 'Age' of type int
            int result = 0;

            Animal animal1 = object1 as Animal;
            Animal animal2 = object2 as Animal;

            if (animal1.Age == animal2.Age)
            {
                result = 0;
            }
            else if (animal1.Age > animal2.Age)
            {
                result = 1;
            }
            else if (animal1.Age < animal2.Age)
            {
                result = -1;
            }

            return result;
        }

    }
}