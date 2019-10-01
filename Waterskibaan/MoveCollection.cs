﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    static class MoveCollection
    {
        private static List<IMoves> _possibleMoves = new List<IMoves> { new Jump(), new Omdraaien(), new Salto(), new OpEenBeen() };

        public static List<IMoves> GetWillekeurigeMoves()
        {
            List<IMoves> moves = new List<IMoves>();
            Random random = new Random();
            int limit = random.Next(1, _possibleMoves.Count + 1);

            for (int i = 0; i < limit; i++)
            {
                moves.Add(_possibleMoves[i]);
            }

            return moves;
        }
    }
}
