﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Accounts;
using Animals;
using BoothItems;
using People;
using Reproducers;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class that provides console functionality.
    /// </summary>
    internal static class ConsoleHelper
    {
        /// <summary>
        /// Loads a zoo from a file.
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        /// <returns>The loaded zoo.</returns>
        public static Zoo LoadFile(string fileName)
        {
            Zoo result = null;

            try
            {
                // Load the selected zoo file as current zoo.
                result = Zoo.LoadFromFile(fileName);

                // Send a message that the zoo has changed.
                Console.WriteLine("Your zoo has been loaded successfully.");
            }
            catch
            {
                throw new Exception("Unable to load the specified file path.");
            }

            return result;
        }

        /// <summary>
        /// Sets the birthing room temperature of a zoo.
        /// </summary>
        /// <param name="zoo">The zoo to update.</param>
        /// <param name="temperature">The value to which to set the temperature.</param>
        public static void SetTemperature(Zoo zoo, string temperature)
        {
            try
            {
                double newTemp = double.Parse(temperature);
                double previousTemp = zoo.BirthingRoomTemperature;
                zoo.BirthingRoomTemperature = newTemp;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Temperature must be a number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Shows a help index of console commands.
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine("OOP 2 Zoo Help Index:");

            ConsoleUtil.WriteHelpDetail("help", "Show help detail.", "[command]", "The (optional) command for which to show help details.");

            Console.WriteLine("Known commands:");
            Console.WriteLine("RESTART: Creates a new zoo.");
            Console.WriteLine("EXIT: Exits the application.");
            Console.WriteLine("TEMP: Sets the temperature of the zoo's birthing room.");
            Console.WriteLine("SHOW: Shows the properties of an animal, guest, or cage.");
            Console.WriteLine("ADD: Adds an animal or guest to the zoo.");
            Console.WriteLine("REMOVE: Removes a guest or animal from the zoo.");
            Console.WriteLine("SAVE: Saves a zoo to a file.");
            Console.WriteLine("LOAD: Loads a zoo from a file.");
        }

        /// <summary>
        /// Shows detailed help information for an individual console command.
        /// </summary>
        /// <param name="command">The command to show detail for.</param>
        public static void ShowHelpDetail(string command)
        {
            Dictionary<string, string> arguments;

            switch (command)
            {
                case "restart":
                    ConsoleUtil.WriteHelpDetail(command, "Creates a new zoo and corresponding objects.");

                    break;
                case "exit":
                    ConsoleUtil.WriteHelpDetail(command, "Exits the application.");

                    break;
                case "temp":
                    ConsoleUtil.WriteHelpDetail(command, "Sets the temperature of the zoo's birthing room.", "temperature", "The temperature to set the birthing room to.");

                    break;
                case "show":
                    arguments = new Dictionary<string, string>
                    {
                        { "objectType", "The type of object to show (ANIMAL, GUEST, or CAGE)." },
                        { "objectName", "The name of the object to show (use an animal name for CAGE)." }
                    };

                    ConsoleUtil.WriteHelpDetail(command, "Shows details of an object.", arguments);

                    break;
                case "add":
                    ConsoleUtil.WriteHelpDetail(command, "Adds an object to the zoo.", "objectType", "The type of object to add (ANIMAL or GUEST).");

                    break;
                case "remove":
                    arguments = new Dictionary<string, string>
                    {
                        { "objectType", "The type of object to remove (ANIMAL or GUEST)." },
                        { "objectName", "The name of the object to remove." }
                    };

                    ConsoleUtil.WriteHelpDetail(command, "Removes an object from the zoo.", arguments);

                    break;
                case "save":
                    ConsoleUtil.WriteHelpDetail(command, "Saves a zoo to a file.", "fileName", "The name of the file to save.");

                    break;
                case "load":
                    ConsoleUtil.WriteHelpDetail(command, "Loads a zoo from a file.", "fileName", "The name of the file to load.");

                    break;
            }
        }

        /// <summary>
        /// Processes the add console command.
        /// </summary>
        /// <param name="zoo">The zoo to contain the added object.</param>
        /// <param name="type">The type of object to add.</param>
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    ConsoleHelper.AddAnimal(zoo);

                    break;
                case "guest":
                    ConsoleHelper.AddGuest(zoo);

                    break;
                default:
                    Console.WriteLine("Unknown type. Only animals and guests can be added.");

                    break;
            }
        }

        /// <summary>
        /// Processes the show console command.
        /// </summary>
        /// <param name="zoo">The zoo containing the object to show.</param>
        /// <param name="type">The type of object to show.</param>
        /// <param name="name">The name of the object to show.</param>
        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            string uppercaseName = ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ConsoleHelper.RemoveAnimal(zoo, uppercaseName);

                    break;
                case "guest":
                    ConsoleHelper.RemoveGuest(zoo, uppercaseName);

                    break;
                default:
                    Console.WriteLine("Unknown type. Only animals and guests can be removed.");

                    break;
            }
        }

        /// <summary>
        /// Processes the show console command.
        /// </summary>
        /// <param name="zoo">The zoo containing the object to show.</param>
        /// <param name="type">The type of object to show.</param>
        /// <param name="name">The name of the object to show.</param>
        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            string uppercaseName = ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ConsoleHelper.ShowAnimal(zoo, uppercaseName);

                    break;
                case "guest":
                    ConsoleHelper.ShowGuest(zoo, uppercaseName);

                    break;
                case "cage":
                    ConsoleHelper.ShowCage(zoo, uppercaseName);

                    break;
                case "children":
                    ConsoleHelper.ShowChildren(zoo, uppercaseName);

                    break;
                default:
                    Console.WriteLine("Unknown type. Only animals and guests can be shown.");

                    break;
            }
        }

        /// <summary>
        /// Saves a zoo to a file.
        /// </summary>
        /// <param name="zoo">The zoo to save.</param>
        /// <param name="fileName">The name of the file to save.</param>
        public static void SaveFile(Zoo zoo, string fileName)
        {
            try
            {
                // Save the selected zoo to file.
                zoo.SaveToFile(fileName);

                // Send a message that the zoo has saved.
                Console.WriteLine("Your zoo has been successfully saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save to the specified file name." + ex);
            }
        }

        public static void AttachDelegates(Zoo zoo)
        {
            zoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                Console.WriteLine($"Previous temperature: {previousTemp:0.0}.");
                Console.WriteLine($"New temperature: {currentTemp:0.0}.");
            };
        }

        public static string QueryHelper(Zoo zoo, string query)
        {
            List<Animal> animals = zoo.Animals.ToList();
            string result = "Noting was returned... Something went wrong. Go to the method at line 264 in ConsoleHelper.cs to fix.";

            switch (query)
            {
                case "totalanimalweight":
                    return animals.Sum(a => a.Weight).ToString();

                case "averageanimalweight":
                    return animals.Average(a => a.Weight).ToString();

                case "animalcount":
                    return animals.Count().ToString();

                case "firstheavyanimal":
                    List<Animal> fattestAnimal = new List<Animal>();
                    fattestAnimal.Add(zoo.FindAnimal(a => a.Weight > 200));
                    fattestAnimal.ForEach(a => result = a.ToString());
                    return result;

                case "firstyoungguest":
                    List<Guest> youngest = new List<Guest>();
                    youngest.Add(zoo.FindGuest(g => g.Age <= 10));
                    youngest.ForEach(g => result = g.ToString());
                    return result;

                case "firstfemaledingo":
                    List<Animal> femaleDingos = new List<Animal>();
                    femaleDingos.Add(zoo.FindAnimal(a => a.GetType() == typeof(Dingo) && a.Gender == Gender.Female));
                    femaleDingos.ForEach(a => result = a.ToString());
                    return result;

                case "getyoungguests":
                    List<object> youngList = zoo.GetYoungGuests().ToList();
                    StringBuilder resultBuilder = new StringBuilder();
                    youngList.ForEach(guest => resultBuilder.AppendLine(guest.ToString()));
                    return resultBuilder.ToString();

                case "getfemaledingos":
                    List<object> dingos = zoo.GetFemaleDingos().ToList();
                    StringBuilder resultBuilder3 = new StringBuilder();
                    dingos.ForEach(animal => resultBuilder3.AppendLine(animal.ToString()));
                    return resultBuilder3.ToString();

                case "getheavyanimals":
                    List<object> heavy = zoo.GetHeavyAnimals().ToList();
                    StringBuilder resultBuilder2 = new StringBuilder();
                    heavy.ForEach(animal => resultBuilder2.AppendLine(animal.ToString()));
                    return resultBuilder2.ToString();

                case "getguestsbyage":
                    List<object> guestsAge = zoo.GetGuestsByAge().ToList();
                    StringBuilder resultBuilder4 = new StringBuilder();
                    guestsAge.ForEach(guest => resultBuilder4.AppendLine(guest.ToString()));
                    return resultBuilder4.ToString();

                case "getflyinganimals":
                    List<object> flyingAnimals = zoo.GetFlyingAnimals().ToList();
                    StringBuilder resultBuilder5 = new StringBuilder();
                    flyingAnimals.ForEach(animal => resultBuilder5.AppendLine(animal.ToString()));
                    return resultBuilder5.ToString();

                case "getadoptedanimals":
                    List<object> adoptedAnimals = zoo.GetAdoptedAnimals().ToList();
                    StringBuilder resultBuilder6 = new StringBuilder();
                    adoptedAnimals.ForEach(animal => resultBuilder6.AppendLine(animal.ToString()));
                    return resultBuilder6.ToString();

                case "totalbalancebycolor":
                    List<object> guestWalletColors = zoo.GetTotalBalanceByWalletColor().ToList();
                    StringBuilder resultBuilder7 = new StringBuilder();
                    guestWalletColors.ForEach(guest => resultBuilder7.AppendLine(guest.ToString()));
                    return resultBuilder7.ToString();

                case "averageweightbyanimaltype":
                    List<object> AWT = zoo.GetAverageWeightByAnimalType().ToList();
                    StringBuilder resultBuilder8 = new StringBuilder();
                    AWT.ForEach(animal => resultBuilder8.AppendLine(animal.ToString()));
                    return resultBuilder8.ToString();

                default:
                    return result;
            }
        }

        /// <summary>
        /// Shows a cage in the console.
        /// </summary>
        /// <param name="zoo">The zoo in which the cage resides.</param>
        /// <param name="name">The name of the animal whose cage to show.</param>
        private static void ShowCage(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);


            if (animal != null)
            {
                Cage cage = zoo.FindCage(animal.GetType());

                if (cage != null)
                {
                    Console.WriteLine("Cage found: " + cage.ToString());
                }
            }
            else
            {
                Console.WriteLine("Animal could not be found.");
            }
        }

        /// <summary>
        /// Shows an animal in the console.
        /// </summary>
        /// <param name="zoo">The zoo in which the animal resides.</param>
        /// <param name="name">The name of the animal to show.</param>
        private static void ShowAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);


            if (animal != null)
            {
                Console.WriteLine($"The following animal was found: {animal}.");
            }
            else
            {
                Console.WriteLine("Animal could not be found.");
            }
        }

        /// <summary>
        /// Shows all of ann animal's children.
        /// </summary>
        /// <param name="zoo">The zoo in which the animal lives.</param>
        /// <param name="name">The name of the animal whose children to show.</param>
        private static void ShowChildren(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);


            ConsoleHelper.WalkTree(animal, string.Empty);
        }

        /// <summary>
        /// Walks an animal's family tree.
        /// </summary>
        /// <param name="animal">The animal whose tree to walk.</param>
        /// <param name="prefix">The amount of space to print before the animal's name.</param>
        private static void WalkTree(Animal animal, string prefix)
        {
            Console.WriteLine(prefix + animal.Name);

            foreach (Animal a in animal.Children)
            {
                ConsoleHelper.WalkTree(a, prefix + "  ");
            }
        }

        /// <summary>
        /// Shows a guest in the console.
        /// </summary>
        /// <param name="zoo">The zoo in which the guest resides.</param>
        /// <param name="name">The name of the guest to show.</param>
        private static void ShowGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(a => a.Name == name);


            if (guest != null)
            {
                Console.WriteLine($"The following guest was found: {guest}.");
            }
            else
            {
                Console.WriteLine("Guest could not be found.");
            }
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the animal to remove.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animalToRemove = zoo.FindAnimal(a => a.Name == name);


            try
            {
                zoo.RemoveAnimal(animalToRemove);
                Console.WriteLine(animalToRemove.Name + " was removed from the zoo.");
            }
            catch
            {
                Console.WriteLine("The animal could not be removed.");
            }
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the guest to remove.</param>
        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guestToRemove = zoo.FindGuest(a => a.Name == name);


            try
            {
                zoo.RemoveGuest(guestToRemove);
                Console.WriteLine(guestToRemove.Name + " was removed from the zoo.");
            }
            catch
            {
                Console.WriteLine("The guest could not be removed.");
            }
        }

        /// <summary>
        /// Adds a new animal to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        private static void AddAnimal(Zoo zoo)
        {
            AnimalType animalType = ConsoleUtil.ReadAnimalType();

            Animal animal = AnimalFactory.CreateAnimal(animalType, string.Empty, 0, 1, Gender.Female);

            if (animal == null)
            {
                throw new NullReferenceException("Animal could not be found.");
            }

            animal.Name = ConsoleUtil.InitialUpper(ConsoleUtil.ReadAlphabeticValue("Name"));
            animal.Gender = ConsoleUtil.ReadGender();
            animal.Age = ConsoleUtil.ReadIntValue("Age");
            animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");

            zoo.AddAnimal(animal);

            ConsoleHelper.ShowAnimal(zoo, animal.Name);
        }

        /// <summary>
        /// Adds a new guest to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        private static void AddGuest(Zoo zoo)
        {
            bool success = false;

            Guest guest = new Guest(string.Empty, 0,  0m, WalletColor.Black, Gender.Female, new Account());

            if (guest == null)
            {
                throw new NullReferenceException("Guest could not be found.");
            }

            while (!success)
            {
                try
                {
                    string name = ConsoleUtil.ReadAlphabeticValue("Name");

                    guest.Name = ConsoleUtil.InitialUpper(name);
                    success = true;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Gender = ConsoleUtil.ReadGender();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    int age = ConsoleUtil.ReadIntValue("Age");

                    guest.Age = age;
                    success = true;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    double moneyBalance = ConsoleUtil.ReadDoubleValue("Wallet money balance");

                    guest.Wallet.AddMoney((decimal)moneyBalance);
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Wallet.Color = ConsoleUtil.ReadWalletColor();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    double moneyBalance = ConsoleUtil.ReadDoubleValue("Checking account money balance");

                    guest.CheckingAccount.AddMoney((decimal)moneyBalance);
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Ticket ticket = zoo.SellTicket(guest);

            zoo.AddGuest(guest, ticket);

            ConsoleHelper.ShowGuest(zoo, guest.Name);
        }
    }
}