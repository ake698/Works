using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Sequence
{
    static void Main(string[] args)
    {
        //var result = Kmers(3, "AGATCGATGTG", "AGATCGATGTG");
        //foreach (var item in result)
        //{
        //    Console.WriteLine(item);
        //}
        //string seq = "ACTGA";
        //ReverseComplement(ref seq);
        //Console.WriteLine(seq);

        Console.WriteLine(Dyad("TGGGTCAAAATTTGGCACGCTACCCTTTATACA", 5));
        Console.WriteLine(Dyad("TAGGAATCTCTGGCGGCCCCTGAATAGTTACTTCCAGATTCCTA", 10));
        Console.WriteLine(Dyad("CCTGCGCAGG", 5));
        Console.WriteLine(Dyad("AAGCTGGACTTGTGGATGTTGACGCCAGCTT", 5));
        Console.WriteLine(Dyad("TTTATAACTTCGTGCCGGCACTAAGACTAGTGGCACGAAGTTATAAA", 15));
    }

    public static string[] Kmers(int k, params string[] seqs)
    {
        string seq = "";
        for (int i = 0; i < seqs.Length; i++)
        {
            seq += seqs[i];
        }
        Console.WriteLine(seq);
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
        if (seq.Length < num) return false;

        string up = seq.Substring(0, num);
        string down = seq.Substring(seq.Length - num, num);
        string tmp = up;
        ReverseComplement(ref tmp);
        if (tmp.Equals(down))
        {
            return true;
        }
        
        return false;
    }
}
