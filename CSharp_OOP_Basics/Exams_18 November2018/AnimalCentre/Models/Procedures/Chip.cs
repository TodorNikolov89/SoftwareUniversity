using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Procedures
{
    public class Chip : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            IAnimal currentAnimal = animal;

            if (CheckProcedureTime(currentAnimal.ProcedureTime, procedureTime))
            {
                throw new ArgumentException("Animal doesn't have enough procedure time");
            }

            currentAnimal.ProcedureTime -= procedureTime;
            currentAnimal.Happiness -= 5;

            if (currentAnimal.IsChipped)
            {
                throw new ArgumentException($"{currentAnimal.Name} is already chipped");
            }

            currentAnimal.IsChipped = true;

        }

        public override string History()
        {
            return base.History();
        }
    }
}
