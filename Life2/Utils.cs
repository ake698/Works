using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life
{
    public static class Utils
    {
        public static bool EqualList(this List<int[]> source, List<int[]> target)
        {
            if (source.Count != target.Count) return false;

            for (int i = 0; i < source.Count; i++)
            {
                var sourceItem = source[i];
                bool isContain = false;
                foreach (var item in target)
                {
                    if (sourceItem.SequenceEqual(item)) 
                    { 
                        isContain = true; 
                        break; 
                    }
                }
                if (!isContain) return false;
            }

            return true;
        }

        public static bool IntParamCheck(this string source, string title, Predicate<int> predicate, out int result)
        {
            result = -1;
            if (!int.TryParse(source, out int value))
            {
                ConsoleErrorMsg($"{title}: The {title.ToLower()} must be integer.");
                return false;
            }

            if (predicate(value))
            {
                result = value;
                return true;
            }
            return false;
        }


        public static void ConsoleErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] Warning: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleSuccessMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] Success: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
