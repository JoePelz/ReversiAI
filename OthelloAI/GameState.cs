using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
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
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    if (validMove(state, player, x, y)) {
                        results[x | y << 3] = 1;
                    }
                }
            }
            return results;
        }

        public static bool anyValidMoves(GameState state, byte player) {
            byte[] results = new byte[64];
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    if (validMove(state, player, x, y)) {
                        return true;
                    }
                }
            }
            return false;
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

        //Precondition: the given coordinates are a legal move for the current player.
        public static GameState getTransformedBoard(GameState past, int x, int y) {
            GameState result = new GameState();
            Array.Copy(past.squares, result.squares, 64);
            result.nextTurn = past.nextTurn;

            int index = x | y << 3;

            //update clicked square
            result.squares[index] = past.nextTurn;

            #region Cascade
            //cascade changes in all directions
            //left
            for (int dx = x - 1; dx >= 0; dx--) {
                if (result.squares[index - x + dx] == 0) {
                    break;
                }
                if (result.squares[index - x + dx] == result.nextTurn) {
                    while (++dx < x) {
                        result.squares[index - x + dx] = result.nextTurn;
                    }
                    break;
                }
            }
            //right
            for (int dx = x + 1; dx < 8; dx++) {
                if (result.squares[index - x + dx] == 0) {
                    break;
                }
                if (result.squares[index - x + dx] == result.nextTurn) {
                    while (--dx > x) {
                        result.squares[index - x + dx] = result.nextTurn;
                    }
                    break;
                }
            }
            //up
            for (int dy = y - 1; dy >= 0; dy--) {
                if (result.squares[index + (dy - y << 3)] == 0) {
                    break;
                }
                if (result.squares[index + (dy - y << 3)] == result.nextTurn) {
                    while (++dy < y) {
                        result.squares[index + (dy - y << 3)] = result.nextTurn;
                    }
                    break;
                }
            }
            //down
            for (int dy = y + 1; dy < 8; dy++) {
                if (result.squares[index + (dy - y << 3)] == 0) {
                    break;
                }
                if (result.squares[index + (dy - y << 3)] == result.nextTurn) {
                    while (--dy > y) {
                        result.squares[index + (dy - y << 3)] = result.nextTurn;
                    }
                    break;
                }
            }


            //upleft
            for (int dx = x - 1, dy = y - 1; dx >= 0 && dy >= 0; dx--, dy--) {
                if (result.squares[index + (dy - y << 3) + (dx - x)] == 0) {
                    break;
                }
                if (result.squares[index + (dy - y << 3) + (dx - x)] == result.nextTurn) {
                    while (++dy < y) {
                        ++dx;
                        result.squares[index + (dy - y << 3) + (dx - x)] = result.nextTurn;
                    }
                    break;
                }
            }
            //upright
            for (int dx = x + 1, dy = y - 1; dx < 8 && dy >= 0; dx++, dy--) {
                if (result.squares[index + (dy - y << 3) + (dx - x)] == 0) {
                    break;
                }
                if (result.squares[index + (dy - y << 3) + (dx - x)] == result.nextTurn) {
                    while (++dy < y) {
                        --dx;
                        result.squares[index + (dy - y << 3) + (dx - x)] = result.nextTurn;
                    }
                    break;
                }
            }
            //downright
            for (int dx = x + 1, dy = y + 1; dx < 8 && dy < 8; dx++, dy++) {
                if (result.squares[index + (dy - y << 3) + (dx - x)] == 0) {
                    break;
                }
                if (result.squares[index + (dy - y << 3) + (dx - x)] == result.nextTurn) {
                    while (--dy > y) {
                        --dx;
                        result.squares[index + (dy - y << 3) + (dx - x)] = result.nextTurn;
                    }
                    break;
                }
            }
            //downleft
            for (int dx = x - 1, dy = y + 1; dx >= 0 && dy < 8; dx--, dy++) {
                if (result.squares[index + (dy - y << 3) + (dx - x)] == 0) {
                    break;
                }
                if (result.squares[index + (dy - y << 3) + (dx - x)] == result.nextTurn) {
                    while (--dy > y) {
                        ++dx;
                        result.squares[index + (dy - y << 3) + (dx - x)] = result.nextTurn;
                    }
                    break;
                }
            }
            #endregion Cascade

            //switch players
            //check if the next player can actually go
            if (GameState.anyValidMoves(result, (byte)(result.nextTurn ^ 3))) {
                result.nextTurn = (byte)(result.nextTurn ^ 3);
            }

            return result;
        }
    }
}
