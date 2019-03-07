using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class Graph
    {
        private readonly Node[] _nodes;

        public Graph(int nodesCount) =>
            _nodes = Enumerable.Range(1, nodesCount)
                               .Select(nodeNumber => new Node(nodeNumber))
                               .ToArray();

        public IEnumerable<Node> Nodes => _nodes.Select(node => node);

        public Node this[int nodeNumber] => _nodes[nodeNumber];
    }
}