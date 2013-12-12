using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class Program
{
    [Serializable]
    class Point2D : IComparable
    {
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X
        {
            set {m_x = value;}
            get {return m_x;}
        }
        int m_x;
        public int Y
        {
            set {m_y = value;}
            get {return m_y;}
        }
        int m_y;

        bool IsValid()
        {
            string strX = Math.Abs(X).ToString();

            int count = 0;
            foreach (char c in strX)
            {
                count += Convert.ToInt32(c - '0');
            }

            string strY = Math.Abs(Y).ToString();
            foreach (char c in strY)
            {
                count += Convert.ToInt32(c - '0');
            }

            return count < 20;
        }

        public List<Point2D> GetValidNeighs()
        {
            List<Point2D> output = new List<Point2D>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x== 0 && y ==0)
                        continue;

                    if (Math.Abs(x) + Math.Abs(y) > 1)
                        continue;

                    Point2D val = new Point2D(x + X, y + Y);
                    if (val.IsValid())
                        output.Add(val);
                }
            }
            return output;
        }

        public override bool Equals(object obj)
        {
            var v = obj as Point2D;
            if (v == null)
                return false;
           
            return X == v.X && Y == v.Y;
        }

        public int CompareTo(object obj)
        {
            var v = obj as Point2D;

            if (X == v.X && Y == v.Y)
                return 0;

            if (X < v.X)
                return -1;

            if (Y < v.Y)
                return -1;

            return 1;
        }
        public override int GetHashCode()
        {
            return X * Y + Y;
        }
    }


    static void Main(string[] args)
    {
        //using (StreamReader reader = File.OpenText(args[0]))
        //while (!reader.EndOfStream)
        //{
        //    string line = reader.ReadLine();
        //    if (null == line)
        //        continue;
        //    // do something with line

        //    var strSplit = line.Split(',');

            

        //    if (strSplit.Length != 2)
        //        continue;

        //    if (strSplit[0].Contains(strSplit[1]))
        //        Console.WriteLine("1");
        //    else
        //        Console.WriteLine("0");

        //}

        HashSet<Point2D> AllVisitedPoints = new HashSet<Point2D>();
        Stack<Point2D> ToExamine = new Stack<Point2D>();

        ToExamine.Push(new Point2D(0, 0));

        while (!(ToExamine.Count == 0))
        {
            
            var Top = ToExamine.Peek();
            ToExamine.Pop();

            AllVisitedPoints.Add(Top);

            var Neighs = Top.GetValidNeighs();

            foreach (var neigh in Neighs)
            {
                if (AllVisitedPoints.Contains(neigh))
                    continue;
                ToExamine.Push(neigh);
            }
        }

        //Console.WriteLine("Total visited size: " + AllVisitedPoints.Count.ToString());
        Console.WriteLine(AllVisitedPoints.Count.ToString());

    }
}
