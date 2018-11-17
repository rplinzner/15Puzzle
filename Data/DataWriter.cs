using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DataWriter
    {
        public static void WriteSolutionToFile(string path)
        {
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(SolutionInfo.SolutionLength);
                if (SolutionInfo.SolutionLength >= 0)
                {
                    outputFile.Write(SolutionInfo.StatesVisited);
                }

            }
        }

        public static void WriteInfoToFile( string path)
        {
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(SolutionInfo.SolutionLength);
                outputFile.WriteLine(SolutionInfo.StatesVisited);
                outputFile.WriteLine(SolutionInfo.StatesProcessed);
                outputFile.WriteLine(SolutionInfo.DeepestRecursionReached);
                outputFile.WriteLine(SolutionInfo.ProcessingTime.ToString("F3"));
            }
        }
    }
}
