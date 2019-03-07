using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CASecondTask
{
    public static class Program
    {
        public static void Main()
        {
            var finder = new DepthFirstSearch();
            var graph = GetInputData(Console.ReadLine);
            var resultCycle = finder.GetCycle(graph);
            
            Console.Write(ResultGenerate(resultCycle));
        }
        
        public static string ResultGenerate(IEnumerable<Node> cycle) => 
            cycle == null ?
                "A" :
                string.Join(Environment.NewLine,
                            "N",
                            string.Join(Environment.NewLine, 
                                        cycle.Select(node => node.Number.ToString())));


        public static Graph GetInputData(Func<string> lineReader)
        {   
            var nodesCount = int.Parse(lineReader().Trim());
            var graph = new Graph(nodesCount);

            foreach (var node in graph.Nodes)
                foreach (var nodeNumber in ReadAdjacentNodeCollection())
                {
                    if (node.AdjacentNodes.Contains(graph[nodeNumber - 1])) continue;
                    node.MakeAdjacent(graph[nodeNumber - 1]);
                }

            return graph;

            IEnumerable<int> ReadAdjacentNodeCollection() =>
                Regex.Split(lineReader(), @"\W+").Where(str => str.Length > 0 && str != "0")
                                                 .Select(int.Parse);
        }
    }
}