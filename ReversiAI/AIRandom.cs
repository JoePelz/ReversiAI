using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIRandom : IReversiAI {
        private static Random rng = new Random();
        public byte getNextMove(GameState state) {
            Thread.Sleep(5000);
            var moves = GameState.getValidMoves(state, state.nextTurn);
            int options;
            options = moves.Count((b) => { return b > 0; });
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    if (rng.Next(options) == 0) {
                        return i;
                    } else {
                        options--;
                    }
                }
            }
            return 255;
        }
    }
}
