using Animals;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zoos
{
    public static class ZooExtensions
    {
        /// <summary>
        /// Find an animal based on type.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public static Animal FindAnimal(this Zoo zoo, Predicate<Animal> match)
        {
            return zoo.Animals.ToList().Find(match);
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="name">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo zoo, Predicate<Guest> match)
        {
            return zoo.Guests.ToList().Find(match);
        }

        /// <summary>
        /// Flattens a list of values to a delimited string.
        /// </summary>
        /// <param name="list">The list of values to flatten.</param>
        /// <param name="separator">A string to insert as a delimiter between each of the values.</param>
        /// <returns>A flattened string.</returns>
        public static string Flatten(this IEnumerable<string> list, string separator)
        {
            string result = null;

            list.ToList().ForEach(s => result = result == null ? s : separator + s);

            return result;
        }

        public static IEnumerable<object> GetYoungGuests(this Zoo zoo)
        {
            var result =
                zoo.Guests
                .Where(g => g.Age <= 10)
                .Select(g => new { g.Name, g.Age })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetFemaleDingos(this Zoo zoo)
        {
            var result =
                zoo.Animals
                .Where(a => a.GetType() == typeof(Dingo) && a.Gender == Reproducers.Gender.Female)
                .Select(a => new { a.Name, a.Age, a.Weight, a.Gender })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetHeavyAnimals(this Zoo zoo)
        {
            var result =
                zoo.Animals
                .Where(a => a.Weight > 200)
                .Select(a => new { Type = a.GetType().Name, a.Name, a.Age, a.Weight })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetGuestsByAge(this Zoo zoo)
        {
            var result =
                zoo.Guests
                .OrderBy(g => g.Age)
                .Select(g => new { g.Name, g.Age, g.Gender })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetFlyingAnimals(this Zoo zoo)
        {
            var result =
                zoo.Animals
                .Where(a => a.MoveBehavior.GetType().Equals(typeof(FlyBehavior)))
                .Select(a => new { Type = a.GetType().Name, a.Name })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetAdoptedAnimals(this Zoo zoo)
        {
            var result =
                zoo.Guests
                .Where(g => g.AdoptedAnimal != null)
                .Select(g => new { Adopter = g.Name, Name = g.AdoptedAnimal != null ? g.AdoptedAnimal.Name : null, Type = g.AdoptedAnimal != null ? g.AdoptedAnimal.GetType().Name : null })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo zoo)
        {
            var result =
                zoo.Guests
                .Where(g => g.Wallet != null)
                .OrderBy(g => g.Wallet.Color)
                .Select(g => new { Key = g.Wallet.Color, TotalMoneyBalance = g.Wallet.MoneyBalance })
                .GroupBy(g => g.Key)
                .Select(group => new { Color = group.Key, TotalBalance = group.Sum(item => item.TotalMoneyBalance) })
                .ToList();

            return result;
        }

        public static IEnumerable<object> GetAverageWeightByAnimalType(this Zoo zoo)
        {
            var result =
                zoo.Animals
                .GroupBy(a => a.GetType().Name)
                .Select(a => new { Type = a.Key, AverageWeight = a.Average(animal => animal.Weight) })
                .ToList();

            return result;
        }
    }
}
