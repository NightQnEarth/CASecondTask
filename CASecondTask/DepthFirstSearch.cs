using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class DepthFirstSearch
    {
        public IEnumerable<Node> GetCycle(Graph graph)
        {
            var visited = new HashSet<Node>();
            var track = new Dictionary<Node, Node>();
            //var putToStack = new Dictionary<Node, Node>();

            while (visited.Count < graph.Nodes.Count())
            {
                var searchStartNode = graph.Nodes.Except(visited).First();
                var stack = new Stack<Node>();
                stack.Push(searchStartNode);
                track[searchStartNode] = null;
                //putToStack[searchStartNode] = null;

                while (stack.Count != 0)
                {
                    var currentNode = stack.Pop();
                    visited.UnionWith(currentNode.AdjacentNodes);
                    foreach (var adjacentNode in currentNode.AdjacentNodes.Where(node => !node.Equals(track[currentNode])))
                    {
                        if (stack.Contains(adjacentNode))
                        {
                            track[adjacentNode] = currentNode;
                            return RouteRestore(adjacentNode).OrderBy(node => node.Number);
                        }
                        stack.Push(adjacentNode);
                        track.Add(adjacentNode, currentNode); // TODO Тут перезаписываются значения ключа;
                    }
                }
            }

            return null;
            
            IEnumerable<Node> RouteRestore(Node childOfCycleStartNode)
            {
                yield return childOfCycleStartNode;
                var currentNode = childOfCycleStartNode;
                while (track[currentNode] != null && !currentNode.Equals(track[childOfCycleStartNode]))
                {
                    yield return track[currentNode];
                    currentNode = track[currentNode];
                }
            }
        }
    }
}