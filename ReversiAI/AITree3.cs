using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AITree3 : IReversiAI {
        public byte getNextMove(GameState state) {
            Node root = new Node(state);
            root.growAll(5); //grows three levels past the root. (root + 3 rows below. Next row is nulls.)
            byte best = 255;
            int minWorst = 0, tempWorst;
            foreach (var kvp in root.children) {
                tempWorst = kvp.Value.getWorstCase();
                if (tempWorst > minWorst) {
                    best = kvp.Key;
                    minWorst = tempWorst;
                }
            }
            //Thread.Sleep(2000);
            return best;
        }

        public class Node {
            public GameState value;
            public Dictionary<byte, Node> children;

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

            public Node getChild(byte i) {
                if (!children.ContainsKey(i)) {
                    return null;
                }
                if (children[i] != null) {
                    return children[i];
                }
                children[i] = new Node(GameState.getTransformedBoard(value, i & 7, i >> 3));

                return children[i];
            }

            public void growAll(int depth) {
                if (depth == 1) {
                    return;
                }

                foreach (byte i in children.Keys.ToList()) {
                    getChild(i).growAll(depth - 1);
                }
            }

            public int getWorstCase() {
                int minCount = 255, tempCount;
                foreach(Node branch in children.Values) {
                    if (branch != null) {
                        tempCount = branch.getWorstCase();
                        if (tempCount < minCount) {
                            minCount = tempCount;
                        }
                    }
                }
                if (minCount == 255) {
                    minCount = value.squares.Count((b) => { return b == value.nextTurn; });
                }

                return minCount;
            }
        }
    }
}
