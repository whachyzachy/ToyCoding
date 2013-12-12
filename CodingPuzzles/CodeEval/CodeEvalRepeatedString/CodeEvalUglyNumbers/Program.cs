using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Collections;

namespace CodeEvalUglyNumbers
{
    class Program
    {
        static bool IsUgly(Int64 val)
        {
            return (val % 2 == 0 ||
                val % 3 == 0 ||
                val % 5 == 0 ||
                val % 7 == 0);
                
        }


        public static void PrintValues(IEnumerable myList, int myWidth)
        {
            int i = myWidth;
            foreach (Object obj in myList)
            {
                if (i <= 0)
                {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,8}", obj);
            }
            Console.WriteLine();
        }

        public static int GetNumUglyNumbers(List<string> linput)
        {
            int nMaxVal = Convert.ToInt32(Math.Pow(2, linput.Count - 1));

            int totalVas = 0;
            for (int i = 0; i < nMaxVal; i++)
            {
                int[] intArray = new int[1];
                intArray[0] = i;

                BitArray ba = new BitArray(intArray);

                Int64 nSum = Convert.ToInt64(linput[0]);

                for (int v = 1; v < linput.Count; v++)
                {
                   if (ba[v-1])
                       nSum += Convert.ToInt64(linput[v]);
                   else
                       nSum -= Convert.ToInt64(linput[v]);
                }

                if (IsUgly(nSum))
                    totalVas++;

            }

            return totalVas;
        }

        static List<string> GetSubsetFromBitArray(string strInput, BitArray ba)
        {
            var output = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append(strInput[0]);

            for (int i = 1; i < strInput.Length; i++)
            {
                if (ba[i - 1])
                {
                    output.Add(sb.ToString());
                    sb = new StringBuilder();
                }
                sb.Append(strInput[i]);
            }

            output.Add(sb.ToString());

           // Console.WriteLine("Input: " + strInput);
           // Console.WriteLine("bit array: " + ba.ToString());

            //PrintValues(ba, 8);
           // foreach (var v in output)
            //    Console.WriteLine("val: " + v);

            return output;
        }



        static void Main(string[] args)
        {
            List<string> lnInputs = new List<string>();
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    // do something with line
                    lnInputs.Add(line);
                }

            foreach (var inpt in lnInputs)
            {
                int nMaxVal = Convert.ToInt32(Math.Pow(2, inpt.Length - 1));

                int totalUgly = 0;
                for (int i = 0; i < nMaxVal; i++)
                {
                    int[] intArray = new int[1];
                    intArray[0] = i;
                    BitArray ba = new BitArray(intArray);

                   totalUgly +=  GetNumUglyNumbers( GetSubsetFromBitArray(inpt, ba) );
                }
                Console.WriteLine(totalUgly);
            }
        }
    }
}
