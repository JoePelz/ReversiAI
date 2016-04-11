using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AINegaMax : IReversiAI {
        public struct Score {
            public int value;
            public byte move;
            public Score(byte m, int v) {
                move = m;
                value = v;
            }

            public static Score operator -(Score s) {
                return new Score(s.move, -s.value);
            }
        }

        GameStats stats = new GameStats();
        private byte levels;
        private int statesVisited;
        private Stopwatch timer;
        private bool pruning;
        /// <summary>
        /// Constructor, creates an empty AI (no root yet in the game tree)
        /// </summary>
        /// <param name="levels">The maximum number of levels to search for the solution. </param>
        public AINegaMax() {
            timer = new Stopwatch();
            statesVisited = 0;
        }

        /// <summary>
        /// Main AI access method. Given a particular game state, what should the next move be?
        /// </summary>
        /// <param name="state">The current state of the game.  Throws exception on null.</param>
        /// <returns>The index in the board to put the next token.</returns>
        public byte getNextMove(GameState state) {
            timer.Restart();
            statesVisited = 1;


            Score result;
            if (pruning) {
                result = negamax_pruning(state, levels, int.MinValue, int.MaxValue, state.nextTurn * -2 + 3);
            } else {
                result = negamax(state, levels, state.nextTurn * -2 + 3);
            }

            //update stats object
            timer.Stop();
            stats.branches += GameState.getFilterValidMoves(state, state.nextTurn).Length;
            stats.turnsRepresented++;
            stats.augmentTime(timer.ElapsedMilliseconds / 1000.0);
            stats.augmentLeaves(statesVisited);
            stats.augmentDepth(levels);

            return result.move;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="depth"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="player">1 means player 1, -1 means player 2</param>
        private Score negamax_pruning(GameState node, int depth, int alpha, int beta, int player) {
            if (depth == 0) {
                return new Score(255, player * evaluateBoard(node));
            }
            statesVisited++;

            var allMoves = GameState.getFilterValidMoves(node, node.nextTurn);
            if (allMoves.Length == 0) {
                return new Score(255, player * evaluateBoard(node));
            }

            Score v;
            Score bestValue = new Score(255, int.MinValue);

            foreach (var move in allMoves) {
                v = -negamax_pruning(GameState.getTransformedBoard(node, move & 7, move >> 3), depth - 1, -beta, -alpha, -player);
                if (v.value > bestValue.value) {
                    bestValue.value = v.value;
                    bestValue.move = move;
                }
                if (v.value > alpha) alpha = v.value;
                if (alpha >= beta) {
                    break;
                }
            }
            return bestValue;
        }
        private Score negamax(GameState node, int depth, int player) {
            if (depth == 0) {
                return new Score(255, player * evaluateBoard(node));
            }
            statesVisited++;

            var allMoves = GameState.getFilterValidMoves(node, node.nextTurn);
            if (allMoves.Length == 0) {
                return new Score(255, player * evaluateBoard(node));
            }

            Score v;
            Score bestValue = new Score(255, int.MinValue);

            foreach (var move in allMoves) {
                v = -negamax(GameState.getTransformedBoard(node, move & 7, move >> 3), depth - 1, -player);
                if (v.value > bestValue.value) {
                    bestValue.value = v.value;
                    bestValue.move = move;
                }
            }
            return bestValue;
        }

        public void setConfiguration(AIConfiguration config) {
            levels = (byte)config.maxDepth;
            stats.augmentDepth(levels);
            pruning = config.ABPruning;
        }

        public GameStats getStats() {
            return stats;
        }

        /// <summary>
        /// Calculate the value of the board configuration for the given player.
        /// </summary>
        /// <param name="state">The game state to evaluate.</param>
        /// <param name="player">The player to evaluate the board with respect to.</param>
        /// <returns>The value of the board configuration for the given player</returns>
        public static int evaluateBoard(GameState state, byte player = 1) {
            int numTilesOwned = getTilesOwned(state, player);
            //int numWeightsBonus = getWeightedTilesBonus(state, player);
            int numStableTiles = getStableTiles(state, player);
            int numLostStableTiles = getStableTiles(state, (byte)(player ^ 3));
            return numTilesOwned + numStableTiles * 64 - numLostStableTiles * 64;
        }

        /// <summary>
        /// Counts the number of tiles owned by the given player on the board. 
        /// I.e. Answers the question: "how many tiles does white have?"
        /// </summary>
        /// <param name="state">The game configuration to examine</param>
        /// <param name="player">The player who's tiles are to be counted</param>
        /// <returns>The number of tiles on the given board owned by the given player</returns>
        public static int getTilesOwned(GameState state, byte player) {
            return state.squares.Count((b) => { return b == player; });
        }

        /// <summary>
        /// Counts the value of the tiles owned by the given player on the board, 
        /// with non-uniform tile values based on location.
        /// </summary>
        /// <param name="state">The game configuration to examine</param>
        /// <param name="player">The player who's tiles are to be counted</param>
        /// <returns>The weighted value of the tiles on the given board owned by the given player</returns>
        public static int getWeightedTilesBonus(GameState state, byte player) {
            int count = 0;
            const int cornerValue = 3; //corner value
            const int edgeValue = 2; //edge value
            const int badC = -1; //offcorner edge (w/o corner)
            const int badX = -1; //offcorner diagonal (w/o corner)
                                 // 0  1 ..  6  7
                                 // 8  9    14 15
                                 // .           .
                                 // .           .
                                 //48 49    54 55
                                 //56 57 .. 62 63
                                 //goodEdges are the numbers in the ellipses above
            int[] goodEdges = { 2, 3, 4, 5, 58, 59, 60, 61, 16, 24, 32, 40, 23, 31, 39, 37 };
            foreach (var i in goodEdges) {
                if (state.squares[i] == player) {
                    count += edgeValue;
                }
            }

            //Check top right corner.
            if (state.squares[7] == player) {
                count += cornerValue;
                if (state.squares[15] == player) count += badC;
                if (state.squares[6] == player) count += badC;
            } else {
                if (state.squares[6] == player) count -= badC;
                if (state.squares[15] == player) count -= badC;
                if (state.squares[14] == player) count -= badX;
            }
            //Check bottom right corner.
            if (state.squares[63] == player) {
                count += cornerValue;
                if (state.squares[62] == player) count += badC;
                if (state.squares[55] == player) count += badC;
            } else {
                if (state.squares[62] == player) count -= badC;
                if (state.squares[55] == player) count -= badC;
                if (state.squares[54] == player) count -= badX;
            }
            //Check top left corner.
            if (state.squares[0] == player) {
                count += cornerValue;
                if (state.squares[1] == player) count += badC;
                if (state.squares[8] == player) count += badC;
            } else {
                if (state.squares[1] == player) count -= badC;
                if (state.squares[8] == player) count -= badC;
                if (state.squares[9] == player) count -= badX;
            }
            //Check bottom left corner.
            if (state.squares[56] == player) {
                count += cornerValue;
                if (state.squares[48] == player) count += badC;
                if (state.squares[57] == player) count += badC;
            } else {
                if (state.squares[48] == player) count -= badC;
                if (state.squares[57] == player) count -= badC;
                if (state.squares[49] == player) count -= badX;
            }

            return count;
        }

        /// <summary>
        /// Counts the number of unchangeable tiles on the board owned by a player. 
        /// Unchangeable tiles have no possible way of being flipped to the other color.
        /// </summary>
        /// <param name="state">The game configuration to examine.</param>
        /// <param name="player">The player to count the stable tiles of.</param>
        /// <returns>The weighted value of the tiels on the given board owned by the given player</returns>
        public static int getStableTiles(GameState state, byte player) {
            int count = 0;
            for (int i = 0; i < 64; i++) {
                if (state.squares[i] == player && GameState.isStable(state, i & 7, i >> 3)) {
                    count++;
                }
            }
            return count;
        }

    }
}
