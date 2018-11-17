using System;
using Data;
using Solvers;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
                Console.WriteLine("Too few arguments");
            else
            {
                SolverBase solver;
                NodeDTO node = DataReader.ReadFirstNode(args[2]);
                WritePathDTO paths = new WritePathDTO();
                paths.SolutionFilePath = args[3];
                paths.InfoFilePath = args[4];
                switch (args[0])
                {
                    case "bfs":
                        {
                            solver = new BfsSolver(args[1], node, paths);
                            break;

                        }
                    case "dfs":
                        {
                            solver = new DfsSolver(args[1], node, paths);
                            break;

                        }
                    case "astr":
                        {
                            if (args[1] == "hamm")
                                
                                solver = new AStarSolver(node, paths, AStarHeuresticEnum.Hamming);
                            else
                                solver = new AStarSolver(node, paths, AStarHeuresticEnum.Manhattan);
                            break;

                        }
                    default:
                        {
                            solver = new BfsSolver(args[1], node, paths);
                            break;
                        }

                }
                Console.WriteLine(args[0] +" "+ node.X + " " + node.Y + " " + paths.SolutionFilePath + " " + paths.InfoFilePath);
                solver.Solve();
            }


        }
    }
}
