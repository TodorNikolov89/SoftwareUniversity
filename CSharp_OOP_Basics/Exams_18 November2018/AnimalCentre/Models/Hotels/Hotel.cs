using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Models.Hotels
{
    public class Hotel : IHotel
    {
        private int capacity;
        private Dictionary<string, IAnimal> animals;

        public Hotel()
        {
            this.animals = new Dictionary<string, IAnimal>();
            this.Capacity = 10;
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public void Accommodate(IAnimal animal)
        {
            if (this.Animals.Count >= this.Capacity)
            {
                throw new InvalidOperationException("Not enough capacity");
            }

            if (this.Animals.ContainsKey(animal.Name))
            {
                throw new ArgumentException($"Animal {animal.Name} already exist");
            }

            this.animals.Add(animal.Name, animal);
        }

        public void Adopt(string animalName, string owner)
        {
            if (!this.Animals.ContainsKey(animalName))
            {
                throw new ArgumentException($"Animal {animalName} does not exist");
            }

            IAnimal animal = this.Animals[animalName];

            animal.Owner = owner;
            animal.IsAdopt = true;
            this.animals.Remove(animalName);
        }

        public IReadOnlyDictionary<string, IAnimal> Animals => this.animals;
    }
}
