using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class Controller {
        public enum Player { Human, Random, Max, Min, First, Tree2, Tree3, Tree4, Tree5, Tree6, Tree7 }
        public static string[] players = new string[] {
            "Human", "AI Random", "AI Maximize", "AI Minimize", "AI First",
            "2-Depth Tree", "3-Depth Tree", "4-Depth Tree",
            "5-Depth Tree", "6-Depth Tree", "7-Depth Tree"};
        public static IReversiAI getAI(Player sel) {
            switch (sel) {
                case Player.Max: return new AIMaximize();
                case Player.Min: return new AIMinimize();
                case Player.Random: return new AIRandom();
                case Player.First: return new AIFirst();
                case Player.Tree2: return new AITreeAll(2);
                case Player.Tree3: return new AITreeAll(3);
                case Player.Tree4: return new AITreeAll(4);
                case Player.Tree5: return new AITreeAll(5);
                case Player.Tree6: return new AITreeAll(6);
                case Player.Tree7: return new AITreeAll(7);
                default: return null;
            }
        }


        private IReversiAI player1, player2;
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

            //TimeProfiler tp = new TimeProfiler();
            //tp.runTests();
        }

        /// <summary>
        /// startGame creates a new AI for each player 
        /// and begins the first turn.
        /// </summary>
        public void startGame() {
            player1 = getAI(gui.getPlayerSelection(1));
            player2 = getAI(gui.getPlayerSelection(2));
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
            int winner = getWinner(state);
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

        public static int applyMoveBatch(ref GameState state, int x, int y) {
            state = GameState.getTransformedBoard(state, x, y);
            return getWinner(state);
        }

        public static int playGame(Controller ctrl, Player p1, Player p2) {
            int winner = 0;
            IReversiAI ai1 = getAI(p1);
            IReversiAI ai2 = getAI(p2);
            if (ai1 == null || ai2 == null) {
                return winner;
            }

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
                gamesToPlay[i] = Task.Factory.StartNew(() => { playGame(this, p1, p2); });
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

        private void mergeStats(Dictionary<string, double> into, Dictionary<string, double> from) {
            foreach (var key in from.Keys) {
                if (into.ContainsKey(key)) {
                    into[key] = (into[key] * (batchGamesComplete - 1) + from[key]) / batchGamesComplete;
                } else {
                    into[key] = from[key];
                }
            }
        }

        /// <summary>
        /// Determine if a game is over and if so, who the winner is.
        /// </summary>
        /// <param name="state">The game board to check for a winner</param>
        /// <returns>return 0 if no winner, return 1 if player 1 won, 
        /// return 2 if player 2 won, return 3 if tie game.</returns>
        public static int getWinner(GameState state) {
            byte[] moves = GameState.getValidMoves(state, (byte)(state.nextTurn));
            if (moves.Any((v) => { return v != 0; })) {
                return 0;
            }
            int black_counter = 0, white_counter = 0;
            for (int i = 0; i < 64; i++) {
                if (state.squares[i] == 1) white_counter++;
                else if (state.squares[i] == 2) black_counter++;
            }
            if (white_counter == black_counter) return 3;
            return white_counter > black_counter ? 1 : 2;
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
