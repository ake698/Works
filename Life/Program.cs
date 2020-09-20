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
