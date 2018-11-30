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
        /*public string InfoFilePath { get; set; }
        public string SolutionFilePath { get; set; }*/
        public WritePathDTO WritePaths { get; set; }
        public int VisitedStates { get; set; }
        public Dictionary<string, int> ExploredStates { get; set; }
        public int DeepestRecursion { get; set; }

        #endregion

        #region ctor

        public SolverBase(NodeDTO startNodeDto, WritePathDTO writePaths)
        {
            DimX = startNodeDto.X;
            DimY = startNodeDto.Y;
            /*SolutionFilePath = writePaths.SolutionFilePath;
            InfoFilePath = writePaths.InfoFilePath;*/
            WritePaths = writePaths;
            InitialNode = new Node(DimX, DimY, startNodeDto.Board, MoveEnum.N, null, 0);
            NodeInProcessing = InitialNode;
            VisitedStates = 1;
            ExploredStates = new Dictionary<string, int>();
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
                    possibleMove, NodeInProcessing, NodeInProcessing.DepthLevel + 1);

                if (!ExploredStates.ContainsKey(newNode.ToString()) ||
                    ExploredStates[newNode.ToString()] > newNode.DepthLevel)
                {
                    AddNode(newNode);
                    VisitedStates++;
                }
            }
        }

        public void Solve()
        {
            Console.Out.WriteLine("Starting Computing");
            Console.Out.WriteLine(WritePaths.SolutionFilePath);
            Stopwatch time = new Stopwatch();
            time.Start();
            while (GetNodesInContainer() > 0)
            {
                NodeInProcessing = GetNode();
                if (!ExploredStates.ContainsKey(NodeInProcessing.ToString()))
                {
                    ExploredStates.Add(NodeInProcessing.ToString(), NodeInProcessing.DepthLevel);
                }
                else if (NodeInProcessing.DepthLevel < ExploredStates[NodeInProcessing.ToString()])
                {
                    ExploredStates[NodeInProcessing.ToString()] = NodeInProcessing.DepthLevel;
                }

                if (NodeInProcessing.DepthLevel > DeepestRecursion)
                {
                    DeepestRecursion = NodeInProcessing.DepthLevel;
                }

                if (NodeInProcessing.IsSolved())
                {
                    time.Stop();
                    SolutionInfo solution = new SolutionInfo()
                    {
                        Steps = NodeInProcessing.GetSolutionPath(),
                        Length = NodeInProcessing.DepthLevel,
                        DeepestRecursionReached = DeepestRecursion,
                        ProcessingTime = time,
                        StatesProcessed = ExploredStates.Count,
                        StatesVisited = VisitedStates
                       
                    };
                    DataWriter.WriteSolution(WritePaths.SolutionFilePath,solution);
                    DataWriter.WriteInfo(WritePaths.InfoFilePath, solution);
                    
                    Console.Out.WriteLine("Computing Complete, time: " + time.Elapsed.TotalMilliseconds.ToString("F3") + "ms");
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
