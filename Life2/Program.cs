using Life;
using System;

namespace Life2
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
