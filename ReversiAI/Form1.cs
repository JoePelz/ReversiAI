using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReversiAI {

    public partial class Form1 : Form {
        public enum Player { Human, Random, Max }
        public string[] players = new string[] { "Human", "AI Random", "AI Maximize" };
        GameState state;
        public event EventHandler nextTurn;
        private bool humanTurn;

        public Form1() {
            InitializeComponent();
            state = GameState.createInitialSetup();
            panel_Game.updateBoard(state);
            panel_Game.eClicked += gameClick;
            lbl_Overlay.BackColor = Color.FromArgb(192, 160, 192, 160);
            updateUI();
            nextTurn += Form1_nextTurn;
            combo_p1.Items.AddRange(players);
            combo_p2.Items.AddRange(players);
            combo_p1.SelectedIndex = 0;
            combo_p2.SelectedIndex = 1;
        }

        private void Form1_nextTurn(object sender, EventArgs e) {
            humanTurn = false;
            byte choice = 255;
            if (state.nextTurn == 1) {
                //player 1's turn (black)
                switch ((Player)combo_p1.SelectedIndex) {
                    case Player.Human:
                        humanTurn = true;
                        break;
                    case Player.Random:
                        choice = AIRandom(state);
                        break;
                    case Player.Max:
                        choice = AIMax(state);
                        break;
                }
            } else {
                //player 2's turn (white)
                switch ((Player)combo_p2.SelectedIndex) {
                    case Player.Human:
                        humanTurn = true;
                        break;
                    case Player.Random:
                        choice = AIRandom(state);
                        break;
                    case Player.Max:
                        choice = AIMax(state);
                        break;
                }
            }
            if (choice != 255) {
                takeTurn(choice & 7, choice >> 3);
            }
        }

        private byte AIRandom(GameState s) {
            byte[] moves = GameState.getValidMoves(s, s.nextTurn);
            Random rng = new Random();
            int options;
            options = moves.Count((b) => { return b > 0; });
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    if (rng.Next(options) == 0) {
                        return i;
                    } else {
                        options--;
                    }
                }
            }
            return 255;
        }

        private byte AIMax(GameState s) {
            byte[] moves = GameState.getValidMoves(s, s.nextTurn);
            byte best = 255;
            int tempCount, bestTurns = 0;
            GameState temp;
            for (byte i = 0; i < 64; i++) {
                if (moves[i] > 0) {
                    temp = GameState.getTransformedBoard(s, moves[i] & 7, moves[i] >> 3);
                    tempCount = temp.squares.Count((b) => { return b == s.nextTurn; });
                    if (tempCount > bestTurns) {
                        best = i;
                        bestTurns = tempCount;
                    }
                }
            }

            return best;
        }

        private void takeTurn(int x, int y) {
            if (!GameState.validMove(state, state.nextTurn, x, y)) return;
            
            state = GameState.getTransformedBoard(state, x, y);

            panel_Game.updateBoard(state);
            updateUI();

            //Check for game over
            int winner = getWinner(state);
            if (winner == 3) {
                lbl_Overlay.Text = "Tie Game!";
                lbl_Overlay.Visible = true;
            } else if (winner != 0) {
                lbl_Overlay.Text = "Player " + winner + "\nWINS!";
                lbl_Overlay.Visible = true;
            }
            //Trigger turn change
            endTurn();
        }

        private void gameClick(object sender, GamePanel.BoardClick e) {
            if (!humanTurn) return;
            takeTurn(e.x, e.y);
        }

        private void endTurn() {
            EventHandler temp = nextTurn;
            if (temp != null) {
                temp(this, EventArgs.Empty);
            }
        }

        private int getWinner(GameState state) {
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

        private void panel_nextPlayer_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Brush temp = new SolidBrush(panel_nextPlayer.ForeColor);
            int size = Math.Min(panel_nextPlayer.Width, panel_nextPlayer.Height);
            size = size * 8 / 10;
            g.FillEllipse(temp, panel_nextPlayer.Width / 2 - size / 2, panel_nextPlayer.Height / 2 - size / 2, size, size);
        }

        private void updateUI() {
            if (state.nextTurn == 1) {
                panel_nextPlayer.ForeColor = Color.Black;
            } else {
                panel_nextPlayer.ForeColor = Color.White;
            }
            panel_nextPlayer.Invalidate();

            int black_counter = 0, white_counter = 0;
            for (int i = 0; i < 64; i++) {
                if (state.squares[i] == 1) black_counter++;
                else if (state.squares[i] == 2) white_counter++;
            }

            lbl_BlackCounter.Text = black_counter.ToString();
            lbl_WhiteCounter.Text = white_counter.ToString();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            
            int size = lblCurrentPlayer.Width;
            lblCurrentPlayer.Font = new Font(lblCurrentPlayer.Font.Name, size / 10,
                lblCurrentPlayer.Font.Style, lblCurrentPlayer.Font.Unit);

            size = lbl_BlackCounter.Width;
            lbl_BlackCounter.Font = new Font(lbl_BlackCounter.Font.Name, size * 2 / 5,
                lbl_BlackCounter.Font.Style, lbl_BlackCounter.Font.Unit);
            lbl_WhiteCounter.Font = new Font(lbl_WhiteCounter.Font.Name, size * 2 / 5,
                lbl_WhiteCounter.Font.Style, lbl_WhiteCounter.Font.Unit);
            lbl_Black.Font = new Font(lbl_Black.Font.Name, size * 2 / 5,
                lbl_Black.Font.Style, lbl_Black.Font.Unit);
            lbl_White.Font = new Font(lbl_White.Font.Name, size * 2 / 5,
                lbl_White.Font.Style, lbl_White.Font.Unit);


            lbl_P1.Font = new Font(lbl_P1.Font.Name, size * 2 / 5,
                lbl_P1.Font.Style, lbl_P1.Font.Unit);
            lbl_P2.Font = new Font(lbl_P2.Font.Name, size * 2 / 5,
                lbl_P2.Font.Style, lbl_P2.Font.Unit);
            combo_p1.Font = new Font(combo_p1.Font.Name, size * 2 / 6,
                combo_p1.Font.Style, combo_p1.Font.Unit);
            combo_p2.Font = new Font(combo_p2.Font.Name, size * 2 / 6,
                combo_p2.Font.Style, combo_p2.Font.Unit);


            lbl_Restart.Font = new Font(lbl_Restart.Font.Name, size / 2,
                lbl_Restart.Font.Style, lbl_Restart.Font.Unit);

            size = Math.Min(panel_Game.Width, panel_Game.Height);
            lbl_Overlay.Font = new Font(lbl_Overlay.Font.Name, size / 6,
                lbl_Overlay.Font.Style, lbl_Overlay.Font.Unit);

        }

        private void lbl_Overlay_Click(object sender, EventArgs e) {
            if (lbl_Overlay.Text.Equals("Start Game")) {
                lbl_Overlay.Visible = false;
                //Trigger turn change
                EventHandler temp = nextTurn;
                if (temp != null) {
                    temp(this, EventArgs.Empty);
                }
            }
        }

        private void lbl_Restart_MouseEnter(object sender, EventArgs e) {
            lbl_Restart.BackColor = Color.OliveDrab;
        }

        private void lbl_Restart_MouseLeave(object sender, EventArgs e) {
            lbl_Restart.BackColor = Color.Olive;
        }

        private void lbl_Restart_Click(object sender, EventArgs e) {
            lbl_Overlay.Text = "Start Game";
            lbl_Overlay.Visible = true;
            state = GameState.createInitialSetup();
            panel_Game.updateBoard(state);
            updateUI();
        }
    }
}
