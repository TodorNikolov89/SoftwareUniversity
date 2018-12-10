namespace P02_BlackBoxInteger
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            var type = typeof(BlackBoxInteger);

            var instanceClass = (BlackBoxInteger)Activator.CreateInstance(type, true);

            string line = Console.ReadLine();
            while (line != "END")
            {
                string[] info = line.Split('_');

                string command = info[0];
                int value = int.Parse(info[1]);
                
                var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .First(o => o.Name == command);

                method.Invoke(instanceClass, new object[] { value });

                var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .First(x => x.Name == "innerValue")
                    .GetValue(instanceClass);

                Console.WriteLine(field);

                line = Console.ReadLine();
            }
        }
    }
}
