using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI {
    public class GameState {
        /*  The next player to play. 1 is white, 2 is black. */
        public byte nextTurn = 1;
        public byte[] squares = new byte[64];

        public static GameState createInitialSetup() {
            GameState s = new GameState();
            s.squares[27] = s.squares[36] = 2;
            s.squares[28] = s.squares[35] = 1;
            return s;
        }

        /// <summary>
        /// Creates an array of 64 entries. If a move is a valid move, then result[move] == 1.
        /// </summary>
        /// <param name="state">The start-point to search for moves from</param>
        /// <param name="player">The player to find moves for</param>
        /// <returns>An array of all board spaces, 
        /// where result[move] == 1 if `move` is a valid move. 
        /// Ex: [0, 0, 0, 1, 0, ...]</returns>
        public static byte[] getValidMoves(GameState state, byte player) {
            byte[] results = new byte[64];
            for (int i = 0; i < 64; i++) {
                if (validMove(state, player, i & 7, i >> 3)) {
                    results[i] = 1;
                }
            }
            return results;
        }

        /// <summary>
        /// Creates an array of valid moves, where the value of each entry is a new move.
        /// </summary>
        /// <param name="state">The start-point to search for moves from</param>
        /// <param name="player">The player to find moves for</param>
        /// <returns>An array of all valid moves. Ex: [24, 25, 37].</returns>
        public static byte[] getFilterValidMoves(GameState state, byte player) {
            byte[] results = new byte[64];
            int j = 0;
            for (int i = 0; i < 64; i++) {
                if (validMove(state, player, i & 7, i >> 3)) {
                    results[j++] = (byte)i;
                }
            }
            Array.Resize(ref results, j);
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

        public static bool isStable(GameState state, int x, int y) {
            if (state.squares[x | y << 3] == 0) {
                return false;
            }
            byte friend = state.squares[x | y << 3];


            //horizontal
            int lost = 0;
            for (int tx = x - 1; tx >= 0; tx--) if (state.squares[tx | y << 3] != friend) { lost++; break; }
            if (lost == 1) {
                for (int tx = x + 1; tx <= 7; tx++) if (state.squares[tx | y << 3] != friend) return false;
            }

            //vertical
            lost = 0;
            for (int ty = y - 1; ty >= 0; ty--) if (state.squares[x | ty << 3] != friend) { lost++; break; }
            if (lost == 1) {
                for (int ty = y + 1; ty <= 7; ty++) if (state.squares[x | ty << 3] != friend) return false;
            }

            //diagonal 1
            lost = 0;
            for (int ty = y - 1, tx = x - 1; ty >= 0 && tx >= 0; ty--, tx--) if (state.squares[tx | ty << 3] != friend) { lost++; break; }
            if (lost == 1) {
                for (int ty = y + 1, tx = x + 1; ty <= 7 && tx <= 7; ty++, tx++) if (state.squares[tx | ty << 3] != friend) return false;
            }
            //diagonal 1
            lost = 0;
            for (int ty = y + 1, tx = x - 1; ty <= 7 && tx >= 0; ty++, tx--) if (state.squares[tx | ty << 3] != friend) { lost++; break; }
            if (lost == 1) {
                for (int ty = y - 1, tx = x + 1; ty >= 0 && tx <= 7; ty--, tx++) if (state.squares[tx | ty << 3] != friend) return false;
            }

            return true;
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
            int dx, dy, rx, ry;
            for (dx = -1; dx < 2; dx++) { //-1, 0, +1
                for (dy = -8; dy < 16; dy += 8) { //-8, 0, +8
                    if (dx == 0 && dy == 0) continue; // skip 0,0
                    rx = x + dx;
                    ry = (y << 3) + dy;
                    //if it's in bounds and an enemy token
                    if (rx >= 0 && rx < 8 && ry >= 0 && ry < 64 && past.squares[rx | ry] == (past.nextTurn ^ 3)) {
                        rx += dx;
                        ry += dy;
                        //while we're still in bounds, try to find a friend token
                        while (rx >= 0 && rx < 8 && ry >= 0 && ry < 64) {
                            if (past.squares[rx | ry] == past.nextTurn) {
                                //reverse and flip!
                                rx -= dx;
                                ry -= dy;
                                while ((rx | ry) != index) {
                                    result.squares[rx | ry] = past.nextTurn;
                                    rx -= dx;
                                    ry -= dy;
                                }
                                break;
                            }
                            if (past.squares[rx | ry] == 0) {
                                break;
                            }
                            rx += dx;
                            ry += dy;
                        }
                    }
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

        public bool Equals(GameState other) {
            if (nextTurn != other.nextTurn) return false;

            for (int i = 0; i < 64; i++) {
                if (squares[i] != other.squares[i]) return false;
            }

            return true;
        }
    }
}
