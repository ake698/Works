using System;
using System.Collections.Generic;
using System.Text;

namespace Work1
{
    public class StringOrder
    {
        enum Order { Precedes = -1, Equals = 0, Follows = 1 };
        static void Main(string[] args)
        {
            Console.WriteLine("What is name A?");
            string A = Console.ReadLine();
            Console.WriteLine("What is name B?");
            string B = Console.ReadLine();
            A = A.Split(" ")[1];
            B = B.Split(" ")[1];
            var result = string.Compare(A, B);
            var state = Enum.GetName(typeof(Order), result);
            Console.WriteLine("{0} {1} {2}", A, state, B);
            Console.ReadLine();
        }

    }
}
