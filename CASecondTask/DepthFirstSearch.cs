using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class DepthFirstSearch
    {
        public IEnumerable<Node> GetCycle(Graph graph)
        {
            var stack = new Stack<Node>();
            stack.Push(graph[0]);
            var visited = new HashSet<Node>();
            var parent = new Dictionary<Node, Node>{ {graph[0], null} };

            while (stack.Count != 0)
            {
                var currentNode = stack.Pop();
                visited.Add(currentNode);
                foreach (var adjacentNode in currentNode.AdjacentNodes.Where(node => !node.Equals(parent[currentNode])))
                {
                    if (stack.Contains(adjacentNode))
                    {
                        parent[adjacentNode] = currentNode;
                        return RouteRestore(adjacentNode).OrderBy(node => node.Number);
                    }
                    stack.Push(adjacentNode);
                    parent.Add(adjacentNode, currentNode);
                }
            }
            
            return null;
            
            IEnumerable<Node> RouteRestore(Node cycleStartNode)
            {
                yield return cycleStartNode;
                while (parent[cycleStartNode] != null)
                {
                    yield return parent[cycleStartNode];
                    cycleStartNode = parent[cycleStartNode];
                }
            }
        }
    }
}