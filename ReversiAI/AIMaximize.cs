using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIMaximize : IReversiAI {
        public byte getNextMove(GameState state) {
            Thread.Sleep(5000);
            var moves = GameState.getValidMoves(state, state.nextTurn);
            byte best = 255;
            int tempCount, bestTurns = 0;
            GameState temp;
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    temp = GameState.getTransformedBoard(state, moves[i] & 7, moves[i] >> 3);
                    tempCount = temp.squares.Count((b) => { return b == state.nextTurn; });
                    if (tempCount > bestTurns) {
                        best = i;
                        bestTurns = tempCount;
                    }
                }
            }
            return best;
        }
    }
}
