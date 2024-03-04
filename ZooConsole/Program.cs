using System;
using Zoos;
using Animals;
using People;
using System.Runtime.CompilerServices;

namespace ZooConsole
{
    public class Program
    {
        private static Zoo zoo;

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Como Zoo!");
            Console.Title = "Object-Oriented Programming 2: Zoo";

            bool bContinue = true;
            zoo = Zoo.NewZoo();
            while (bContinue)
            {
                string command = string.Empty;
                Console.Write("] ");
                command = Console.ReadLine();

                command = command.ToLower().Trim();
                string[] commandWords = command.Split();

                

                switch (commandWords[0])
                {
                    case "exit":
                        bContinue = false;
                        break;

                    case "restart":
                        zoo = Zoo.NewZoo();
                        zoo.BirthingRoomTemperature = 77.0;
                        Console.WriteLine("A new Como Zoo has been created.");
                        break;

                    case "help":
                        Console.WriteLine("Known commands:\nHELP: Shows a list of known commands." +
                            "\nEXIT: Exits the application.\nRESTART: Creates the ComoZoo. " +
                            "\nTEMP: Sets birthingroom temp. " +
                            "\nSHOW ANIMAL [animal name]: Displays information for specified animal." +
                            "\nSHOW GUEST [guest name]: Displays information for specified guest." +
                            "\nADD ANIMAL: Adds an animal to the zoo." +
                            "\nADD GUEST: Adds a guest to the zoo." +
                            "\nREMOVE ANIMAL [animal name]: Removes the specified animal from the zoo." +
                            "\nREMOVE GUEST [guest name]: Removes the specified guest from the zoo.");
                        break;

                    case "temp":
                        
                        try
                        {
                            ConsoleHelper.SetTemperature(zoo, commandWords[1]);
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

                        break;

                    case "show":
                        try
                        {
                            ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter alphabetical characters for the show method.. (i.e. show Animal name)");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("A valid type must be entered for command to work.(i.e. show Animal name)");
                        }

                        break;

                    case "add":
                        try
                        {
                            ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                        }
                        catch (NullReferenceException ex)
                        {
                            throw new NullReferenceException(ex.Message);
                        }
                        break;

                    case "remove":
                        ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        /// <summary>
        /// Converts all input names to proper capitalized words.
        /// </summary>
        /// <returns>The capitalized word.</returns>
        public static string InitialUpper(string value)
        {
            if (value != null && value.Length > 0)
            value = char.ToUpper(value[0]) + value.Substring(1);

            else
            {
                throw new Exception("Word character length must be greater than 0");
            }
            return value;
        }
    }
}
