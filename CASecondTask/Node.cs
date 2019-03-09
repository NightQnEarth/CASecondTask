using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class Node
    {
        public readonly int Number;
        private readonly List<Node> _adjacentNodes;

        public Node(int nodeNumber)
        {
            Number = nodeNumber;
            _adjacentNodes = new List<Node>();
        }

        public IEnumerable<Node> AdjacentNodes => _adjacentNodes.Select(node => node);

        public void MakeAdjacent(Node otherNode)
        {
            _adjacentNodes.Add(otherNode);
            otherNode._adjacentNodes.Add(this);
        }

        public override string ToString() => Number.ToString();

        public override bool Equals(object obj) => 
            obj is Node node && node.Number == Number && node._adjacentNodes == _adjacentNodes;

        public override int GetHashCode() => 
            unchecked((Number * 397) ^ (_adjacentNodes != null ? _adjacentNodes.GetHashCode() : 0));
    }
}