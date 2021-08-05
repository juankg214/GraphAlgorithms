using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithms
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string lines2 = System.IO.File.ReadAllText(@"C:\Users\juank\OneDrive\Documentos\file.txt");

            if (!FileManager.FileExists())
            {
                FileManager.SaveFile(lines2);
                Console.WriteLine("Saved");
            }

            string decoded = FileManager.GetData();

            string[] s = decoded.Split('\n');

            int V = FileManager.GetNumberOfNodes(s);
            List<Edge> Edges = FileManager.GetEdges(s, V);
            GraphChallenge graph = new GraphChallenge(Edges, V);
            while (true)
            {
                Console.WriteLine("0: Hamiltonian Path, 1: Hamiltonian Path (Parallel),  2: Shortest Distance between U and V, 3: Exit");
                string data = Console.ReadLine();
                if (data.Equals("0"))
                {
                    Stopwatch clock = Stopwatch.StartNew();
                    graph.PrimerPunto();
                    clock.Stop();
                    Console.WriteLine("Time (ms): " + clock.ElapsedMilliseconds);
                }
                else if (data.Equals("1"))
                {
                    Stopwatch clock = Stopwatch.StartNew();
                    graph.PrimerPuntoParalelo();
                    clock.Stop();
                    Console.WriteLine("Time (ms): " + clock.ElapsedMilliseconds);
                }
                else if (data.Equals("2"))
                {
                    Console.WriteLine("Nodo U: ");
                    string u = Console.ReadLine();
                    Console.WriteLine("Nodo V: ");
                    string v = Console.ReadLine();
                    graph.SegundoPunto(Int32.Parse(u) - 1, Int32.Parse(v) - 1);
                }
                else
                {
                    break;
                }
            }

            //Parallel.For(0, 100, i =>
            // {
            //     Console.WriteLine(i);
            // });

        }







    }
}
