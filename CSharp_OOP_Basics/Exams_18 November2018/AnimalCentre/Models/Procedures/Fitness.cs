using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Procedures
{
    public class Fitness : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            //•	Fitness – removes 3 happiness and adds 10 energy

            IAnimal currentAnimal = animal;

            if (CheckProcedureTime(currentAnimal.ProcedureTime, procedureTime))
            {
                throw new ArgumentException("Animal doesn't have enough procedure time");
            }

            currentAnimal.ProcedureTime -= procedureTime;
            currentAnimal.Happiness -= 3;
            currentAnimal.Energy += 10;
        }

        public override string History()
        {
            return base.History();
        }
    }
}
