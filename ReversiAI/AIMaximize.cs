using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIMaximize : IReversiAI {
        Dictionary<string, double> stats = new Dictionary<string, double>();
        int turnsTaken;
        int choices;

        public byte getNextMove(GameState state) {
            var moves = GameState.getValidMoves(state, state.nextTurn);
            choices += moves.Count((b) => { return b > 0; });
            turnsTaken++;

            byte best = 255;
            int tempCount, bestTurns = 0;
            GameState temp;
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    temp = GameState.getTransformedBoard(state, i & 7, i >> 3);
                    tempCount = temp.squares.Count((b) => { return b == state.nextTurn; });
                    if (tempCount > bestTurns) {
                        best = i;
                        bestTurns = tempCount;
                    }
                }
            }
            return best;
        }

        public Dictionary<string, double> getStats() {
            stats["Average Branching: "] = (double)choices / turnsTaken;
            return stats;
        }

        public void setConfiguration(Dictionary<string, object> config) {
            //do nothing -- no configuration possible
        }
    }
}
