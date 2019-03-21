using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class DepthFirstSearch
    {
        public IEnumerable<Node> GetCycle(Graph graph)
        {
            var track = new Dictionary<Node, Node>();
            var stack = new Stack<Node>();

            while (track.Count < graph.Nodes.Count())
            {
                var searchStartNode = graph.Nodes.Except(track.Keys).First();
                stack.Clear();
                stack.Push(searchStartNode);
                track[searchStartNode] = null;

                while (stack.Count != 0)
                {
                    var currentNode = stack.Pop();
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

            IEnumerable<Node> RouteRestore(Node newParentOfCycleEndNode)
            {
                yield return newParentOfCycleEndNode;
                var currentNode = newParentOfCycleEndNode;
                do
                {
                    yield return track[currentNode];
                    currentNode = track[currentNode];
                } while (!track[currentNode].Equals(newParentOfCycleEndNode));
            }
        }
    }
}