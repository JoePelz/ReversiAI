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

        public static byte[] getValidMoves(GameState state, byte player) {
            byte[] results = new byte[64];
            int i = 0;
            for(int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    if (validMove(state, player, x, y)) {
                        results[i++] = 1;
                    }
                }
            }
            return results;
        }

        public static bool validMove(GameState state, byte player, int x, int y) {
            if (state.squares[x | y << 3] > 0) return false;
            
            int dx, dy, rx, ry;
            for (dx = -1; dx < 2; dx++) { //-1, 0, +1
                for (dy = -8; dy < 16; dy += 8) { //-8, 0, +8
                    if (dx == 0 && dy == 0) continue; // skip 0,0
                    rx = x + dx;
                    ry = (y << 3) + dy;
                    //if it's in bounds and an enemy token
                    if (rx >= 0 && rx < 8 && ry >= 0 && ry < 64 && state.squares[rx | ry] == (player ^ 3)) {
                        rx += dx;
                        ry += dy;
                        //while we're still in bounds, try to find a friend token
                        while (rx >= 0 && rx < 8 && ry >= 0 && ry < 64) {
                            if (state.squares[rx | ry] == player) {
                                return true;
                            }
                            if (state.squares[rx|ry] == 0) {
                                break;
                            }
                            rx += dx;
                            ry += dy;
                        }
                    }
                }
            }
            return false;
        }
    }
}
