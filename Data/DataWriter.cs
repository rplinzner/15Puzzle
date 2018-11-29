using System.IO;

namespace Data
{
    public static class DataWriter
    {
        public static void WriteSolution(string filename, SolutionInfo solution)
        {
            int solutionLength = solution.Length;
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(solutionLength);
                if (solutionLength<0)
                {
                    return;
                }

                for (int i = 0; i < solutionLength; i++)
                {
                    sw.Write(solution.Steps);
                }
            }
        }

        public static void WriteInfo(string filename, SolutionInfo solution)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(solution.Length);
                sw.WriteLine(solution.StatesVisited);
                sw.WriteLine(solution.StatesProcessed);
                sw.WriteLine(solution.DeepestRecursionReached);
                sw.WriteLine(solution.ProcessingTime.Elapsed.TotalMilliseconds.ToString("F3"));
                
            }
        }
    }
}