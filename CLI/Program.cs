using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REG_MARK_LIB;
using SB_UTILS;

namespace CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var n = new RegNumberSeries("xxx");
            n += 1;

            Console.Write(n.ToString());
            Console.ReadKey();
        }
    }
}
