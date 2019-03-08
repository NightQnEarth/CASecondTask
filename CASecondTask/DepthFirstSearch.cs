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

            while (visited.Count < graph.Nodes.Count())
            {
                var searchStartNode = graph.Nodes.Except(visited).First();
                var stack = new Stack<Node>();
                stack.Push(searchStartNode);
                visited.Add(searchStartNode);
                track[searchStartNode] = null;

                while (stack.Count != 0)
                {
                    var currentNode = stack.Pop();
                    visited.UnionWith(currentNode.AdjacentNodes);
                    foreach (var adjacentNode in currentNode.AdjacentNodes)
                    {
                        if (adjacentNode.Equals(track[currentNode])) continue; 
                            
                        if (stack.Contains(adjacentNode))
                        {
                            var oldParentOfCycleEndNode = adjacentNode.AdjacentNodes
                                                                      .First(node => node.Equals(track[adjacentNode]));
                            track[oldParentOfCycleEndNode] = adjacentNode;
                            track[adjacentNode] = currentNode;
                            
                            return RouteRestore(currentNode).OrderBy(node => node.Number);
                        }
                        
                        stack.Push(adjacentNode);
                        track.Add(adjacentNode, currentNode);
                    }
                }
            }

            return null;
            
            IEnumerable<Node> RouteRestore(Node parentOfCycleEndNode)
            {
                yield return parentOfCycleEndNode;
                var currentNode = parentOfCycleEndNode;
                do
                {
                    yield return track[currentNode];
                    currentNode = track[currentNode];
                } while (!track[currentNode].Equals(parentOfCycleEndNode));
            }
        }
    }
}