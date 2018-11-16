using System;
using Data;
using System.Collections.Generic;
using Priority_Queue;

namespace Solvers
{
    public class AStarSolver : SolverBase
    {
        #region prop & fields
        private HeuristicMethod _heuristicMethod;
        public SimplePriorityQueue<Node, int> Nodes = new SimplePriorityQueue<Node, int>();
        #endregion

        #region ctor
        public AStarSolver(NodeDTO startNodeDto, WritePathDTO writePaths, AStarHeuresticEnum heurestic) : base(startNodeDto, writePaths)
        {
            _heuristicMethod = _heuristicMethodsDictionary[heurestic];

        }
        #endregion

        #region overrided methods

        protected override bool IsMovePossible()
        {
            return true;
        }

        protected override List<MoveEnum> GetPossibleMoves()
        {
            return NodeInProcessing.FindPossibleMoves();
        }

        protected override void AddNode(Node newNode)
        {
            Nodes.Enqueue(newNode, _heuristicMethod(newNode));
        }

        protected override Node GetNode()
        {
            return Nodes.Dequeue();
        }

        protected override int GetNodesInContainer()
        {
            return Nodes.Count;
        }

        #endregion

        #region Private

        private delegate int HeuristicMethod(Node node);

        private readonly Dictionary<AStarHeuresticEnum, HeuristicMethod> _heuristicMethodsDictionary =
            new Dictionary<AStarHeuresticEnum, HeuristicMethod>()
            {
                {AStarHeuresticEnum.Hamming, HammingMethod },
                {AStarHeuresticEnum.Manhattan, ManhattanMethod }
            };

        private static int HammingMethod(Node node)
        {
            throw new NotImplementedException();
        }

        private static int ManhattanMethod(Node node)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}