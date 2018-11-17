using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class SolutionInfo
    {
        public static int SolutionLength { get; set; }
        public static string StatesVisited { get; set; }
        public static int StatesProcessed { get; set; }
        public static int DeepestRecursionReached { get; set; }
        public static double ProcessingTime { get; set; }
    }
}
