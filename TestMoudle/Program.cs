using System;
using System.Diagnostics;

namespace TestMoudle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            KillProcess("CHROME");
            Console.ReadKey();
        }

        public static void KillProcess(string strProcessesByName)//关闭线程
        {
            var a =Process.GetProcesses();
            foreach (Process p in Process.GetProcesses())//GetProcessesByName(strProcessesByName))
            {
                //Console.WriteLine(p.ProcessName);
                if (p.ProcessName.ToUpper().Contains(strProcessesByName))
                {
                    Console.WriteLine(p.ProcessName);
                    try
                    {
                        p.Kill();
                        p.WaitForExit(); // possibly with a timeout
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToString());   // process was terminating or can't be terminated - deal with it
                    }
                }
            }
        }
    }
}
