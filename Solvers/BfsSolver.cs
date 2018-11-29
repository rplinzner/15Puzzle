using System.Collections.Generic;
using Data;

namespace Solvers
{
    public class BfsSolver : SolverBase
    {
        #region prop & field
        public Queue<Node> Nodes = new Queue<Node>();
        public List<MoveEnum> MoveOrder { get; set; }
#endregion

        #region ctor

        public BfsSolver(string moveOrder, NodeDTO nodeData, WritePathDTO writeData) : base(nodeData,writeData)
        {
            MoveOrder = SolverHelp.ConvertStringMovesToEnums(moveOrder);
            Nodes.Enqueue(InitialNode);
        }
        #endregion

        //in bfs you can always make new node
        protected override bool IsMovePossible()
        {
            return true;
        }

        protected override List<MoveEnum> GetPossibleMoves()
        {
            return NodeInProcessing.FindPossibleMovesWithOrder(MoveOrder);
        }

        protected override void AddNode(Node newNode)
        {
            Nodes.Enqueue(newNode);
        }

        protected override Node GetNode()
        {
            return Nodes.Dequeue();
        }

        protected override int GetNodesInContainer()
        {
            return Nodes.Count;
        }
    }
}