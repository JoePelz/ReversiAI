using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIFirst : IReversiAI {
        GameStats stats = new GameStats();

        public byte getNextMove(GameState state) {
            var moves = GameState.getValidMoves(state, state.nextTurn);
            stats.branches += moves.Count((b) => { return b > 0; });
            stats.turnsRepresented++;

            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    return i;
                }
            }
            return 255;
        }

        public GameStats getStats() {
            return stats;
        }

        public void setConfiguration(AIConfiguration config) {
            //do nothing. No configuration possible.
        }
    }
}
