using Accounts;
using Animals;
using BoothItems;
using MoneyCollectors;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Zoos;

namespace ZooConsole
{
    internal static class ConsoleHelper
    {
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    AddAnimal(zoo, ConsoleUtil.ReadAnimalType());
                    break;

                case "guest":
                    AddGuest(zoo);
                    break;

                default:
                    Console.WriteLine("The default in the ProcessAddCommand switch in ConsoleHelper.cs was hit.\n Only animals or guests can be added.");
                    break;
            }
        }

        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            name = ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ShowAnimal(zoo, name);
                    break;

                case "guest":
                    ShowGuest(zoo, name);
                    break;

                default:
                    Console.WriteLine("Please enter a valid type of Animal or Guest. (I.e. Animal, Guest)");
                    break;
            }
        }

        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            switch (type)
            {
                case "animal":
                    string upperName = ConsoleUtil.InitialUpper(name);
                    RemoveAnimal(zoo, upperName);
                    break;

                case "guest":
                    string upperGuest = ConsoleUtil.InitialUpper(name);
                    RemoveGuest(zoo, upperGuest);
                    break;

                default:
                    Console.WriteLine("The default in the ProcessRemoveCommand switch in ConsoleHelper.cs was hit.\n Only animals and guests can be removed.");
                    break;
            }
        }

        public static void SetTemperature(Zoo zoo, string temperature)
        {
            try
            {
                double previousTemp = zoo.BirthingRoomTemperature;
                zoo.BirthingRoomTemperature = Double.Parse(temperature);
                Console.WriteLine($"Previous Temperature: {previousTemp: 0.0} °F.");
                Console.WriteLine($"Current Temperature: {temperature: 0.0} °F.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter numerical digets for the temperature. (i.e. 10)");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A numerical temperature must be entered for command to work.(i.e. temp 10)");
            }
        }

        private static void AddAnimal(Zoo zoo, AnimalType type)
        {

            Animal animal = AnimalFactory.CreateAnimal(type, "Test", 0, 0.0, Reproducers.Gender.Female);

            bool successName = false;

            while (!successName)
            {
                try
                {
                    // set name here
                    animal.Name = ConsoleUtil.ReadAlphabeticValue("Name");
                    ConsoleUtil.InitialUpper(animal.Name);
                    successName = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the animal name being set as an alphabetical single word?");
                }
            }

            bool successGender = false;

            while (!successGender)
            {
                try
                {
                    // set gender here
                    animal.Gender = ConsoleUtil.ReadGender();
                    successGender = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the animal gender being set as an alphabetical single word?");
                }
            }

            bool successAge = false;

            while (!successAge)
            {
                try
                {
                    // set age here
                    animal.Age = ConsoleUtil.ReadIntValue("Age");
                    successAge = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the animal Age being set as an int?");
                }
            }

            bool successWeight = false;

            while (!successWeight)
            {
                try
                {
                    // set weight here
                    animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");
                    successWeight = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the animal weight being set as a double? ");
                }
            }
            zoo.AddAnimal(animal);
            ShowAnimal(zoo, animal.Name);
        }

        private static void AddGuest(Zoo zoo)
        {
            Account guestAccount = new Account();
            Guest guest = new Guest("TestGuest", 0, guestAccount, 0, WalletColor.Brown, Reproducers.Gender.Male);
            bool successName = false;

            while (!successName)
            {
                try
                {
                    // set name here
                    guest.Name = ConsoleUtil.ReadAlphabeticValue("Name");
                    ConsoleUtil.InitialUpper(guest.Name);
                    successName = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the guest name being set as an alphabetical single word?");
                }
            }

            bool successGender = false;

            while (!successGender)
            {
                try
                {
                    // set gender here
                    guest.Gender = ConsoleUtil.ReadGender();
                    successGender = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the guest gender being set as an alphabetical single word?");
                }
            }

            bool successAge = false;

            while (!successAge)
            {
                try
                {
                    // set age here
                    guest.Age = ConsoleUtil.ReadIntValue("Age");
                    successAge = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the guest Age being set as an int?");
                }
            }

            bool successWalletMoneyBalance = false;

            while (!successWalletMoneyBalance)
            {
                try
                {
                    // set moneyBalance here
                    double moneyToAdd = ConsoleUtil.ReadDoubleValue("Wallet Money Balance");
                    guest.Wallet.AddMoney((decimal)moneyToAdd);
                    successWalletMoneyBalance = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the guest Age being set as an int?");
                }
            }

            bool successWalletColor = false;

            while (!successWalletColor)
            {
                try
                {
                    // set wallet color here
                    guest.Wallet.Color = ConsoleUtil.ReadWalletColor();
                    successWalletColor = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the guest wallet color being set as an alphabetical single word?");
                }
            }

            bool successCheckingBalance = false;

            while (!successCheckingBalance)
            {
                try
                {
                    // set checking balance here
                    double moneyToAdd = ConsoleUtil.ReadDoubleValue("Checking Account Balance");
                    guest.CheckingAccount.AddMoney((decimal)moneyToAdd);
                    successCheckingBalance = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Is the guest Age being set as an int?");
                }
            }

            Ticket ticket = zoo.SellTicket(guest);

            zoo.AddGuest(guest, ticket);
            ShowGuest(zoo, guest.Name);
        }

        private static void RemoveAnimal(Zoo zoo, string name)
        {
            try
            {
                string animalName = name;
                Animal animal = zoo.FindAnimal(animalName);

                if (animal != null)
                {
                    zoo.RemoveAnimal(animal);
                    Console.WriteLine($"The following animal was removed: {animal}");
                }
                else
                {
                    Console.WriteLine("The searched animal couldn't be found.");
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter alphabetical letters for the animal. (i.e. Dolly)");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An alphabetical lettered word must be entered for command to work.(i.e. remove animal (Name of Animal).)");
            }
        }

        private static void RemoveGuest(Zoo zoo, string name)
        {
            try
            {
                string guestName = name;
                Guest guest = zoo.FindGuest(guestName);

                if (guest != null)
                {
                    zoo.RemoveGuest(guest);
                    Console.WriteLine($"The following guest was removed: {guest}");
                }
                else
                {
                    Console.WriteLine("The searched guest couldn't be found.");
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter alphabetical letters for the guest. (i.e. Bob)");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An alphabetical lettered word must be entered for command to work.(i.e. remove guest (Name of guest).)");
            }
        }

        private static void ShowAnimal(Zoo zoo, string name)
        {
            try
            {
                string animalName = name;
                Animal animal = zoo.FindAnimal(animalName);

                if (animal != null)
                {
                    Console.WriteLine($"The following animal was found: {animal}");
                }
                else
                {
                    Console.WriteLine("The searched animal couldn't be found.");
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter alphabetical letters for the animal. (i.e. Dolly)");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An alphabetical lettered word must be entered for command to work.(i.e. show animal (Name of Animal).)");
            }
        }

        private static void ShowGuest(Zoo zoo, string name) 
        {
            try
            {
                string guestName = name;

                Guest guest = zoo.FindGuest(guestName);

                if (guest != null)
                {
                    Console.WriteLine($"The following guest was found: {guest}");
                }
                else
                {
                    Console.WriteLine("The searched guest couldn't be found.");
                }

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter alphabetical letters for the guest. (i.e. Ethel)");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An alphabetical lettered word must be entered for command to work.(i.e. show guest (name of guest).)");
            }
        }
    }
}
