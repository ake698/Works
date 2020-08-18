using System;
using System.Collections.Generic;
using System.Linq;

class Sequence
{
    static void main(string[] args)
    {
        var result = Kmers(3, "AGATCGATGTG");
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
        string seq = "ACTGA";
        ReverseComplement(ref seq);
        Console.WriteLine(seq);
    }

    public static string[] Kmers(int k, string seq)
    {
        if (k > seq.Length) return null;

        int count = seq.Length - k + 1;
        string[] result = new string[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = seq.Substring(i, k);
        }
        return result;
    }

    public static void ReverseComplement(ref string seq)
    {
        Dictionary<char, char> format = new Dictionary<char, char>
        {
            {'A','T' },
            {'T','A' },
            {'C','G' },
            {'G','C' }
        };

        char[] result = new char[seq.Length];
        var seqArrs = seq.ToCharArray();
        for (int i = seqArrs.Length - 1; i >= 0 ; i--)
        {
            if (format.ContainsKey(seqArrs[i]))
            {
                result[seqArrs.Length - 1 - i] = format[seqArrs[i]];
            }
        }
        seq = new string(result);
    }

    public static bool Dyad(string seq, int num = 5)
    {
        var seqArrs = seq.Split("...");
        if(seqArrs.Length == 2 && !seqArrs.Contains(".") && seqArrs[1].Length <= 5 && seqArrs[0].Length <= 5)
        {
            string tmp = seqArrs[0];
            ReverseComplement(ref tmp);
            if (tmp.Equals(seqArrs[1])) return true;
        }
        return false;
    }
}
