using StorageMaster.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster.Entity.Vehicles
{
    public abstract class Vehicle
    {

        private List<Product> trunk;

        protected Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.trunk = new List<Product>();
        }

        public int Capacity
        {
            get;
        }

        public bool IsFull
        {
            get
            {
                return this.Trunk.Select(x => x.Weight).Sum() >= this.Capacity;
            }

        }

        public bool IsEmpty
        {
            get
            {
                return this.Trunk.Count == 0;
            }

        }

        public IReadOnlyCollection<Product> Trunk => this.trunk.AsReadOnly();

        public void LoadProduct(Product product)
        {
            if (this.IsFull)
            {
                throw new InvalidOperationException("Vehicle is full!");
            }

            this.trunk.Add(product);
        }
        
        public Product Unload()
        {
            if (!this.Trunk.Any())
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }

            Product product = this.trunk[trunk.Count - 1];
            this.trunk.RemoveAt(this.trunk.Count - 1);
            return product;
        }
    }
}
