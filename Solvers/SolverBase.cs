using Data;
using System.Collections.Generic;

namespace Solvers
{
    public abstract class SolverBase
    {
        #region props

        public byte DimX { get; set; }
        public byte DimY { get; set; }
        public Node InitialNode { get; set; }
        public Node NodeInProcessing { get; set; }
        public string InfoFilePath { get; set; }
        public string SolutionFilePath { get; set; }

        #endregion

        #region ctor

        public SolverBase(NodeDTO startNodeDto, WritePathDTO writePaths)
        {
            DimX = startNodeDto.X;
            DimY = startNodeDto.Y;
            SolutionFilePath = writePaths.SolutionFilePath;
            InfoFilePath = writePaths.InfoFilePath;
            InitialNode = new Node(DimX, DimY, startNodeDto.Board, MoveEnum.N, null, 0);
            NodeInProcessing = InitialNode;
        }

        #endregion

        #region methods

        private void AddChildren()
        {
            if (!IsMovePossible())
            {
                return;
            }

            List<MoveEnum> possibleMoves = GetPossibleMoves();
            foreach (var possibleMove in possibleMoves)
            {
                Node newNode = new Node(DimX, DimY, NodeInProcessing.MoveEmptyTile(possibleMove),
                    possibleMove, NodeInProcessing, NodeInProcessing.DepthLevel);
                AddNode(newNode);
            }
        }

        protected abstract bool IsMovePossible();
        protected abstract List<MoveEnum> GetPossibleMoves();
        protected abstract void AddNode(Node newNode);
        protected abstract Node GetNode();
        protected abstract int GetNodesInContainer();

        #endregion
    }
}
