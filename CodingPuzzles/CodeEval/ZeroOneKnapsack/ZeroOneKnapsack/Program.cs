using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZeroOneKnapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] Weights = { 2, 3, 4, 5, 9 };
            //int[] Benefits = { 3, 4, 5, 8, 10 };

            //int MaxWeight = 20;

            //Parse the input
            int[] Weights = {};
            int[] Benefits = {};
            int MaxWeight = 0;
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\jdgib_000\Dropbox\ToyCoding\ZeroOneKnapsack\ZeroOneKnapsack\bin\0.input.txt"))
                {

                    MaxWeight = Convert.ToInt32(sr.ReadLine());
                    Console.WriteLine(String.Format("MaxWeight {0}",MaxWeight));

                    int NumObjects = Convert.ToInt32(sr.ReadLine());
                    Weights = new int[NumObjects];
                    Benefits = new int[NumObjects];
                    for (int i = 0; i < Benefits.Length; i++)
                    {
                        string StrLine = sr.ReadLine();
                        var SubStr = StrLine.Split(' ');
                        Weights[i] = Convert.ToInt32(SubStr[1]);
                        Benefits[i] = Convert.ToInt32(SubStr[2]);

                        Console.WriteLine("Object {0} has Weight {1} and Benefit {2}",SubStr[0], Weights[i], Benefits[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            int[,] Tableau = new int[Weights.Length, MaxWeight + 1];

            for (int w = 0; w <= MaxWeight; w++)
            {               

                for (int i = 0; i < Weights.Length; i++)
                {
                    int currItemWeight = Weights[i];
                    int currItemBenefit = Benefits[i];

                    if (currItemWeight > w) //If the item weighs more than the current weight, it can't contribute
                    {
                        if (0 == i) //Initial case, need to zero out
                        {
                            Tableau[i,w] = 0;
                        }
                        else //otherwise we can look at the previous row
                        {
                            Tableau[i, w] = Tableau[i - 1, w];
                        }
                    }
                    else //The item weighs less than the current weight
                    {
                        if (0 == i) //if this is the first object, it's the best you can do for all weights
                        {
                            Tableau[i, w] = Benefits[i];
                        }
                        else //We are at at least the second object
                        {
                            //calculate the new benefit of the object
                            int nNewBenefit = Benefits[i] + Tableau[i - 1, w - Weights[i]];

                            if (nNewBenefit > Tableau[i - 1, w])
                                Tableau[i, w] = nNewBenefit;
                            else
                                Tableau[i, w] = Tableau[i - 1, w];
                        }
                    }
                }
            }

            Console.WriteLine(String.Format("Best value: {0}",Tableau[Weights.Length -1 , MaxWeight]));

        }
    }
}
