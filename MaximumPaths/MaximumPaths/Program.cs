using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace MaximumPaths
{
    class Program
    {
        public class Vertex
        {
            public Vertex(int row, int col, int total, Vertex back)
            {
                _row = row;
                _column = col;
                _totalSoFar = total;
                _back = back;

            }

            public int _row;
            public int _column;
            public int _totalSoFar;
            public Vertex _back;
        }

        static int Main(string[] args)
        {
            var vRawInput = new List<List<int>>();
            int MaxVal = 0;
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    // do something with line
                    var tokens = line.Split(' ');
                    var Level = new List<int>();
                    foreach (var v in tokens)
                    {
                        int nIntVal = Convert.ToInt32(v);
                        MaxVal = Math.Max(nIntVal, MaxVal);
                        Level.Add(nIntVal);
                    }
                    vRawInput.Add(Level);
                }

            return 0;
        }
    }
}
