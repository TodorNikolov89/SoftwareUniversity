using System;

namespace Tuple
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] personInfo = Console.ReadLine().Split();

            string fullName = personInfo[0] + " " + personInfo[1];
            string neightborhood = personInfo[2];
            string town = personInfo[3];

            string[] beerInfo = Console.ReadLine().Split();

            string name = beerInfo[0];
            int litters = int.Parse(beerInfo[1]);
            bool isDrunk = beerInfo[2] == "drunk" ? true : false;

            string[] info = Console.ReadLine().Split();

            string currentName = info[0];
            double value = double.Parse(info[1]);
            string bank = info[2];

            SpecialTuple<string, string, string> personTuple = new SpecialTuple<string, string, string>(fullName, neightborhood, town);
            SpecialTuple<string, int, bool> beerTuple = new SpecialTuple<string, int, bool>(name, litters, isDrunk);
            SpecialTuple<string, double, string> specialTuple = new SpecialTuple<string, double, string>(currentName, value, bank);

            Console.WriteLine(personTuple);
            Console.WriteLine(beerTuple);
            Console.WriteLine(specialTuple);

        }
    }
}
