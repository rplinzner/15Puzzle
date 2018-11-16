using System.Collections.Generic;

namespace Solvers
{
    public static class SolverHelp
    {
        public static List<MoveEnum> ConvertStringMovesToEnums(string moves)
        {
            List<MoveEnum> temp = new List<MoveEnum>();
            foreach (var move in moves)
            {
                if (move == 'L')
                {
                    temp.Add(MoveEnum.L);
                }
                if(move == 'R')
                {
                    temp.Add(MoveEnum.R);
                }
                if (move == 'U')
                {
                    temp.Add(MoveEnum.U);
                }
                if (move == 'D')
                {
                    temp.Add(MoveEnum.D);
                }
            }

            return temp;
        }
    }
}