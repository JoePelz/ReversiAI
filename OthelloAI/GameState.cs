using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloAI {
    public class GameState {
        /*  The next player to play. 1 is black, 2 is white. */
        public byte nextTurn = 1;
        public byte[] squares = new byte[64];

        public static GameState createInitialSetup() {
            GameState s = new GameState();
            s.squares[27] = s.squares[36] = 2;
            s.squares[28] = s.squares[35] = 1;
            return s;
        }
    }
}
