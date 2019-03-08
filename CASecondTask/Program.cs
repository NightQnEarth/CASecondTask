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
            cycle == null ? "A" : string.Join(Environment.NewLine, "N", string.Join(Environment.NewLine, cycle));

        public static Graph GetInputData(Func<string> lineReader)
        {   
            var nodesCount = int.Parse(lineReader().Trim());
            var graph = new Graph(nodesCount);

            for (int nodeNumber = 0; nodeNumber < nodesCount; nodeNumber++)
                foreach (var adjacentNodeNumber in ReadAdjacentNodeCollection(nodeNumber + 1))
                    if (!graph[nodeNumber].AdjacentNodes.Contains(graph[adjacentNodeNumber - 1]))
                        graph[nodeNumber].MakeAdjacent(graph[adjacentNodeNumber - 1]);

            return graph;

            IEnumerable<int> ReadAdjacentNodeCollection(int nodeNumber) =>
                Regex.Split(lineReader(), @"\W+")
                     .Where(str => str.Length > 0 && str != "0" && str != nodeNumber.ToString())
                     .Distinct()
                     .Select(int.Parse);
        }
    }
}