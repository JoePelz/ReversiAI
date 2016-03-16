﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversiAI {
    public class Controller {
        public enum Player { Human, Random, Max }
        public static string[] players = new string[] { "Human", "AI Random", "AI Maximize" };
        public bool HumanTurn { get { return humanTurn; } }

        private IReversiAI player1, player2;
        private GameState state;
        private Form1 gui;
        private bool humanTurn;

        public Controller(Form1 owner) {
            state = GameState.createInitialSetup();
            gui = owner;
        }

        /// <summary>
        /// startGame creates a new AI for each player 
        /// and begins the first turn.
        /// </summary>
        public void startGame() {
            player1 = null;
            player2 = null;
            switch (gui.getPlayerSelection(1)) {
                case Player.Max:
                    player1 = new AIMaximize();
                    break;
                case Player.Random:
                    player1 = new AIRandom();
                    break;
            }
            switch (gui.getPlayerSelection(2)) {
                case Player.Max:
                    player2 = new AIMaximize();
                    break;
                case Player.Random:
                    player2 = new AIRandom();
                    break;
            }
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

        /// <summary>
        /// Determine if a game is over and if so, who the winner is.
        /// </summary>
        /// <param name="state">The game board to check for a winner</param>
        /// <returns>0 if no winner, 1 if player 1, 2 if player 2, 3 if tie game.</returns>
        public static int getWinner(GameState state) {
            byte[] moves = GameState.getValidMoves(state, (byte)(state.nextTurn));
            if (moves.Any((v) => { return v != 0; })) {
                return 0;
            }
            int black_counter = 0, white_counter = 0;
            for (int i = 0; i < 64; i++) {
                if (state.squares[i] == 1) black_counter++;
                else if (state.squares[i] == 2) white_counter++;
            }
            if (black_counter == white_counter) return 3;
            return black_counter > white_counter ? 1 : 2;
        }
        
        /// <summary>
        /// Get the ai representing the current player. 
        /// Null is returned if it is a human's turn.
        /// </summary>
        /// <returns>The AI for the current player, or null if it is a human's turn.</returns>
        private IReversiAI getPlayer() {
            if (state.nextTurn == 1) {
                return player1;
            } else {
                return player2;
            }
        }

    }
}
