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
            var n1 = new RegNumber(new RegNumberSeries("aaa"), new RegNumberNumber(000), 1);
            var n2 = new RegNumber(new RegNumberSeries("bbb"), new RegNumberNumber(999), 1);


            foreach (var item in RegNumber.Range(n1, n2))
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
