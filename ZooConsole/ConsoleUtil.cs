using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;

namespace ZooConsole
{
    internal static class ConsoleUtil
    {
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

        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Eagle;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Animal");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<AnimalType>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid Animal Type.");
                }
            }

            return result;
        }

        public static WalletColor ReadWalletColor()
        {
            WalletColor result = WalletColor.Brown;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Wallet Color");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<WalletColor>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid Wallet Color.");
                }
            }

            return result;
        }

        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        public static double ReadDoubleValue(string prompt)
        {
            double result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        public static Gender ReadGender()
        {
            Gender result = Gender.Female;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<Gender>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender.");
                }
            }

            return result;
        }

        public static int ReadIntValue(string prompt)
        {
            int result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        public static string ReadStringValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "] ");

                string stringValue = Console.ReadLine().ToLower().Trim();

                Console.WriteLine();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }
    }
}
