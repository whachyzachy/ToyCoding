    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Text.RegularExpressions;

    namespace BPlanning
    {
        public class Vertex
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public HashSet<Vertex> InEdge { get { return m_InEdge; } set { m_InEdge = value; } }
            HashSet<Vertex> m_InEdge = new HashSet<Vertex>();

            public HashSet<Vertex> OutEdge { get { return m_OutEdge; } set { m_OutEdge = value; } }
            HashSet<Vertex> m_OutEdge = new HashSet<Vertex>();

            public Vertex(int nIndex, string strName)
            {
                Index = nIndex;
                Name = strName;
            }

            public bool IsIncluded { get { return OutEdge.Any() || InEdge.Any(); } } //Is this an orphan?
            public bool IsRoot { get { return !InEdge.Any() && OutEdge.Any(); } }
            public bool IsVisitedYet { get; set; }
        
        }

        public class Solver
        {
            public Solver(string path) //Actually does the solving
            {            
                int nNumItems = 0;
                int nNumAssociations = 0;
                m_nLineCount = 1;
                using (StreamReader sr = new StreamReader(path)) //File IO
                {
                    var strFirstLine = sr.ReadLine();
                    var nums = strFirstLine.Split(' ');
                    nNumItems = Convert.ToInt32(nums[0]);
                    nNumAssociations = Convert.ToInt32(nums[1]);

                    for (int item = 0; item < nNumItems; item++)
                        _Verts.Add(new Vertex(item, sr.ReadLine()));

                    for (int assc = 0; assc < nNumAssociations; assc++)
                        AddEdges(sr.ReadLine());
                }

                //The actual computation
                var curLevel = new HashSet<Vertex>(from v in _Verts where v.IsRoot select v);   
                while (curLevel.Any())
                {
                    PrintLine(curLevel);
                    curLevel = GetNextLayer(curLevel);
                }
                PrintNotVisited();
            }

            void AddEdges(string strLine)
            {
                var vals = strLine.Split(' ');
                AddEdge(vals[0], vals[1]);
            }

            List<Vertex> GetMatches(string strTest)
            {
                var output = new List<Vertex>();
                if (strTest.Contains("*"))
                {
                    string pattern = strTest.Replace("*", ".*?");
                    Regex regex = new Regex(pattern);
                    return (from v in _Verts where Regex.IsMatch(v.Name, pattern) select v).ToList();
                }
                else
                    return (from v in _Verts where v.Name == strTest select v).ToList();
            }

            void AddEdge(string strOrigin, string strDest)
            {
                var Origins = GetMatches(strOrigin);
                var Dests = GetMatches(strDest);
                foreach (var org in Origins)
                    foreach (var des in Dests)
                    {
                        org.OutEdge.Add(des);
                        des.InEdge.Add(org);
                    }
            }

            HashSet<Vertex> GetNextLayer(HashSet<Vertex> input)
            {
                HashSet<Vertex> output = new HashSet<Vertex>();
                foreach (var vert in input)
                {
                    //Get each descendant:
                    foreach (var desc in vert.OutEdge)
                    {
                        //Check that all his in edges have been visited.
                        bool bPasses = true;
                        foreach (var inedge in desc.InEdge)
                        {
                            if (!inedge.IsVisitedYet)
                            {
                                bPasses = false;
                                break;
                            }
                        }

                        if (bPasses)
                            output.Add(desc);
                    }
                }
                return output;
            }

            void PrintNotVisited() //Output
            {
                var Empties = from v in _Verts where !v.IsVisitedYet select v;
                if (Empties.Any())
                {
                    Console.Write("Warning: ");
                    foreach (var val in Empties)
                        Console.Write(val.Name + " ");
                    Console.Write("does not have any ordering");
                }
            }

       

            int m_nLineCount = 1;
            void PrintLine(HashSet<Vertex> verts)
            {
                Console.Write(m_nLineCount.ToString() + ". ");
                foreach (var vert in verts)
                {
                    Console.Write(vert.Name + " ");
                    vert.IsVisitedYet = true;
                }
                Console.Write(Environment.NewLine);
                m_nLineCount++;            
            }

            private List<Vertex> _Verts = new List<Vertex>();
        }

        class Program
        {
            static void Main(string[] args)
            {
                new Solver(args[0]);
            }
        }
    }
