using System;
using System.Text;


public class MatrixLibrary
{
    public static void Main()
    {
        int[,] A = new int[3, 2]
        {
            {0, 1 },
            {2, 3 },
            {4, 5 },
        };
        Console.WriteLine($"Original Matrix (A):\n\n{MatrixToString(A)}\n");

        int[,] A2 = MatrixAdd(A, A);
        Console.WriteLine($"A + A:\n\n{MatrixToString(A2)}\n");

        int[,] AT = MatrixTranspose(A);
        Console.WriteLine($"A Transposed:\n\n{MatrixToString(AT)}\n");
    }
    public static string MatrixToString(int[,] input)
    {
        int row = input.GetLength(0);
        int cell = input.GetLength(1);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < row; i++)
        {
            for (int p = 0; p < cell; p++)
            {
                stringBuilder.Append(string.Format($"{input[i, p],4}"));
            }
            stringBuilder.Append("\n");
        }
        return stringBuilder.ToString();

    }

    public static int[,] MatrixTranspose(int[,] input)
    {
        int row = input.GetLength(0);
        int cell = input.GetLength(1);

        int[,] result = new int[cell, row];

        for (int i = 0; i < row; i++)
        {
            for (int p = 0; p < cell; p++)
            {
                result[p, i] = input[i, p];
            }
        }
        return result;
    }

    public static int[,] MatrixAdd(int[,] input1, int[,] input2)
    {
        int row = input1.GetLength(0);
        int cell = input1.GetLength(1);
        int[,] result = new int[row, cell];
        for (int i = 0; i < row; i++)
        {
            for (int p = 0; p < cell; p++)
            {
                result[i, p] = input1[i, p] + input2[i, p];
            }
        }
        return result;
    }

}

