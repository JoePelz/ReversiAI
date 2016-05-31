using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class AIProofNumber : IReversiAI {
        public enum Value { True, False, Unknown };
        public enum Type { AND, OR };
        public class Node {
            public int proof; //set, used
            public int disproof; //set, used
            public GameState state; //set, used
            public List<Node> children = new List<Node>(); //set, used
            public Value value; //set, used
            public int move; //set, used
            public bool expanded; //set, used
            public bool evaluated; //set, used
            public Type type; //set. may be wrong??
            public Node parent; //used
            public int depth;
        }
        GameStats stats = new GameStats();
        private int statesVisited;
        private Stopwatch timer;
        private int timeLimit;
        private byte goalWinner;
        private int depth;
        /// <summary>
        /// Constructor, creates an empty AI (no root yet in the game tree)
        /// </summary>
        public AIProofNumber() {
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
            goalWinner = state.nextTurn;

            depth = 0;
            Node root = new Node();
            root.state = state;
            root.depth = 0;
            ProofNumberSearch(root);
            byte result = 255;
            result = (byte)ChoseMove(root);

            //update stats object
            timer.Stop();
            stats.branches += GameState.getFilterValidMoves(state, state.nextTurn).Length;
            stats.turnsRepresented++;
            stats.augmentTime(timer.ElapsedMilliseconds / 1000.0);
            stats.augmentLeaves(statesVisited);
            stats.augmentDepth(depth);

            return result;
        }

        private void ProofNumberSearch(Node root) {
            /*
            Evaluate(root)
            SetProofAndDisproofNumbers(root);
            while (root.proof != 0 && root.disproof != 0 && ResourcesAvailable()) {
                mostProvingNode = SelectMostProving(root);
                DevelopNode(mostProvingNode);
                UpdateAncestors(mostProvingNode);
            }
            if (root.proof == 0) root.value = Value.True;
            else if (root.disproof == 0) root.value = Value.False;
            else root.value = Value.Unknown;
            */
            Evaluate(root);
            SetProofAndDisproofNumbers(root);
            while (root.proof != 0 && root.disproof != 0 && ResourcesAvailable()) {
                Node mostProvingNode = SelectMostProving(root);
                DevelopNode(mostProvingNode);
                UpdateAncestors(mostProvingNode);
            }

            if (root.proof == 0) root.value = Value.True;
            else if (root.disproof == 0) root.value = Value.False;
            else root.value = Value.Unknown;

            return;
        }

        private Node SelectMostProving(Node node) {
            int i = -1;
            if (node.children.Count() == 0) {
                return node;
            }
            while (node.expanded) {
                
                switch (node.type) {
                    case Type.OR:
                        i = 0;
                        while (node.children[i].proof != node.proof) {
                            i++;
                        }
                        break;
                    case Type.AND:
                        i = 0;
                        while (node.children[i].disproof != node.disproof) {
                            i++;
                        }
                        break;
                }
                if (i == -1) {
                    throw new Exception("Catastrophic Failure in SelectMostProving.");
                }
                node = node.children[i];
            }
            return node;
        }

        private int ChoseMove(Node node) {
            int i = -1;
            switch (node.type) {
                case Type.OR:
                    i = 0;
                    while (node.children[i].proof != node.proof) {
                        i++;
                    }
                    break;
                case Type.AND:
                    i = 0;
                    while (node.children[i].disproof != node.disproof) {
                        i++;
                    }
                    break;
            }
            if (i == -1) {
                throw new Exception("Catastrophic Failure in SelectMostProving.");
            }

            return node.children[i].move;
        }

        private void SetProofAndDisproofNumbers(Node node) {
            if (node.expanded) {
                switch (node.type) {
                    case Type.AND:
                        node.proof = 0;
                        foreach (Node n in node.children) { node.proof += n.proof; }
                        node.disproof = int.MaxValue;
                        foreach (Node n in node.children) { node.disproof = Math.Min(node.disproof, n.disproof); }
                        break;
                    case Type.OR:
                        node.proof = int.MaxValue;
                        foreach (Node n in node.children) { node.proof = Math.Min(node.proof, n.proof); }
                        node.disproof = 0;
                        foreach (Node n in node.children) { node.disproof += n.disproof; }
                        break;
                }
            } else if (node.evaluated) {
                if (node.value == Value.True) {
                    node.proof = int.MaxValue;
                    node.disproof = 0;
                } else if (node.value == Value.False) {
                    node.proof = 0;
                    node.disproof = int.MaxValue;
                } else if (node.value == Value.Unknown) {
                    node.proof = 1;
                    node.disproof = 1;
                }
            }
        }

        private void DevelopNode(Node node) {
            var moves = GameState.getFilterValidMoves(node.state, node.state.nextTurn);
            foreach (var move in moves) {
                Node temp = new Node();
                temp.move = move;
                temp.state = GameState.getTransformedBoard(node.state, move & 7, move >> 3);
                statesVisited++;
                temp.parent = node;
                temp.depth = node.depth + 1;
                node.children.Add(temp);
            }
            if (node.depth + 1 > depth) depth = node.depth + 1;

            foreach (var n in node.children) {
                Evaluate(n);
                SetProofAndDisproofNumbers(n);
            }
            if (node.value == Value.Unknown) {
                node.expanded = true;
            }
        }

        private bool ResourcesAvailable() {
            if (timer.ElapsedMilliseconds < timeLimit) {
                return true;
            }
            return false;
        }

        private void UpdateAncestors(Node node) {
            while (node != null) {
                SetProofAndDisproofNumbers(node);
                node = node.parent;
            }
        }

        private void Evaluate(Node node) {
            int winner = GameState.getWinner(node.state);
            if (winner == goalWinner) node.value = Value.True;
            else if (winner == (goalWinner ^ 3)) node.value = Value.False;
            else node.value = Value.Unknown;
            node.evaluated = true;
            if (node.state.nextTurn == goalWinner) {
                node.type = Type.AND;
            } else {
                node.type = Type.OR;
            }
        }

        public void setConfiguration(AIConfiguration config) {
            timeLimit = config.maxTime * 1000;
        }

        public GameStats getStats() {
            return stats;
        }
    }
}
