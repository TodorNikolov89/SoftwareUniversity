using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Procedures
{
    public class Play : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            IAnimal currentAnimal = animal;

            if (CheckProcedureTime(currentAnimal.ProcedureTime, procedureTime))
            {
                throw new ArgumentException("Animal doesn't have enough procedure time");
            }

            currentAnimal.ProcedureTime -= procedureTime;
            currentAnimal.Energy -= 6;
            currentAnimal.Happiness += 12;

        }

        public override string History()
        {
            return base.History();
        }
    }
}
