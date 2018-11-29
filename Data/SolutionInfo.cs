using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SolutionInfo
    {
        public string Steps { get; set; }
        public  int Length { get; set; }
        public  int StatesVisited { get; set; }
        public  int StatesProcessed { get; set; }
        public  int DeepestRecursionReached { get; set; }
        public Stopwatch ProcessingTime { get; set; }
    }
}
