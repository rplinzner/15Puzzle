using Data;
using Priority_Queue;
using System;
using System.Collections.Generic;

namespace Solvers
{
    public class AStarSolver : SolverBase
    {
        #region prop & fields
        private HeuristicMethod _heuristicMethod;
        public SimplePriorityQueue<Node, int> Nodes = new SimplePriorityQueue<Node, int>();
        #endregion

        #region ctor
        public AStarSolver(NodeDTO startNodeDto, WritePathDTO writePaths, AStarHeuresticEnum heuristic) : base(startNodeDto, writePaths)
        {
            _heuristicMethod = _heuristicMethodsDictionary[heuristic];
            Nodes.Enqueue(InitialNode, 0);

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
            byte[] board = node.Board;
            int distance = node.DepthLevel;
            byte dimX = node.DimX;
            byte dimY = node.DimY;
            for (int i = 0; i < dimY; i++)
            {
                for (int j = 0; j < dimX; j++)
                {
                    if (IsLastTile(i, j, dimX, dimY))
                    {
                        if (board[j + i * dimX] != 0) //if 0 value isn't on the last tile position
                        {
                            distance++;
                        }
                    }
                    else
                    {
                        if (!IsGoodPosition(board, i, j, dimX))
                        {
                            distance++;
                        }
                    }
                }
            }

            return distance;
        }

        private static int ManhattanMethod(Node node)
        {
            byte[] board = node.Board;
            int distance = node.DepthLevel;
            byte dimX = node.DimX;
            byte dimY = node.DimY;
            for (int i = 0; i < dimY; i++)
            {
                for (int j = 0; j < dimX; j++)
                {
                    int value = board[j + i * dimX];
                    if (value != 0)
                    {
                        int x = (value - 1) % dimX;
                        int y = (value - 1 - x) / dimX;
                        x = Math.Abs(x - j);
                        y = Math.Abs(y - i);
                        distance += (x + y);
                    }
                }
            }

            return distance;
        }

        private static bool IsLastTile(int i, int j, byte dimX, byte dimY)
        {
            return (i == dimY - 1) && (j == dimX - 1);
        }

        private static bool IsGoodPosition(byte[] board, int i, int j, byte dimX)
        {
            return board[j + i * dimX] == (j + i * dimX + 1);
        }
        #endregion


    }
}