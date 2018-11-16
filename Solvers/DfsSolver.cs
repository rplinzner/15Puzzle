using System.Collections.Generic;
using Data;

namespace Solvers
{
    public class DfsSolver :SolverBase
    {
        #region prop & field
        public Stack<Node> Nodes = new Stack<Node>();
        public List<MoveEnum> MoveOrder { get; set; }
        private const int MaxDepth = 20;
        #endregion

        #region ctor
        public DfsSolver(string moveOrder, NodeDTO startNodeDto, WritePathDTO writePaths) : 
            base(startNodeDto, writePaths)
        {
            MoveOrder = SolverHelp.ConvertStringMovesToEnums(moveOrder);
            MoveOrder.Reverse();
            AddNode(InitialNode);
        }
        #endregion

        #region Overrided methods

        protected override bool IsMovePossible()
        {
            return NodeInProcessing.DepthLevel < MaxDepth;
        }

        protected override List<MoveEnum> GetPossibleMoves()
        {
            return NodeInProcessing.FindPossibleMovesWithOrder(MoveOrder);
        }

        protected override void AddNode(Node newNode)
        {
            Nodes.Push(newNode);
        }

        protected override Node GetNode()
        {
            return Nodes.Pop();
        }

        protected override int GetNodesInContainer()
        {
            return Nodes.Count;
        }

        #endregion
        
    }
}