using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    public static class AnimalFactory
    {
        public static Animal CreateAnimal(AnimalType type, string name, int age, double weight, Gender gender)
        {
            Animal animal = null;
            switch (type)
            {
                case AnimalType.Chimpanzee:
                    animal = new Chimpanzee(name, age, weight, gender);
                    break;
                case AnimalType.Dingo:
                    animal = new Dingo(name, age, weight, gender);
                    break;
                case AnimalType.Eagle:
                    animal = new Eagle(name, age, weight, gender);
                    break;
                case AnimalType.Hummingbird:
                    animal = new Hummingbird(name, age, weight, gender);
                    break;
                case AnimalType.Kangaroo:
                    animal = new Kangaroo(name, age, weight, gender);
                    break;
                case AnimalType.Ostrich:
                    animal = new Ostrich(name, age, weight, gender);
                    break;
                case AnimalType.Platypus:
                    animal = new Platypus(name, age, weight, gender);
                    break;
                case AnimalType.Shark:
                    animal = new Shark(name, age, weight, gender);
                    break;
                case AnimalType.Squirrel:
                    animal = new Squirrel(name, age, weight, gender);
                    break;
                default:
                    break;
            }
            return animal;
        }
    }
}
