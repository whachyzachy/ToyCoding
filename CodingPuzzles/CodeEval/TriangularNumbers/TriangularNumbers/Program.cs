using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TriangularNumbers
{
    class Program
    {
        static void Main(string[] args)
        {

            for (long i = 0; i > -1; i++)
            {
                long lVal = (i * (i + 1)) / 2;

                var str = lVal.ToString("X");

                if (str.Length == 9 && str.All(Char.IsLetter))
                {
                    Console.Write("lVal: " + lVal + " str: " + str);
                    break;
                }

            }

        }
    }
}
