using System;
using System.Collections.Generic;
using System.Text;


public class RandomArray
{
    public static void main()
    {
        var a = Unique(20);
        Console.WriteLine(a);
        for (int i = 0; i < a.Length; i++)
        {
            Console.WriteLine(a[i]);
        }
    }


    public static int[] Unique(int size)
    {

        var arrs = new int[size];
        var result = new int[size];

        for (int i = 1; i <= size; i++)
        {
            arrs[i - 1] = i;
        }

        for (int i = 0; i < size; i++)
        {
            Random rnd = new Random();
            int index = rnd.Next(size - i);
            result[i] = arrs[index];
            MoveArrs(arrs, index);
        }

        return result;
    }
    private static void MoveArrs(int[] arrs, int index)
    {
        for (int i = index; i < arrs.Length - 1; i++)
        {
            arrs[i] = arrs[i + 1];
        }
    }
}
