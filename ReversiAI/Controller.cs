using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class Controller {
        public enum Player { Human, Random, Max, Min, First, BestLeaf, Negamax, ProofNumber }
        public static string[] players = new string[] {
            "Human", "AI Random", "AI Maximize", "AI Minimize", "AI First",
            "Best Leaf", "NegaMax", "ProofNumber"};
        public static IReversiAI getAI(AIConfiguration cfg) {
            switch (cfg.AI) {
                case Player.Max: return new AIMaximize();
                case Player.Min: return new AIMinimize();
                case Player.Random: return new AIRandom();
                case Player.First: return new AIFirst();
                case Player.BestLeaf: return new AITreeAll();
                case Player.Negamax: return new AINegaMax();
                case Player.ProofNumber: return new AIProofNumber();
                default: return null;
            }
        }


        private IReversiAI player1, player2;
        public AIConfiguration cfg1, cfg2;
        private GameState state;
        private Form1 gui;
        private bool humanTurn;

        private int batchGamesComplete;
        private int[] batchResults;
        GameStats batchStatsP1;
        GameStats batchStatsP2;

        public bool HumanTurn { get { return humanTurn; } }

        public Controller(Form1 owner) {
            state = GameState.createInitialSetup();
            gui = owner;
            cfg1 = new AIConfiguration();
            cfg1.player = 1;
            cfg2 = new AIConfiguration();
            cfg2.player = 2;

            //TimeProfiler tp = new TimeProfiler();
            //tp.runTests();
        }

        /// <summary>
        /// startGame creates a new AI for each player 
        /// and begins the first turn.
        /// </summary>
        public void startGame() {
            player1 = getAI(cfg1);
            if (player1 != null) player1.setConfiguration(cfg1);
            player2 = getAI(cfg2);
            if (player2 != null) player2.setConfiguration(cfg2);
            beginTurn();
        }

        /// <summary>
        /// Resets the game to starting conditions. 
        /// (Threading bug: Asynchronous AI tasks are not stopped, 
        /// and can finish (i.e. make a move) after pressing reset)
        /// </summary>
        public void resetGame() {
            state = GameState.createInitialSetup();
            gui.updateUI(state);
        }

        /// <summary>
        /// Starts a new turn and, if necessary, invoke the AI to
        /// determine a new move.
        /// </summary>
        private void beginTurn() {
            humanTurn = getPlayer() == null;

            if (!humanTurn) {
                AIHandler ai = new AIHandler(getAIMove);
                AsyncCallback callback = new AsyncCallback(AIMoveCallback);
                ai.BeginInvoke(state, callback, ai);
            }
        }
        
        /// <summary>
        /// Run the current player's AI to generate the next move to make for the current player.
        /// </summary>
        /// <param name="state">The current state of the game board from which to make a move.</param>
        /// <returns>The index to play in, [0..63], or 255 if no move is identified.</returns>
        private byte getAIMove(GameState state) {
            byte move = 255;
            IReversiAI engine = getPlayer();
            if (engine != null) {
                move = engine.getNextMove(state);
            }
            if (move == 255) {
                System.Windows.Forms.MessageBox.Show("Error getting next move. Please restart the game.");
            }
            return move;
        }
        private delegate byte AIHandler(GameState state);
        /// <summary>
        /// Callback from running the AI via the thread pool. This is 
        /// executed when the thread has determined the next move 
        /// to make.  If the move is valid (0..63), the particular 
        /// move is forwarded to the applyMove function 
        /// as (x, y) coordinates.
        /// </summary>
        /// <param name="ar"></param>
        private void AIMoveCallback(IAsyncResult ar) {
            AIHandler ai = (AIHandler)ar.AsyncState;
            byte move = ai.EndInvoke(ar);
            if (move < 64) 
                applyMove(move & 7, move >> 3);
        }

        /// <summary>
        /// Places a token for the current player at the given co-
        /// -ordinates (if valid) and updates the board based on Reversi rules.
        /// If the move is invalid, nothing will happen
        /// and the game will need to be restarted.
        /// If the move results in a winner, the gui is updated to
        /// reflect the victory. If not, the next turn is begun.
        /// </summary>
        /// <param name="x">The x coordinate [0..7] to play at.</param>
        /// <param name="y">The y coordinate [0..7] to play at.</param>
        public void applyMove(int x, int y) {
            if (!GameState.validMove(state, state.nextTurn, x, y)) return;

            state = GameState.getTransformedBoard(state, x, y);
            //Update UI with changes
            gui.updateUI(state);

            //Check for game over
            int winner = GameState.getWinner(state);
            //Update UI to show winner
            if (winner > 0) {
                gui.setWinner(winner);
            } else {
                beginTurn();
            }
        }

        public void printStats(int player) {
            if (player == 1) {
                if (player1 == null) return;
                gui.displayStats(player1.getStats(), "Player 1: ", "Game Stats (thus far)");
            } else {
                if (player2 == null) return;
                gui.displayStats(player2.getStats(), "Player 2: ", "Game Stats (thus far)");
            }
        }

        public void saveConfig(AIConfiguration ai) {
            if (ai.player == 1) {
                cfg1 = ai;
                gui.setAI(cfg1);
            } else if (ai.player == 2) {
                cfg2 = ai;
                gui.setAI(cfg2);
            } else {
                System.Windows.Forms.MessageBox.Show("Error in Controller::saveConfig.");
            }
        }

        public static int applyMoveBatch(ref GameState state, int x, int y) {
            state = GameState.getTransformedBoard(state, x, y);
            return GameState.getWinner(state);
        }

        public static int playGame(Controller ctrl, AIConfiguration p1, AIConfiguration p2) {
            int winner = 0;
            IReversiAI ai1 = getAI(p1);
            IReversiAI ai2 = getAI(p2);
            if (ai1 == null || ai2 == null) {
                return winner;
            }
            ai1.setConfiguration(p1);
            ai2.setConfiguration(p2);

            byte move;
            GameState state = GameState.createInitialSetup();
            while(winner == 0) {
                if (state.nextTurn == 1)
                    move = ai1.getNextMove(state);
                else
                    move = ai2.getNextMove(state);
                winner = applyMoveBatch(ref state, move & 7, move >> 3);
            }
            ctrl.updateBatchResults(winner, ai1, ai2);
            return winner;
        }

        public void doBatch(int numGames) {
            batchGamesComplete = 0;
            batchResults = new int[4];
            batchStatsP1 = new GameStats();
            batchStatsP2 = new GameStats();
            Array.Clear(batchResults, 0, 4);
            Task[] gamesToPlay = new Task[numGames];
            Player p1 = gui.getPlayerSelection(1);
            Player p2 = gui.getPlayerSelection(2);
            for (int i = 0; i < numGames; i++) {
                gamesToPlay[i] = Task.Factory.StartNew(() => { playGame(this, cfg1, cfg2); });
            }
            Task.WaitAll(gamesToPlay);
            gui.batchComplete(batchResults, batchStatsP1, batchStatsP2);
        }

        public void updateBatchResults(int result, IReversiAI p1, IReversiAI p2) {
            lock(this) {
                batchGamesComplete++;
                batchResults[result]++;
                batchStatsP1.mergeStats(p1.getStats());
                batchStatsP2.mergeStats(p2.getStats());
            }
        }

        /// <summary>
        /// Get the ai representing the current player. 
        /// Null is returned if it is a human's turn.
        /// </summary>
        /// <returns>The AI for the current player, or null if it is a human's turn.</returns>
        private IReversiAI getPlayer() {
            return state.nextTurn == 1 ? player1 : player2;
        }
    }
}
