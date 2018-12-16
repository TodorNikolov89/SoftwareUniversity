// REMOVE any "using" statements, which start with "Travel." BEFORE SUBMITTING

namespace Travel.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Travel.Core.Controllers;
    using Travel.Entities;
    using Travel.Entities.Airplanes;
    using Travel.Entities.Airplanes.Contracts;
    using Travel.Entities.Contracts;
    using Travel.Entities.Factories;
    using Travel.Entities.Items;

    [TestFixture]
    public class FlightControllerTests
    {

        [Test]
        public void ValidateTakeoffMethod()
        {
            var airport = new Airport();

            var airplane = new LightAirplane();
            
            var passengers = new Passenger[10];


            for (int i = 0; i < passengers.Length; i++)
            {
                passengers[i] = new Passenger("Todor" + i);
                airplane.AddPassenger(passengers[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    var bag = new Bag(passengers[i], new Item[] { new Colombian() });
                    passengers[i].Bags.Add(bag);
                }
                else
                {
                    var bag = new Bag(passengers[i], new Item[] { new Toothbrush() });
                    passengers[i].Bags.Add(bag);
                }
            }

            var trip = new Trip("Sofiq", "Varna", airplane);
            airport.AddTrip(trip);

            var complatedTrip = new Trip("Varna", "Sofiq", new MediumAirplane());
            complatedTrip.Complete();
            airport.AddTrip(complatedTrip);

            FlightController flightController = new FlightController(airport);

            var actualResult = flightController.TakeOff();
            var expectedResult = "SofiqVarna1:\r\nOverbooked! Ejected Todor1, Todor0, Todor3, Todor7, Todor8\r\nConfiscated 3 bags ($50006)\r\nSuccessfully transported 5 passengers from Sofiq to Varna.\r\nConfiscated bags: 3 (3 items) => $50006";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(trip.IsCompleted, true);

        }

    }

}
