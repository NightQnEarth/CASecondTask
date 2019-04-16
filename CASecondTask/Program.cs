using System;

namespace CASecondTask
{
    public static class Program
    {
        public static void Main()
        {
            var graph = DataParser.GetInputData(Console.ReadLine);
            var resultCycle = DepthFirstSearch.GetCycle(graph);

            Console.Write(DataParser.ResultGenerate(resultCycle));
        }
    }
}