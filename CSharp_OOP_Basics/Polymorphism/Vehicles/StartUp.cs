using System;
using System.Collections.Generic;
using Vehicles.Models;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();
            string[] busInfo = Console.ReadLine().Split();
            int n = int.Parse(Console.ReadLine());

            Vehicle car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));
            Vehicle truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));
            Vehicle bus = new Truck(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split();

                string command = input[0];
                string vehicle = input[1];

                double distanceOrLitters = double.Parse(input[2]);

                switch (command)
                {
                    case "Drive":
                        Console.WriteLine(vehicle == "Car"
                            ? car.Drive(distanceOrLitters)
                            : truck.Drive(distanceOrLitters));
                        break;
                    case "Refuel":
                        switch (vehicle)
                        {
                            case "Car":
                                car.Refuel(distanceOrLitters);
                                break;
                            case "Truck":
                                truck.Refuel(distanceOrLitters);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }

            
            Console.WriteLine(car.ToString());
            Console.WriteLine(truck.ToString());
        }
    }
}
