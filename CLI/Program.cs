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
            var n2 = new RegNumber(new RegNumberSeries("mmm"), new RegNumberNumber(999), 1);

            var rnd = new Random();


            foreach (var item in RegNumber.Range(n1, n2))
            {
                if (rnd.Next(1, 1000) == 4)
                {
                    Console.WriteLine(item.Series.ToString() + " " + item.Number.ToString());
                }
                
            }

            Console.ReadKey();
        }
    }
}
