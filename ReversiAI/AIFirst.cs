using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    class AIFirst : IReversiAI {
        public byte getNextMove(GameState state) {
            var moves = GameState.getValidMoves(state, state.nextTurn);
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    return i;
                }
            }
            return 255;
        }
    }
}
