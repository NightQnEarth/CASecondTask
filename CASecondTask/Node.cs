using System.Collections.Generic;
using System.Linq;

namespace CASecondTask
{
    public class Node
    {
        public readonly int Number;
        private readonly List<Node> adjacentNodes;

        public Node(int nodeNumber)
        {
            Number = nodeNumber;
            adjacentNodes = new List<Node>();
        }

        public IEnumerable<Node> AdjacentNodes => adjacentNodes.Select(node => node);

        public void MakeAdjacent(Node otherNode)
        {
            adjacentNodes.Add(otherNode);
            otherNode.adjacentNodes.Add(this);
        }

        public override bool Equals(object obj) =>
            obj is Node node && node.Number == Number && node.adjacentNodes == adjacentNodes;

        public override int GetHashCode() =>
            unchecked((Number * 397) ^ (adjacentNodes != null ? adjacentNodes.GetHashCode() : 0));

        public override string ToString() => Number.ToString();
    }
}