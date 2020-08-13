using System;

namespace LinearSearch
{
    class Program
    {
        static void main(string[] args)
        {
            string[] words = { "the", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            int location;

            Console.WriteLine("String array to be searched:");
            DisplayArray(words);

            location = LinearSearch(words, "the");
            OutputSearchResult(location, "the");

            location = LinearSearch(words, "fox");
            OutputSearchResult(location, "fox");

            location = LinearSearch(words, "zebra");
            OutputSearchResult(location, "zebra");

            ExitPrompt();
        }

        public static void DisplayArray(string[] words)
        {
            foreach (string element in words)
            {
                Console.Write("\t" + element + "\n");
            }
            Console.WriteLine();
        }


        public static int LinearSearch(string[] words, string word)
        {
            // TODO: Search code goes here... 
            // Remove the line below when you are done.
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals(word)) return i;
            }
            return -1;
        }

        public static void OutputSearchResult(int pos, string word)
        {
            if (pos < 0)
            {
                Console.WriteLine($"The word \"{word}\" not found in the list\n ");
            }
            else
            {
                Console.WriteLine($"The word \"{word}\" found in position {pos} of the array\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();

        }

        public static void ExitPrompt()
        {
            Console.WriteLine("Press any key to exit program...");
            Console.ReadKey();
        }

    }

}

