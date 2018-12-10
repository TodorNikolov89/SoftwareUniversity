namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            var classType = Type.GetType("P01_HarvestingFields.HarvestingFields");

            var fields = classType.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            string result = string.Empty;

            string input = Console.ReadLine();
            while (input != "HARVEST")
            {
                if (input == "protected")
                {
                    var protectedFields = fields.Where(f => f.IsFamily);

                    foreach (var field in protectedFields)
                    {
                        Console.WriteLine($"{field.Attributes.ToString().ToLower().Replace("family", "protected")} {field.FieldType.Name} {field.Name}");
                    }
                }

                if (input == "private")
                {
                    var protectedFields = fields.Where(f => f.IsPrivate);

                    foreach (var field in protectedFields)
                    {
                        Console.WriteLine($"{field.Attributes.ToString().ToLower()} {field.FieldType.Name} {field.Name}");
                    }
                }

                if (input == "public")
                {
                    var protectedFields = fields.Where(f => f.IsPublic);

                    foreach (var field in protectedFields)
                    {
                        Console.WriteLine($"{field.Attributes.ToString().ToLower()} {field.FieldType.Name} {field.Name}");
                    }
                }

                if (input == "all")
                {
                    foreach (var field in fields)
                    {
                        Console.WriteLine($"{field.Attributes.ToString().ToLower().Replace("family", "protected")} {field.FieldType.Name} {field.Name}");
                    }
                }


                input = Console.ReadLine();
            }
        }


    }
}
