using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpStackByHand
{
    public class CNode<T>
    {

        public CNode(T data)
        {
            Data = data;
            Child = null;
        }

        private T m_data;
        public T Data
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;
            }
        }
        public CNode<T> Child { get; set; }
    }
    public class CStack<T>
    {
        public CStack()
        {
            Top = null;
        }
        public bool Empty
        {
            get
            {
                return Top == null;
            }
        }
        public CNode<T> Top { get; set; }
        public void Pop()
        {
            if (Top != null)
            {
                Top = Top.Child;
            }
            else
                Console.WriteLine("Top is null");
        }

        public T Peek
        {
            get
            {
                return Top.Data;
            }
        }

        public void Push(T data)
        {
            CNode<T> newdata = new CNode<T>(data);
            newdata.Child = Top;
            Top = newdata;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = File.OpenText(args[0]))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    // do something with line

                    var vals = line.Split(' ');
                    CStack<string> StackStrings = new CStack<string>();
                    foreach (var str in vals)
                    {
                        StackStrings.Push(str);
                    }

                    int counter = 0;
                    StringBuilder sb = new StringBuilder();
                    while (!StackStrings.Empty)
                    {
                        string top = StackStrings.Peek;
                        StackStrings.Pop();

                        if (counter % 2 == 0)
                        {
                            sb.Append(top);
                            sb.Append(" ");
                        }
                        counter++;
                    }
                    Console.WriteLine(sb.ToString());
                }
        }
    }
}
