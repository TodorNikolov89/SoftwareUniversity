namespace Travel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    public class Airport : IAirport
    {
        private readonly List<IBag> takenBags;
        private readonly List<IBag> notTakenBags;
        private readonly List<ITrip> adventures;
        private readonly List<IPassenger> people;

        public Airport()
        {
            this.takenBags = new List<IBag>();
            this.notTakenBags = new List<IBag>();
            this.adventures = new List<ITrip>();
            this.people = new List<IPassenger>();
        }

        public IReadOnlyCollection<IBag> CheckedInBags => this.takenBags;

        public IReadOnlyCollection<IBag> ConfiscatedBags => this.notTakenBags;

        public IReadOnlyCollection<IPassenger> Passengers => this.people;

        public IReadOnlyCollection<ITrip> Trips => this.adventures;

        public IPassenger GetPassenger(string username) => this.people.FirstOrDefault(x => x.Username == username);

        public ITrip GetTrip(string id) => this.adventures.FirstOrDefault(x => x.Id == id);

        public void AddPassenger(IPassenger passenger) => this.people.Add(passenger);

        public void AddTrip(ITrip trip) => this.adventures.Add(trip);

        public void AddCheckedBag(IBag bag) => this.takenBags.Add(bag);

        public void AddConfiscatedBag(IBag bag) => this.notTakenBags.Add(bag);
    }
}