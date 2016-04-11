using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIFirst : IReversiAI {
        Dictionary<string, double> stats = new Dictionary<string, double>();
        int turnsTaken;
        int choices;

        public byte getNextMove(GameState state) {
            var moves = GameState.getValidMoves(state, state.nextTurn);
            choices += moves.Count((b) => { return b > 0; });
            turnsTaken++;

            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    return i;
                }
            }
            return 255;
        }

        public Dictionary<string, double> getStats() {
            stats["Average Branching: "] = (double)choices / turnsTaken;
            return stats;
        }

        public void setConfiguration(Dictionary<string, object> config) {
            //do nothing. No configuration possible.
        }
    }
}
