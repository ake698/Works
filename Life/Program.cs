using Display;
using System;
using System.Diagnostics;
using System.IO;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            LifeGenerate life = new LifeGenerate(args);
            life.Start();
        }


    }
}
