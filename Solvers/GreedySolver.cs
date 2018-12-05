using System;
using System.Collections.Generic;
using Data;
using Priority_Queue;

namespace Solvers
{
    public class GreedySolver : SolverBase
    {
        private HeuristicMethod _heuristicMethod;
        public SimplePriorityQueue<Node, double> Nodes = new SimplePriorityQueue<Node, double>();
        public GreedySolver(NodeDTO startNodeDto, WritePathDTO writePaths, GreedyHeuresticEnum heuristic) : base(startNodeDto, writePaths)
        {
            _heuristicMethod = _heuristicMethodsDictionary[heuristic];
            Nodes.Enqueue(InitialNode, 0);
        }

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

        #region private

        private delegate double HeuristicMethod(Node node);

        private readonly Dictionary<GreedyHeuresticEnum, HeuristicMethod> _heuristicMethodsDictionary =
            new Dictionary<GreedyHeuresticEnum, HeuristicMethod>()
            {
                {GreedyHeuresticEnum.Diagonal, DiagonalMethod },
                {GreedyHeuresticEnum.Euclides, EuclidesMethod }
            };

        private static double DiagonalMethod(Node node)
        {
            byte[] board = node.Board;
            double distance = 0;
            byte dimX = node.DimX;
            byte dimY = node.DimY;
            for (int i = 0; i < dimY; i++)
            {
                for (int j = 0; j < dimX; j++)
                {
                    int value = board[j + i * dimX];
                    if (value != 0)
                    {
                        double x = (value - 1) % dimX;
                        double y = (value - 1 - x) / dimX;
                        x = Math.Abs(x - j);
                        y = Math.Abs(y - i);
                        distance += Math.Max(x, y);
                    }
                }
            }

            return distance;
        }

        private static double EuclidesMethod(Node node)
        {
            byte[] board = node.Board;
            double distance = 0;
            double temp;
            byte dimX = node.DimX;
            byte dimY = node.DimY;
            for (int i = 0; i < dimY; i++)
            {
                for (int j = 0; j < dimX; j++)
                {
                    int value = board[j + i * dimX];
                    if (value != 0)
                    {
                        double x = (value - 1) % dimX;
                        double y = (value - 1 - x) / dimX;
                        x = Math.Pow((x - j),2);
                        y = Math.Pow((y - i),2);
                        temp = x+y;
                        distance += Math.Sqrt(temp);
                    }
                }
            }

            return distance;
        }

        #endregion
    }
}