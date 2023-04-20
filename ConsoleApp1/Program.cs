using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository r1=new Repository();
            r1.Show();
           Console.ReadKey();
            List<string> list = new List<string>();
            list = r1.Zagrz(list);
        }
    }
}
