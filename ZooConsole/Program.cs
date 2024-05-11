using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Animals;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// Contains interaction logic for the console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private static Zoo zoo;

        /// <summary>
        /// The program's main (start-up) method.
        /// </summary>
        /// <param name="args">The method arguments for the console application.</param>
        public static void Main(string[] args)
        {
            // Set window title.
            Console.Title = "Object-Oriented Programming 2: Zoo";

            // Write introductory message.
            Console.WriteLine("Welcome to the Como Zoo!");

            // Create zoo instance.
             zoo = Zoo.NewZoo();
            ConsoleHelper.AttachDelegates(zoo);

            bool exit = false;

            string command;

            try
            {
                while (!exit)
                {
                    Console.Write("] ");
                    command = Console.ReadLine();
                    string[] commandWords = command.ToLower().Trim().Split();

                    switch (commandWords[0])
                    {
                        case "exit":
                            exit = true;
                            break;
                        case "restart":
                            zoo = Zoo.NewZoo();
                            ConsoleHelper.AttachDelegates(zoo);
                            Console.WriteLine("A new Como Zoo has been created.");
                            break;
                        case "help":
                            if (commandWords.Length == 1)
                            {
                                ConsoleHelper.ShowHelp();
                            }
                            else if (commandWords.Length == 2)
                            {
                                ConsoleHelper.ShowHelpDetail(commandWords[1]);
                            }
                            else
                            {
                                Console.WriteLine("Too many parameters were entered. The help command takes 1 or 2 parameters.");
                            }

                            break;
                        case "temp":
                            try
                            {
                                ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter a parameter for temperature.");
                            }

                            break;
                        case "show":
                            try
                            {
                                ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest] [name].");
                            }

                            break;
                        case "remove":
                            try
                            {
                                ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest] [name].");
                            }

                            break;
                        case "add":
                            try
                            {
                                ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest].");
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Zoo is out of tickets.");
                            }

                            break;
                        case "sort":
                            try
                            {
                                if (commandWords[1] == "animals")
                                {
                                    SortResult result = zoo.SortAnimals(commandWords[2], commandWords[3], commandWords[1], zoo.Animals.ToList());
                                    Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                                    Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                                    Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());
                                
                                    Console.WriteLine("COMPARE COUNT: " + result.CompareCount);
                                    Console.WriteLine("SWAP COUNT: " + result.SwapCount);

                                    // Console.WriteLine("TIME: " + result.ElapsedMilliseconds);
                                    foreach (Animal a in result.Objects)
                                    {
                                        Console.WriteLine(a.ToString());
                                    }
                                }
                                else if (commandWords[1] == "guests")
                                {
                                    SortResult result = zoo.SortGuests(commandWords[2], commandWords[3], commandWords[1], zoo.Guests.ToList());
                                    Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                                    Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                                    Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());

                                    Console.WriteLine("COMPARE COUNT: " + result.CompareCount);
                                    Console.WriteLine("SWAP COUNT: " + result.SwapCount);

                                    // Console.WriteLine("TIME: " + result.ElapsedMilliseconds);
                                    foreach (Guest g in result.Objects)
                                    {
                                        Console.WriteLine(g.ToString());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort type] [list to sort, either animals or guests]." + ex.Message);
                            }

                            break;
                        case "search":
                           if (commandWords[1] == "linear")
                            {
                                int loopCount = 0;                          
                                SortResult animals = zoo.SortObjects("bubble", "name", "animal", zoo.Animals.ToList());
                                string animalName = ConsoleUtil.InitialUpper(commandWords[2]);

                                if (!string.IsNullOrWhiteSpace(animalName))
                                {
                                    foreach (Animal a in animals.Objects)
                                    {
                                        loopCount++;
                                        if (a.Name == animalName)
                                        {
                                            Console.WriteLine(string.Format($"{a.Name} found. {loopCount} loops complete."));
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (commandWords[1] == "binary")
                            {
                                int loopCount = 0;

                                string animalName = ConsoleUtil.InitialUpper(commandWords[2]);
                                SortResult animals = zoo.SortObjects("bubble", "name", "animal", zoo.Animals.ToList());

                                if (!string.IsNullOrWhiteSpace(animalName))
                                {
                                    int minPosition = 0;
                                    int maxPosition = animals.Objects.Count - 1;

                                    while (minPosition <= maxPosition)
                                    {
                                        int middlePosition = (minPosition + maxPosition) / 2;

                                        loopCount++;
                                        int comparer = string.Compare(animalName, ((Animal)animals.Objects[middlePosition]).Name);

                                        if (comparer > 0)
                                        {
                                            minPosition = middlePosition + 1;
                                        }
                                        else if (comparer < 0)
                                        {
                                            maxPosition = middlePosition - 1;
                                        }
                                        else
                                        {
                                            Console.WriteLine(string.Format("{0} found. {1} loops complete.", ((Animal)animals.Objects[middlePosition]).Name, loopCount));
                                            break;
                                        }
                                    }
                                }
                            }

                            break;
                        case "save":
                            try
                            {
                                ConsoleHelper.SaveFile(zoo, commandWords[1]);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a name for the file you wish to save.");
                            }

                            break;
                        case "load":
                            try
                            {
                                zoo = ConsoleHelper.LoadFile(commandWords[1]);
                                ConsoleHelper.AttachDelegates(zoo);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter the file name you wish to load.");
                            }

                            break;
                        case "query":
                            string query = ConsoleHelper.QueryHelper(zoo, commandWords[1]);
                            Console.WriteLine(query);

                            break;

                        default:
                            Console.WriteLine("Invalid command entered.");

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}