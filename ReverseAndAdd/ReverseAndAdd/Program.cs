using System;
using System.IO;
using System.Collections.Generic;

class Program
{

    public static bool IsPalindrome(string strInput)
    {
        for (int i = 0; i < strInput.Length / 2; i++)
        {
            if (strInput[i] != strInput[strInput.Length - 1 - i])
                return false;
        }

        return true;
    }

    public static void ComputeNextPalindrome(string strInput, int nIteration = 1)
    {
        Int64 nFirstVal = Convert.ToInt64(strInput);
        Int64 nSecondVal = Convert.ToInt64(ReverseString(strInput));

        var NewVal = nFirstVal + nSecondVal;
        string newVal = NewVal.ToString();
        if (IsPalindrome(newVal))
        {
            Console.WriteLine(nIteration.ToString() + " " + newVal);
            return;
        }

        ComputeNextPalindrome(newVal, nIteration + 1);
    }

    public static string ReverseString(string strin)
    {
        char[] array = strin.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    static void Main(string[] args)
    {
        

        using (StreamReader reader = File.OpenText(args[0]))
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (null == line)
                continue;
            // do something with line
            ComputeNextPalindrome(line);
            
        }

        //Console.WriteLine("Done");
    }
}