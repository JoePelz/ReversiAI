using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AITreeAll : IReversiAI {
        GameStats stats = new GameStats();
        private byte levels;
        private int statesVisited;
        private Node root;
        private Stopwatch timer;
        
        /// <summary>
        /// Constructor, creates an empty AI (no root yet in the game tree)
        /// </summary>
        /// <param name="levels">The maximum number of levels to search for the solution. </param>
        public AITreeAll(byte levels) {
            timer = new Stopwatch();
            this.levels = levels;
            stats.augmentDepth(levels);
            statesVisited = 0;
        }

        /// <summary>
        /// Main AI access method. Given a particular game state, what should the next move be?
        /// </summary>
        /// <param name="state">The current state of the game.  Throws exception on null.</param>
        /// <returns>The index in the board to put the next token.</returns>
        public byte getNextMove(GameState state) {
            timer.Restart();
            updateRoot(state);
            //root = new Node(state);
            statesVisited = 1;
            
            
            //Build the entire game tree, expanding ALL branches
            statesVisited += root.growAll(levels);

            
            byte best = 255;
            int minWorst = int.MinValue, tempWorst;
            //for each option, get the worst case that could result from making the move.
            //  (after _levels_ of branching)
            foreach (var kvp in root.children) {
                tempWorst = kvp.Value.getWorstCase(state.nextTurn);
                if (tempWorst > minWorst) {
                    best = kvp.Key;
                    minWorst = tempWorst;
                }
            }

            //update stats object
            timer.Stop();
            stats.branches += root.children.Count();
            stats.turnsRepresented++;
            stats.augmentTime(timer.ElapsedMilliseconds / 1000.0);
            stats.augmentLeaves(statesVisited);

            root = root.children[best];
            return best;
        }

        /// <summary>
        /// Attempt to build this turns game tree from the previous turn's game tree. 
        /// Effectively move down a branch and discard the old states and branches.
        /// </summary>
        /// <param name="state">The state to update to</param>
        private void updateRoot(GameState state) {
            if (root != null) {
                foreach (var kvp in root.children) {
                    if (kvp.Value != null && kvp.Value.value.Equals(state)) {
                        root = kvp.Value;
                        return;
                    }
                }
            }
            root = new Node(state);
        }

        public void setConfiguration(Dictionary<string, object> config) {
            //do nothing--yet!
        }

        public GameStats getStats() {
            return stats;
        }

        /// <summary>
        /// Node class representing a particular state of the game, or vertex in a tree,
        /// where edges are `moves made` and vertices are resulting game states.
        /// </summary>
        public class Node {
            public GameState value;
            public Dictionary<byte, Node> children;

            /// <summary>
            /// Constructor: Node and the game state it represents.  
            /// Registers a list of valid moves onward from this state, 
            /// but does not expand them into full nodes.
            /// </summary>
            /// <param name="state">The state of the game to construct this node around.</param>
            public Node(GameState state) {
                value = state;
                var moves = GameState.getValidMoves(state, state.nextTurn);
                children = new Dictionary<byte, Node>();
                for (byte i = 0; i < 64; i++) {
                    if (moves[i] > 0) {
                        children[i] = null;
                    }
                }
            }

            /// <summary>
            /// Get the child gamestate of a node based on making a particular move.  
            /// If that child is registered as a valid move, return that child (also a node).
            /// </summary>
            /// <param name="i">The index of the move being made on the game board.  Not validated against game rules.</param>
            /// <returns>A node holding the resulting game state of making a particular move.</returns>
            public Node getChild(byte i) {
                if (!children.ContainsKey(i)) {
                    return null;
                }
                //If the child has been expanded, return that child
                if (children[i] != null) {
                    return children[i];
                }
                //Otherwise, expand that child into a node by calculating the new board state
                //given that move.
                children[i] = new Node(GameState.getTransformedBoard(value, i & 7, i >> 3));

                return children[i];
            }

            /// <summary>
            /// As long as depth is greater than 1, recursively grow all children to depth-1
            /// </summary>
            /// <param name="depth">How many levels deep to explore. 
            /// 1 examine all current move options. 
            /// 3 means examine options this turn, the opponent's options, and then what you would be able to do next turn.</param>
            /// <returns>The number of game states visited.</returns>
            public int growAll(int depth) {
                int visits = 0;
                if (depth <= 1) {
                    return 1;
                }

                //getChild (to expand the child) and growAll (minus 1 depth level) to recurse.
                foreach (byte i in children.Keys.ToList()) {
                    visits += getChild(i).growAll(depth - 1);
                }
                return visits;
            }

            /// <summary>
            /// Get the worst case resulting from choosing a particular node.
            /// The worst case is the case that has the lowest heuristic value. (see evaluateBoard)
            /// </summary>
            /// <param name="player">The player who's tokens we are concerned with.</param>
            /// <returns>The fewest friendly tokens that may result from this state onward.</returns>
            public int getWorstCase(byte player) {
                int minCount = 255, tempCount;
                //Find the worst case among the children.
                foreach(Node branch in children.Values) {
                    if (branch != null) {
                        tempCount = branch.getWorstCase(player);
                        if (tempCount < minCount) {
                            minCount = tempCount;
                        }
                    }
                }
                //In the event this is a leaf node, count the number of friendlies, and return that.
                //Note: this only works for expanding odd numbers of levels
                if (minCount == 255) {
                    minCount = evaluateBoard(value, player);
                }

                return minCount;
            }

            /// <summary>
            /// Calculate the value of the board configuration for the given player.
            /// </summary>
            /// <param name="state">The game state to evaluate.</param>
            /// <param name="player">The player to evaluate the board with respect to.</param>
            /// <returns>The value of the board configuration for the given player</returns>
            public int evaluateBoard(GameState state, byte player) {
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
            private int getTilesOwned(GameState state, byte player) {
                return state.squares.Count((b) => { return b == player; });
            }

            /// <summary>
            /// Counts the value of the tiles owned by the given player on the board, 
            /// with non-uniform tile values based on location.
            /// </summary>
            /// <param name="state">The game configuration to examine</param>
            /// <param name="player">The player who's tiles are to be counted</param>
            /// <returns>The weighted value of the tiles on the given board owned by the given player</returns>
            private int getWeightedTilesBonus(GameState state, byte player) {
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
            private int getStableTiles(GameState state, byte player) {
                int count = 0;
                for(int i = 0; i < 64; i++) {
                    if (state.squares[i] == player && GameState.isStable(state, i & 7, i >> 3)) {
                        count++;
                    }
                }
                return count;
            }
        }
    }
}
