using System.IO;

namespace Data
{
    public static class DataReader
    {
        public static NodeDTO ReadFirstNode(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                string[] diemensions = sr.ReadLine()?.Split(' ');
                byte x = byte.Parse(diemensions[0]);
                byte y = byte.Parse(diemensions[1]);

                byte[] board = new byte[x * y];
                for (int i = 0; i < y; i++)
                {
                    string[] line = sr.ReadLine()?.Split(' ');
                    for (int j = 0; j < x; j++)
                    {
                        board[i * x + j] = byte.Parse(line[j]);
                    }
                }
                return new NodeDTO()
                {
                    Board = board,
                    X = x,
                    Y = y
                };
            }
        }
    }
}