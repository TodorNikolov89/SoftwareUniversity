using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


public class Spy
{
    public string StealFieldInfo(string className, params string[] fieldsNames)
    {
        StringBuilder sb = new StringBuilder();

        Type type = Type.GetType(className);

        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

        Object classInstance = Activator.CreateInstance(type, new object[] { });

        sb.AppendLine($"Class under investigation: {className}");

        foreach (var field in fields.Where(f => fieldsNames.Contains(f.Name)))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");

        }


        string result = sb.ToString().TrimEnd();
        return result;
    }

    public string AnalyzeAcessModifiers(string className)
    {
        StringBuilder sb = new StringBuilder();

        var classType = Type.GetType(className);

        var fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
        var classPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        var classNonPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            if (!field.IsPrivate)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

        }

        foreach (var method in classNonPublicMethods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} have to be public!");
        }

        foreach (var method in classPublicMethods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} have to be private!");
        }

        string result = sb.ToString().TrimEnd();
        return result;
    }

    public string RevealPrivateMethods(string className)
    {
        StringBuilder sb = new StringBuilder();

        var classType = Type.GetType(className);
        var methods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

        sb.AppendLine($"All Private Methods of Class: {classType}");
        sb.AppendLine($"Base Class: {classType.BaseType.Name}");

        foreach (var method in methods)
        {
            sb.AppendLine(method.Name);
        }

        string result = sb.ToString().TrimEnd();
        return result;
    }

    public string CollectGettersAndSetters(string className)
    {
        StringBuilder sb = new StringBuilder();

        var classType = Type.GetType(className);
        var methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods.Where(m => m.Name.StartsWith("get")))
        {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
        }

        foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
        }


        string result = sb.ToString().TrimEnd();
        return result;
    }

}

