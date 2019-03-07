using System.Collections.Generic;

namespace CASecondTask
{
    public class Node
    {
        public readonly int Number;
        public List<Node> AdjacentNodes { get; }

        public Node(int nodeNumber)
        {
            Number = nodeNumber;
            AdjacentNodes = new List<Node>();
        }

        public void MakeAdjacent(Node otherNode)
        {
            AdjacentNodes.Add(otherNode);
            otherNode.AdjacentNodes.Add(this);
        }

        public override string ToString() => Number.ToString();

        public override bool Equals(object obj) => 
            obj is Node node && node.Number == Number && node.AdjacentNodes == AdjacentNodes;

        public override int GetHashCode() => Number + AdjacentNodes.GetHashCode();
    }
}