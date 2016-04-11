using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    //Random Number Generator Generator
    public class RNGG {
        private static int i = 1;
        public static Random getRNG() { return new Random(i++); }
    }

    public class AIRandom : IReversiAI {
        private Random rng = RNGG.getRNG();
        Dictionary<string, double> stats = new Dictionary<string, double>();
        int turnsTaken;
        int choices;
        
        public byte getNextMove(GameState state) {
            var moves = GameState.getValidMoves(state, state.nextTurn);
            int options;
            options = moves.Count((b) => { return b > 0; });
            choices += options;
            turnsTaken++;

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

        public Dictionary<string, double> getStats() {
            stats["Average Branching: "] = (double)choices / turnsTaken;
            return stats;
        }

        public void setConfiguration(Dictionary<string, object> config) {
            //do nothing - no configuration possible
        }
    }
}
