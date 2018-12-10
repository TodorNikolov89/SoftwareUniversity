using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Procedures
{
    public class Vaccinate : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            IAnimal currentAnimal = animal;

            if (CheckProcedureTime(currentAnimal.ProcedureTime, procedureTime))
            {
                throw new ArgumentException("Animal doesn't have enough procedure time");
            }

            currentAnimal.ProcedureTime -= procedureTime;
            currentAnimal.Energy -= 8;
            currentAnimal.IsVaccinated = true;
        }

        public override string History()
        {
            return base.History();
        }

    }
}
