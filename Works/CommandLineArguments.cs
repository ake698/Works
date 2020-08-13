using System;


class CommandLineArguments
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Arguments: ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input.Trim()))
            {
                Console.WriteLine("WARNING: No command line arguments provided.");
                break;
            }
            if (!input.StartsWith("-"))
            {
                Console.WriteLine("WARNING: Parameters must be provided after options.");
                break;
            }

            ParamsParse(input);
        }
    }

    public static void ParamsParse(string input)
    {
        //string input = "--list /fasf -grep dafasf fdsf --help fdsfsd -fdf 999 -dfd f-d";

        var command = input.Split(" ");
        List<string> args = new List<string>();
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        string key = null;
        for (int i = 0; i < command.Length; i++)
        {
            //Console.WriteLine(command[i]);
            if (command[i].StartsWith("-"))
            {
                if (key != null) dic.Add(key, args);
                key = command[i];
                args = new List<string>();
            }
            else
            {
                args.Add(command[i]);
            }
        }
        dic.Add(key, args);
        foreach (var k in dic.Keys)
        {
            Console.WriteLine(k);
            var values = dic[k];
            values.ForEach(x => Console.Write(x + "  "));
            Console.WriteLine();
        }
    }
}

