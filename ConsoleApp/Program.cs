using System;
using Data;
using Solvers;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadParameters(args);
            Solve();
        }

        #region Fields

        private static string _algorithm;
        private static string _strategy;
        private static string _startingStateFileName;
        private static string _solutionFileName;
        private static string _informationFileName;
        private static WritePathDTO _paths;
        private static NodeDTO _startingNode;
        #endregion

        private static void ReadParameters(string[] args)
        {
            if (args.Length != 5)
            {
                Console.Out.WriteLine("Please provide 5 parameters");
                return;
            }

            _algorithm = args[0];
            _strategy = args[1];
            _startingStateFileName = args[2];
            _solutionFileName = args[3];
            _informationFileName = args[4];
            _paths = new WritePathDTO()
            {
                SolutionFilePath = _solutionFileName,
                InfoFilePath = _informationFileName
            };
            _startingNode = DataReader.ReadFirstNode(_startingStateFileName);
        }
        private static void Solve()
        {
            if (string.IsNullOrEmpty(_algorithm))
            {
                return;
            }

            SolverBase solver;

            switch (_algorithm)
            {
                case "bfs":
                
                    solver = new BfsSolver(_strategy, _startingNode, _paths);
                    break;
                case "dfs":
                    solver = new DfsSolver(_strategy, _startingNode, _paths);
                    break;
                case "astr":
                    if (_strategy == "hamm")
                    {
                        solver = new AStarSolver(_startingNode, _paths, AStarHeuresticEnum.Hamming);
                    }
                    else
                    {
                        solver = new AStarSolver(_startingNode, _paths, AStarHeuresticEnum.Manhattan);
                    }
                    break;
                case "greedy":
                    if (_strategy == "diag")
                    {
                        solver = new GreedySolver(_startingNode, _paths, GreedyHeuresticEnum.Diagonal);
                    }
                    else
                    {
                        solver = new GreedySolver(_startingNode, _paths, GreedyHeuresticEnum.Euclides);
                    }
                    break;
                
                default:
                    solver = new AStarSolver(_startingNode, _paths, AStarHeuresticEnum.Manhattan);
                    break;
            }

            solver.Solve();
        }
    }

   
}
