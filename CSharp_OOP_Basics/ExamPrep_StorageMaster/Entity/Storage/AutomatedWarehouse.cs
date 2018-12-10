using StorageMaster.Entity.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Entity.Storages
{
    public class AutomatedWarehouse : Storage
    {
        private static int automatedWarehouseCapacity = 1;
        private static int automatedWarehouseGarageSlots = 2;
        private static Vehicle[] defaultvehicles = new Vehicle[]
        {
            new Truck()
        };

        public AutomatedWarehouse(string name)
            : base(name, automatedWarehouseCapacity, automatedWarehouseGarageSlots, defaultvehicles)
        {
        }
    }
}
