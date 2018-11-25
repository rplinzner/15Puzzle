using System;
using System.Collections.Generic;

namespace Solvers
{
    public class Node
    {
        #region Properties
        public byte DimX { get; set; }
        public byte DimY { get; set; }
        public byte[] Board { get; set; }
        public MoveEnum PreviousMove { get; set; }
        public Node Parent { get; set; }
        public int BlankTileIndex { get; set; }
        public int DepthLevel { get; set; }

        #endregion
        #region ctor

        public Node(byte dimX, byte dimY, byte[] values, MoveEnum previousMove, Node parent, int depthLevel)
        {
            DimX = dimX;
            DimY = dimY;
            Board = values;
            PreviousMove = previousMove;
            Parent = parent;
            DepthLevel = depthLevel;
            BlankTileIndex = GetBlankTile();

        }
        #endregion
        #region Methods

        public int GetBlankTile()
        {
            return Array.FindIndex(Board, val => val == 0);
        }

        public List<MoveEnum> FindPossibleMoves()
        {
            List<MoveEnum> possibleMoves = new List<MoveEnum>();
            int blankIndexX = GetBlankTile() % DimX;
            int blankIndexY = GetBlankTile() / DimY;
            //Check For L Move
            if (blankIndexX > 0 && PreviousMove != MoveEnum.R)
            {
                possibleMoves.Add(MoveEnum.L);
            }
            //Check for R move
            if (blankIndexX < DimX - 1 && PreviousMove != MoveEnum.L)
            {
                possibleMoves.Add(MoveEnum.R);
            }
            //Check for U Move
            if (blankIndexY > 0 && PreviousMove != MoveEnum.D)
            {
                possibleMoves.Add(MoveEnum.U);
            }
            //Check for D Move
            if (blankIndexY < DimY - 1 && PreviousMove != MoveEnum.U)
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

        public byte[] MoveEmptyTile(MoveEnum move)
        {
            byte[] board = Board.Clone() as byte[];
            if (move == MoveEnum.L)
            {
                Swap(ref board[BlankTileIndex - 1], ref board[BlankTileIndex]);
            }

            if (move == MoveEnum.R)
            {
                Swap(ref board[BlankTileIndex + 1], ref board[BlankTileIndex]);
            }

            if (move == MoveEnum.U)
            {
                Swap(ref board[BlankTileIndex - DimX], ref board[BlankTileIndex]);
            }

            if (move == MoveEnum.U)
            {
                Swap(ref board[BlankTileIndex + DimX], ref board[BlankTileIndex]);
            }
            return board;
        }

        private void Swap(ref byte tile1, ref byte tile2)
        {
            byte temp = tile1;
            tile1 = tile2;
            tile2 = temp;
        }

        public bool IsSolved()
        {
            for (int i = 0; i < DimY; i++)
            {
                for (int j = 0; j < DimX; j++)
                {
                    if (i == DimY - 1 && j == DimX - 1)
                    {
                        if (Board[j + i * DimX] != 0)
                            return false;
                    }
                    else
                    {
                        if (Board[j + i * DimX] != j + i * DimX + 1)
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
