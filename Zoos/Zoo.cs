using System;
using System.Collections.Generic;
using People;
using Animals;
using VendingMachines;
using Reproducers;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using Accounts;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private readonly List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

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
        /// The zoo's ticket booth for selling items.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// The zoo's booth for handing out informative items.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The list of cages for each animal in the zoo.
        /// </summary>
        private List<Cage> cages;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="boothMoneyBalance">The initial money balance of the zoo's ticket booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());
            this.informationBooth = new GivingBooth(attendant);
            this.ticketBooth.AddMoney(boothMoneyBalance);
            this.cages = new List<Cage>();

            /// Iterate through each animal type in the AnimalType Enum for instantiation of the dedicated cage for the type.
            foreach (AnimalType animalType in Enum.GetValues(typeof(AnimalType)))
            {
                cages.Add(new Cage(400, 800, Animal.ConvertAnimalTypeToType(animalType)));
            }
        }

        /// <summary>
        /// The property getter for populating the popup window for animals.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// The property getter for populating the popup window for guests.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
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
        /// Gets the temperature of the zoo's birthing room.
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
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                // Define accumulator variable.
                double totalWeight = 0;

                // Loop through the list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);
            Cage cage = this.FindCage(animal.GetType());
            cage.AddAnimal(animal);
        }

        /// <summary>
        /// Adds a guest to the zoo if the have a valid ticket.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.Redeem();
                this.guests.Add(guest);
            }
            else
            {
                throw new NullReferenceException("The ticket from the guest is absent, preventing admission.");
            }
            
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        public void BirthAnimal(IReproducer reproducer)
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal(reproducer);

            // If the baby is an animal...
            if (baby is Animal)
            {
                // Add the baby to the zoo's list of animals.
                this.AddAnimal(baby as Animal);
            }
        }

        /// <summary>
        /// Create the new zoo.
        /// </summary>
        /// <returns>The created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

            // Add money to the animal snack machine.
            comoZoo.AnimalSnackMachine.AddMoney(42.75m);

            return comoZoo;
        }

        /// <summary>
        /// Find the animal to display in the console.
        /// </summary>
        /// <param name="name">The animals name.</param>
        /// <returns>hopefully an animal.</returns>
        public Animal FindAnimal(string name)
        {
            Animal animal = null;

            foreach (Animal a in this.animals)
            {
                if (a.Name == name)
                {
                    animal = a;

                    break;
                }
            }
            return animal;
        }

        /// <summary>
        /// Finds an animal based on type.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(Type type)
        {
            // Define variable to hold matching animal.
            Animal animal = null;

            // Loop through the list of animals.
            foreach (Animal a in this.animals)
            {
                // If the current animal matches...
                if (a.GetType() == type)
                {
                    // Set the current animal to the variable.
                    animal = a;

                    // Break out of the loop.
                    break;
                }
            }

            // Return the matching animal.
            return animal;
        }

        /// <summary>
        /// Finds an animal based on type and pregnancy status.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <param name="isPregnant">The pregnancy status of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(Type type, bool isPregnant)
        {
            // Define variable to hold matching animal.
            Animal animal = null;

            // Loop through the list of animals.
            foreach (Animal a in this.animals)
            {
                // If the current animal matches...
                if (a.GetType() == type && a.IsPregnant == isPregnant)
                {
                    // Store the current animal in the variable.
                    animal = a;

                    // Break out of the loop.
                    break;
                }
            }

            // Return the matching animal.
            return animal;
        }

        public Cage FindCage(Type animalType)
        {
            Cage cage = null;

            // Loop through the list of cages.
            foreach (Cage c in this.cages)
            {
                // If the current animal type matches to the cage
                if (c.AnimalType == animalType)
                {
                    // Store the current cage in the variable.
                    cage = c;

                    // Break out of the loop.
                    break;
                }
            }
            return cage;
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="name">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public Guest FindGuest(string name)
        {
            // Define a variable to hold matching guest.
            Guest guest = null;

            // Loop through the list of guests.
            foreach (Guest g in this.guests)
            {
                // If the current guest matches...
                if (g.Name == name)
                {
                    // Store the current guest in the variable
                    guest = g;

                    // Break out of the loop
                    break;
                }
            }

            // Return the matching guest.
            return guest;
        }

        /// <summary>
        /// Add every instance of the same type animal to a list and return the list.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
        /// Sell the ticket to the guest and visit information booth.
        /// </summary>
        /// <param name="guest">The guest.</param>
        /// <returns>The guest's ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            Ticket ticket = guest.VisitTicketBooth(this.ticketBooth);
            guest.VisitInformationBooth(this.informationBooth);
            return ticket;
        }

        /// <summary>
        /// Removes the selected animal from the zoo.
        /// </summary>
        /// <param name="animal">The animal to be removed.</param>
        public void RemoveAnimal(Animal animal)
        {
            this.animals.Remove(animal);
            Cage cage = this.FindCage(animal.GetType());
            cage.RemoveAnimal(animal);

            // Remove the guest's animal when the animal is removed and taken to the butcherhouse to make some McDonalds burgers.
            foreach (Guest guest in this.guests)
            {
                if (guest.AdoptedAnimal == animal)
                {
                    guest.AdoptedAnimal = null;
                    cage.RemoveAnimal(guest);
                }
            }
        }

        /// <summary>
        /// Removes the selected guest from the zoo.
        /// </summary>
        /// <param name="guest">The guest to be removed.</param>
        public void RemoveGuest(Guest guest)
        {
            this.guests.Remove(guest);
        }
    }
}