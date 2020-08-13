using System;
using System.Collections.Generic;
using System.Text;

namespace Work1
{
    public class Helloword2
    {
        public static void main()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Hello {0} - Welcome to CAB201",name);
            Console.ReadLine();
        }
    }
}
