using AnimalCentre.Controllers.Factories;
using AnimalCentre.Models.Animals;
using AnimalCentre.Models.Contracts;
using AnimalCentre.Models.Hotels;
using AnimalCentre.Models.Procedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Controllers
{
    public class Controller
    {
        private Hotel hotel;
        private Dictionary<string, List<IAnimal>> history;


        public Controller()
        {

            this.history = new Dictionary<string, List<IAnimal>>();
            this.hotel = new Hotel();
        }

        public string RegisterAnimal(string type, string name, int energy, int happiness, int procedureTime)
        {
            CreateAnimalFactory factory = new CreateAnimalFactory();
            IAnimal animal = factory.CreateAnimal(type, name, energy, happiness, procedureTime);
            hotel.Accommodate(animal);

            string result = $"Animal {animal.Name} registered successfully";

            return result;
        }

        public string Chip(string name, int procedureTime)
        {
            IAnimal animal = GetAnimal(name, procedureTime);
            Chip chip = new Chip();
            chip.DoService(animal, procedureTime);

            AddToHistory(chip.GetType().Name, animal);

            string result = $"{animal.Name} had chip procedure";
            return result;
        }

        public string Vaccinate(string name, int procedureTime)
        {
            IAnimal animal = GetAnimal(name, procedureTime);
            Vaccinate vaccinate = new Vaccinate();
            vaccinate.DoService(animal, procedureTime);

            AddToHistory(vaccinate.GetType().Name, animal);

            string result = $"{animal.Name} had vaccination procedure";
            return result;
        }

        public string Fitness(string name, int procedureTime)
        {
            IAnimal animal = GetAnimal(name, procedureTime);
            Fitness fitness = new Fitness();
            fitness.DoService(animal, procedureTime);

            AddToHistory(fitness.GetType().Name, animal);

            string result = $"{animal.Name} had fitness procedure";
            return result;
        }

        public string Play(string name, int procedureTime)
        {
            IAnimal animal = GetAnimal(name, procedureTime);
            Play play = new Play();
            play.DoService(animal, procedureTime);
            AddToHistory(play.GetType().Name, animal);
            string result = $"{animal.Name} was playing for {procedureTime} hours";
            return result;
        }

        public string DentalCare(string name, int procedureTime)
        {
            IAnimal animal = GetAnimal(name, procedureTime);
            DentalCare dentalCare = new DentalCare();
            dentalCare.DoService(animal, procedureTime);

            AddToHistory(dentalCare.GetType().Name, animal);

            string result = $"{animal.Name} had dental care procedure";
            return result;
        }

        public string NailTrim(string name, int procedureTime)
        {
            IAnimal animal = GetAnimal(name, procedureTime);
            NailTrim nailTrim = new NailTrim();
            nailTrim.DoService(animal, procedureTime);

            AddToHistory(nailTrim.GetType().Name, animal);

            string result = $"{animal.Name} had nail trim procedure";
            return result;
        }

        public string Adopt(string animalName, string owner)
        {
            string result = string.Empty;

            if (!this.hotel.Animals.ContainsKey(animalName))
            {
                throw new ArgumentException($"Animal {animalName} does not exist");
            }

            IAnimal animal = this.hotel.Animals[animalName];

            if (animal.IsChipped)
            {
                result = ($"{owner} adopted animal with chip");
            }
            else
            {
                result = ($"{owner} adopted animal without chip");
            }

            hotel.Adopt(animalName, owner);
            return result;
        }

        public string History(string type)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{type}");

            foreach (var animal in this.history[type])
            {
                sb.AppendLine($"    Animal type: {animal.GetType().Name} - {animal.Name} - Happiness: {animal.Happiness} - Energy: {animal.Energy}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        private IAnimal GetAnimal(string name, int procedureTime)
        {
            IAnimal animal = this.hotel.Animals[name];

            return animal;
        }

        public void AddToHistory(string procedureName, IAnimal animal)
        {
            if (!this.history.ContainsKey(procedureName))
            {
                List<IAnimal> current = new List<IAnimal>();
                current.Add(animal);
                this.history.Add(procedureName, current);
            }
            else
            {
                this.history[procedureName].Add(animal);
            }
        }

    }
}
