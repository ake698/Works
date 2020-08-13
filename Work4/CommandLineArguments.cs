using System;
using System.Collections.Generic;
using System.Linq;

class CommandLineArguments
{
    // args is arrs like {"-a", "fda", "fads", "--help"}
    public static void Main(string[] args)
    {
        string input = "";
        try
        {
            input = args[0];
        }
        catch
        {
            Console.WriteLine("WARNING: No command line arguments provided.");
            return;
        }
        if (string.IsNullOrEmpty(input.Trim()))
        {
            Console.WriteLine("WARNING: No command line arguments provided.");
            return;
        }
        if (!input.StartsWith("-"))
        {
            Console.WriteLine("WARNING: Parameters must be provided after options.");
            return;
        }
        ParamsParse(args);

    }

    public static void ParamsParse(string[] command)
    {
        //string input = "--list /fasf -grep dafasf fdsf --help fdsfsd -fdf 999 -dfd f-d";

        var args = new List<string>();
        var dic = new Dictionary<string, List<string>>();
        string key = null;
        for (int i = 0; i < command.Length; i++)
        {
            if (command[i].Trim() == "") continue;
            if (command[i].StartsWith("-"))
            {
                if (key != null) dic.Add(key, args);
                key = command[i];
                args = new List<string>();
            }
            else
            {
                args.Add(command[i].Trim());
            }
        }
        dic.Add(key, args);
        foreach (var k in dic.Keys)
        {
            var values = dic[k];
            if (values.Count < 1)
            {
                Console.WriteLine(k);
                continue;
            };
            Console.Write(k+": { ");
            for (int i = 0; i < values.Count; i++)
            {
                if(i != values.Count - 1)
                {
                    Console.Write(values[i] + ", ");
                }
                else
                {
                    Console.Write(values[i]+ " ");
                }
            }
            Console.Write("}");
            Console.WriteLine();
        }
    }
}

