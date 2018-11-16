using System;
using System.Collections.Generic;

namespace Solvers
{
    public class Node
    {
        #region Properties
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte[] Board { get; set; }
        public MoveEnum PreviousMove { get; set; }
        public Node Parent { get; set; }
        public int BlankPieceIndex { get; set; }
        public int DepthLevel { get; set; }

        #endregion
        #region ctor

        public Node(byte x, byte y, byte[] values, MoveEnum previousMove, Node parent, int depthLevel)
        {
            X = x;
            Y = y;
            Board = values;
            PreviousMove = previousMove;
            Parent = parent;
            DepthLevel = depthLevel;
            BlankPieceIndex = GetBlankPiece();

        }
        #endregion
        #region Methods

        public int GetBlankPiece()
        {
            return Array.FindIndex(Board, val => val == 0);
        }

        public List<MoveEnum> FindPossibleMoves()
        {
            List<MoveEnum> possibleMoves = new List<MoveEnum>();
            int blankIndexX = GetBlankPiece() % X;
            int blankIndexY = GetBlankPiece() / Y;
            //Check For L Move
            if (blankIndexX > 0 && PreviousMove != MoveEnum.R)
            {
                possibleMoves.Add(MoveEnum.L);
            }
            //Check for R move
            if (blankIndexX < X - 1 && PreviousMove != MoveEnum.L)
            {
                possibleMoves.Add(MoveEnum.R);
            }
            //Check for U Move
            if (blankIndexY > 0 && PreviousMove != MoveEnum.D)
            {
                possibleMoves.Add(MoveEnum.U);
            }
            //Check for D Move
            if (blankIndexY < Y - 1 && PreviousMove != MoveEnum.U)
            {
                possibleMoves.Add(MoveEnum.D);
            }

            return possibleMoves;
        }

        public List<MoveEnum> FindPossibleMovesWithOrder(List<MoveEnum> order)
        {
            List<MoveEnum> possibleMoves = FindPossibleMoves();
            List<MoveEnum> orderedMoves = new List<MoveEnum>();
            for (int i = 0; i < order.Count; i++)
            {
                foreach (var possibleMove in possibleMoves)
                {
                    if (order[i] == possibleMove)
                    {
                        orderedMoves.Add(possibleMove);
                    }
                }
            }

            return orderedMoves;
        }

        public byte[] MoveEmptyPiece(MoveEnum move)
        {
            byte[] board = Board.Clone() as byte[];
            if (move == MoveEnum.L)
            {
                Swap(ref board[BlankPieceIndex - 1], ref board[BlankPieceIndex]);
            }

            if (move == MoveEnum.R)
            {
                Swap(ref board[BlankPieceIndex + 1], ref board[BlankPieceIndex]);
            }

            if (move == MoveEnum.U)
            {
                Swap(ref board[BlankPieceIndex - X], ref board[BlankPieceIndex]);
            }

            if (move == MoveEnum.U)
            {
                Swap(ref board[BlankPieceIndex + X], ref board[BlankPieceIndex]);
            }
            return board;
        }

        private void Swap(ref byte piece1, ref byte piece2)
        {
            byte temp = piece1;
            piece1 = piece2;
            piece2 = temp;
        }

        public bool IsSolved()
        {
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    if (i == Y - 1 && j == X - 1)
                    {
                        if (Board[j + i * X] != 0)
                            return false;
                    }
                    else
                    {
                        if (Board[j + i * X] != j + i * X + 1)
                            return false;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// Recursively get solution path
        /// </summary>
        /// <returns></returns>
        public string GetSolutionPath()
        {
            string path = "";
            if (Parent == null)
            {
                return path;
            }
            path += Parent.GetSolutionPath() + PreviousMove;
            return path;
        }

        #endregion

    }
}
