using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIMinimize : IReversiAI {
        GameStats stats = new GameStats();

        public byte getNextMove(GameState state) {
            var moves = GameState.getValidMoves(state, state.nextTurn);
            stats.branches += moves.Count((b) => { return b > 0; });
            stats.turnsRepresented++;

            byte best = 255;
            int tempCount, bestTurns = 64;
            GameState temp;
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    temp = GameState.getTransformedBoard(state, i & 7, i >> 3);
                    tempCount = temp.squares.Count((b) => { return b == state.nextTurn; });
                    if (tempCount < bestTurns) {
                        best = i;
                        bestTurns = tempCount;
                    }
                }
            }
            return best;
        }

        public GameStats getStats() {
            return stats;
        }

        public void setConfiguration(AIConfiguration config) {
            //do nothing -- no configuration possible
        }
    }
}