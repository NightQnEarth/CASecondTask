using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class Graph
    {
        private readonly Node[] nodes;

        public Graph(int nodesCount) =>
            nodes = Enumerable.Range(1, nodesCount)
                              .Select(nodeNumber => new Node(nodeNumber))
                              .ToArray();

        public IEnumerable<Node> Nodes => nodes.Select(node => node);

        public Node this[int nodeNumber] => nodes[nodeNumber];
    }
}