using System;
using System.IO;

namespace Data
{
    public static class DataReader
    {
        public static NodeDTO ReadFirstNode(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                string[] dimensions = sr.ReadLine()?.Split(' ', '\r', '\n');
                byte y = byte.Parse(dimensions[0]);
                byte x = byte.Parse(dimensions[1]);

                byte[] board = new byte[x * y];
                for (int i = 0; i < y; i++)
                {
                    string[] line = sr.ReadLine()?.Split(' ', '\r', '\n');
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