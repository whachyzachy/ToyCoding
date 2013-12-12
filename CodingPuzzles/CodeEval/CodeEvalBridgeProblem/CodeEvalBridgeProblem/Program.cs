using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Set = System.Collections.Generic.HashSet<int>;
using System.Runtime.CompilerServices;


namespace CodeEvalBridgeProblem
{
    public struct Point
    {
        public Point(double x, double y) { X = x; Y = y; }

        
        public static Point operator +(Point p1, Point p2)
        {
            var temp = new Point();
            temp.X = p1.X + p2.X;
            temp.Y = p1.Y + p2.Y;
            return temp;

        }

        
        public static Point operator -(Point p1, Point p2)
        {
            var temp = new Point();
            temp.X = p1.X - p2.X;
            temp.Y = p1.Y - p2.Y;
            return temp;

        }

        
        public override string ToString()
        {
            return X + " " + Y;
        }

        public double X;
        public double Y;
    }

    public struct LineSeg
    {
        public LineSeg(Point i0, Point i1) { I0 = i0; I1 = i1; }

        public LineSeg(List<double> ls)
        {
            I0 = new Point(ls[0], ls[1]);
            I1 = new Point(ls[2], ls[3]);

        }

        public Point I0;
        public Point I1;

        
        public bool Intersects(LineSeg B)
        {
            var A = this;

            var ADelta = A.I1 - A.I0;
            var BDelta = B.I1 - B.I0;

            var denom = (-BDelta.X * ADelta.Y + ADelta.X * BDelta.Y);

            var s = (-ADelta.Y * (A.I0.X - B.I0.X) + ADelta.X * (A.I0.Y - B.I0.Y)) / denom;
            var t = (BDelta.X * (A.I0.Y - B.I0.Y) - ADelta.Y * (A.I0.X - B.I0.X)) / denom;

            if (s >= 0.0 && s <= 1.0 && t >= 0.0 && t <= 1.0)
            {

                var intX = A.I0.X + (t * ADelta.X);
                var intY = A.I0.Y + (t * ADelta.Y);

                var intPoint = new Point(intX, intY);

                //Console.WriteLine("Intersection: " + intPoint.ToString());

                return true;
            }

            return false;
        }

    }

    struct DataPoint
    {
        public DataPoint(int nInd, LineSeg ls)
        {
            _Ls = ls;
            _Ind = nInd;
        }

        public override string ToString()
        {
            return _Ls.I0.ToString() + " " + _Ls.I1.ToString();
        }
        public LineSeg _Ls;
        public int _Ind;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];

            Dictionary<int, DataPoint> dicData = new Dictionary<int, DataPoint>();

            Dictionary<int, Set> dicIndToNeighs = new Dictionary<int, Set>(); //Adjacency

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {

                    string strline = sr.ReadLine();
                    //Console.WriteLine(sr.ReadLine());
                    var firstsplit = strline.Split(':').ToList();
                    int nVal = Convert.ToInt32(firstsplit[0]);

                    var secondsplit = firstsplit[1].Split(',');

                    List<double> dVals = new List<double>();
                    foreach (var val in secondsplit)
                    {
                        var trimmed = val.Trim(new Char[] { ' ', '[', ']', '(', ')' });
                        dVals.Add(Convert.ToDouble(trimmed));
                        //Console.WriteLine("trimmed: " + trimmed);
                    }

                    LineSeg ls = new LineSeg(dVals);
                    dicData[nVal] = (new DataPoint(nVal, ls));
                }
            }

            foreach (var i in dicData) //Could make this symmetric, but will be lazy for now
            {
                Set neighsForInd = new Set();
                Console.WriteLine(i.Value.ToString());
                foreach (var j in dicData)
                {
                    if (i.Key == j.Key)
                        continue;

                    if (i.Value._Ls.Intersects(j.Value._Ls))
                    {
                        //Console.WriteLine(i + " " + j + " intersect");
                    }
                    else
                    {
                        neighsForInd.Add(j.Key);
                    }
                }

                dicIndToNeighs[i.Key] = neighsForInd;
            }

            BronKerbosch bk = new BronKerbosch();
            bk._adjacency = dicIndToNeighs;

            Set R = new Set();
            Set X = new Set();
            Set P = new Set();

            foreach (var v in dicData)
            {
                X.Add(v.Key);
            }

            bk.BK(R, X, P);

            var vMaxSorted = from data in bk._maximumClique orderby data ascending select data;

            foreach (var v in vMaxSorted)
            {
                Console.WriteLine(v);
            }

            //Console.WriteLine("Calls: " + bk._Calls);
        }


    }


    
    public class BronKerbosch //a naive implementation to find maximum clique
    {

        public Dictionary<int, Set> _adjacency;

        public Set _maximumClique = new Set();


        
        Set union(Set A, int v)
        {
            var output = new Set(from n in A select n);
            output.Add(v);
            return output;
        }

        
        Set union(Set A, Set B)
        {

            return new Set(A.Union(B));
        }

        
        Set intersect(Set A, Set B)
        {
            return new Set(A.Intersect(B));
        }

        public int _Calls = 0;

        
        public void BK(Set R, Set P, Set X)
        {
            _Calls++;

            if (P.Count == 0 && X.Count == 0)
            {
                if (R.Count > _maximumClique.Count)
                    _maximumClique = R;
            }

            //Set test = union(R, P);
            //if (test.Count < _maximumClique.Count)
            //    return;

            Set Pmod = union(P, X);

            int u = -1;
            foreach (var v in Pmod)
            {
                u = v;
                break;
            }

            if (u == -1)
                return;

            Set PDiff = new Set(P.Except(_adjacency[u]));



            if (PDiff == null || !PDiff.Any())
                return;

            foreach (var v in PDiff)
            {
                Set RU = union(R, v);

                Set Nv = _adjacency[v];

                Set PIntNV = intersect(P, Nv);

                Set XIntNV = intersect(X, Nv);

                BK(RU, PIntNV, XIntNV);

                P.Remove(v);
                X = union(X, v);
            }
        }
    }
}