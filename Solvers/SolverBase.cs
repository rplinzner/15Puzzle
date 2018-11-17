using System;
using Data;
using System.Collections.Generic;
using System.Diagnostics;

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
        public int Visited { get; set; }
        public Dictionary<string, int> Explored { get; set; }
        public int MaxDepth { get; set; }
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
            Explored = new Dictionary<string, int>();
            Visited = 1;
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

        public void Solve()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int visited = 0;
            Console.WriteLine("Zaczynam obliczenia");
            while (GetNodesInContainer() > 0)
            {

                NodeInProcessing = GetNode();

                if (!Explored.ContainsKey(NodeInProcessing.ToString()))
                {
                    Explored.Add(NodeInProcessing.ToString(), NodeInProcessing.DepthLevel);
                }
                else if (NodeInProcessing.DepthLevel < Explored[NodeInProcessing.ToString()])
                {
                    Explored[NodeInProcessing.ToString()] = NodeInProcessing.DepthLevel;
                }

                MaxDepth = NodeInProcessing.DepthLevel > MaxDepth ? NodeInProcessing.DepthLevel : MaxDepth;
                visited++;

                //Check if state is solved, if its solved Write info to the file
                if (NodeInProcessing.IsSolved())
                {
                    stopwatch.Stop();

                    SolutionInfo.SolutionLength = NodeInProcessing.DepthLevel;
                    SolutionInfo.StatesVisited = NodeInProcessing.GetSolutionPath();
                    SolutionInfo.StatesProcessed = Visited;
                    SolutionInfo.DeepestRecursionReached = Explored.Count;
                    SolutionInfo.ProcessingTime = stopwatch.Elapsed.TotalMilliseconds;

                    DataWriter.WriteSolutionToFile(SolutionFilePath);

                    DataWriter.WriteInfoToFile(InfoFilePath);

                    Console.WriteLine("Done!");
                    return;
                }

                AddChildren();
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
