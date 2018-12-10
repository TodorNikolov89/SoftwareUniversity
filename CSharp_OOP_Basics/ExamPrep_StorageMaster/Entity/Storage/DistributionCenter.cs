using StorageMaster.Entity.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Storages
{
    public class DistributionCenter : Storage
    {
        private static int DistributionCenterCapacity = 2;
        private static int DistributionCenterGarageSlots = 5;

        private static Vehicle[] defaultVehicles = new Vehicle[]
        {
            new Van(),
            new Van(),
            new Van()
        };

        public DistributionCenter(string name)
            : base(name, DistributionCenterCapacity, DistributionCenterGarageSlots, defaultVehicles)
        {
        }
    }
}
