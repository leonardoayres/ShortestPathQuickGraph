using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

namespace ShortPathQuickGraph
{
    class Program
    {        
        private static AdjacencyGraph<string, Edge<string>> _graph;
        private static Dictionary<Edge<string>, double> _costs;

        // Set up the map
        private static void SetUpEdgesAndCosts()
        {
            _graph = new AdjacencyGraph<string, Edge<string>>();
            _costs = new Dictionary<Edge<string>, double>();

            AddEdgeWithCosts("A", "C", 1);
            AddEdgeWithCosts("A", "E", 30);
            AddEdgeWithCosts("A", "H", 10);
            AddEdgeWithCosts("C", "B", 1);
            AddEdgeWithCosts("H", "E", 30);
            AddEdgeWithCosts("E", "D", 3);
            AddEdgeWithCosts("D", "F", 4);
            AddEdgeWithCosts("F", "I", 45);
            AddEdgeWithCosts("F", "G", 40);
            AddEdgeWithCosts("I", "B", 65);
            AddEdgeWithCosts("G", "B", 64);
        }

        // Method to add the edges and costs in one move.
        private static void AddEdgeWithCosts(string source, string target, double cost)
        {
            var edge = new Edge<string>(source, target);
            _graph.AddVerticesAndEdge(edge);
            _costs.Add(edge, cost);
        }

        private static void PrintShortestPath(string @from, string to)
        {
            var edgeCost = AlgorithmExtensions.GetIndexer(_costs);
            
            // Defines the algorithm to be used.
            var tryGetPath = _graph.ShortestPathsDijkstra(edgeCost, @from);

            IEnumerable<Edge<string>> path;
            // Checks for a valid path for the given points.
            if (tryGetPath(to, out path))
            {
                PrintPath(@from, to, path);
            }
            else
            {
                Console.WriteLine("No path found from {0} to {1}.", from, to);
                Console.Read();
            }
        }

        private static void PrintPath(string from, string to, IEnumerable<Edge<string>> path)
        {
            Console.Write("Path found from {0} to {1}: {0}", @from, to);
            
            string resultPath = string.Empty;
            // Get each step of the path and put into a string.
            path.ToList().ForEach(e => { resultPath += $" > {e.Target}"; });

            Console.Write(resultPath);            
            Console.Read();
        }

        static void Main(string[] args)
        {
            // Set the map up.
            SetUpEdgesAndCosts();

            // Define a path for testing.
            PrintShortestPath("A", "I");
        }
    }
}
