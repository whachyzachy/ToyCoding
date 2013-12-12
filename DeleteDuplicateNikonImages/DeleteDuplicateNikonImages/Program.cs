using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DeleteDuplicateNikonImages
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = from file in Directory.EnumerateFiles(@"C:\Users\jdgib_000\Pictures\2013-10-12", "*-00*.jpg", SearchOption.AllDirectories)
                        select file;


            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Length > 1000000)
                {
                    Console.WriteLine(file.ToString());
                    File.Delete(file);
                }
               
                //File.Delete(file);
            }

            Console.WriteLine("There are " + files.ToList().Count + " files ");


        }
    }
}
