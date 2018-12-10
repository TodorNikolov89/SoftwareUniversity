using AnimalCentre.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre.Core
{
    public class Engine
    {
        private Controller begin;
        private Dictionary<string, List<string>> adoptedAnimals;
        public Engine()
        {
            begin = new Controller();
            this.adoptedAnimals = new Dictionary<string, List<string>>();
        }

        public void Run()
        {
            string line = Console.ReadLine();

            string type = string.Empty;
            string name = string.Empty;
            string ownerName = string.Empty;
            string procedureType = string.Empty;
            int energy = 0;
            int happiness = 0;
            int producedTime = 0;

            while (line != "End")
            {
                string[] info = line.Split();
                string result = string.Empty;
                string command = info[0];
                try
                {
                    switch (command)
                    {
                        case "RegisterAnimal":
                            type = info[1];
                            name = info[2];
                            energy = int.Parse(info[3]);
                            happiness = int.Parse(info[4]);
                            producedTime = int.Parse(info[5]);

                            result = begin.RegisterAnimal(type, name, energy, happiness, producedTime);
                            break;
                        case "Chip":
                            name = info[1];
                            producedTime = int.Parse(info[2]);
                            result = begin.Chip(name, producedTime);
                            break;
                        case "Vaccinate":
                            name = info[1];
                            producedTime = int.Parse(info[2]);
                            result = begin.Vaccinate(name, producedTime);
                            break;
                        case "Fitness":
                            name = info[1];
                            producedTime = int.Parse(info[2]);
                            result = begin.Fitness(name, producedTime);
                            break;
                        case "Play":
                            name = info[1];
                            producedTime = int.Parse(info[2]);
                            result = begin.Play(name, producedTime);
                            break;
                        case "DentalCare":
                            name = info[1];
                            producedTime = int.Parse(info[2]);
                            result = begin.DentalCare(name, producedTime);
                            break;
                        case "NailTrim":
                            name = info[1];
                            producedTime = int.Parse(info[2]);
                            result = begin.NailTrim(name, producedTime);
                            break;
                        case "Adopt":
                            name = info[1];
                            ownerName = info[2];
                            result = begin.Adopt(name, ownerName);
                            AddToAdoptedAnimals(ownerName, name);
                            break;
                        case "History":
                            procedureType = info[1];
                            result = begin.History(procedureType);
                            break;
                    }
                }
                catch (ArgumentException argEx)
                {
                    result = argEx.GetType().Name + ": " + argEx.Message;
                    //Console.WriteLine(argEx.Message);
                }
                catch (InvalidOperationException invalidEx)
                {
                    result = invalidEx.GetType().Name + ": " + invalidEx.Message;
                    // Console.WriteLine(invalidEx.Message);
                }
                Console.WriteLine(result);

                line = Console.ReadLine();
            }

            foreach (var owner in adoptedAnimals.OrderBy(x => x.Key))
            {
                Console.WriteLine($"--Owner: { owner.Key}");
                Console.WriteLine($"    - Adopted animals: {string.Join(" ", owner.Value)}");
            }
        }
        public void AddToAdoptedAnimals(string ownerName, string animalName)
        {
            if (!this.adoptedAnimals.ContainsKey(ownerName))
            {
                List<string> current = new List<string>();
                current.Add(animalName);
                this.adoptedAnimals.Add(ownerName, current);
            }
            else
            {
                this.adoptedAnimals[ownerName].Add(animalName);
            }
        }
    }
}
