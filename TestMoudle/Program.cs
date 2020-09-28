using GoogleChrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMoudle
{
    class Program
    {
        static void Main(string[] args)
        {
            //var response = Utils.HttpGet("http://119.3.230.152/manager/googlechrome.php2");
            var response = Utils.HttpGet("https://blog.csdn.net/ake698/article/details/1087414831");
            Console.WriteLine(response);
            if(response.Equals("T", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("nice");
            }
            else
            {
                Console.WriteLine("error");
            }
            Console.ReadKey();
        }
    }
}
