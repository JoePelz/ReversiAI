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
        public IReversiAI player1, player2;
        public GameState state;
        public event EventHandler nextTurn;
        private bool humanTurn;
        private AIRunner worker;
        private Thread workerThread;

        public delegate void InvokeDelegate(int x, int y);

        public Form1() {
            InitializeComponent();
            state = GameState.createInitialSetup();
            panel_Game.updateBoard(state);
            panel_Game.eClicked += eGameClick;
            lbl_Overlay.BackColor = Color.FromArgb(192, 160, 192, 160);
            updateUI();
            nextTurn += Form1_nextTurn;
            combo_p1.Items.AddRange(players);
            combo_p2.Items.AddRange(players);
            combo_p1.SelectedIndex = 0;
            combo_p2.SelectedIndex = 1;

            worker = new AIRunner(this);
            workerThread = new Thread(worker.ThreadRun);
            workerThread.Start();
        }

        private void Form1_nextTurn(object sender, EventArgs e) {
            humanTurn = false;
            /*
            byte choice = 255;
            */
            if (state.nextTurn == 1) {
                //player 1's turn (black)
                if ((Player)combo_p1.SelectedIndex == Player.Human) {
                    humanTurn = true;
                }
                /*
                if (player1 != null) {
                    choice = player1.getNextMove(state);
                }
                */
            } else {
                //player 2's turn (white)
                if ((Player)combo_p2.SelectedIndex == Player.Human) {
                    humanTurn = true;
                }
                /*
                if (player2 != null) {
                    choice = player2.getNextMove(state);
                }
                */
            }
            /*
            if (choice != 255) {
                takeTurn(choice & 7, choice >> 3);
            }
            */
            lock(worker) {
                Monitor.Pulse(worker);
            }
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

        public void turnTaken(byte index) {
            BeginInvoke(new InvokeDelegate(takeTurn), index & 7, index >> 3);
        }

        private void eGameClick(object sender, GamePanel.BoardClick e) {
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
                combo_p1.Enabled = false;
                combo_p2.Enabled = false;
                switch ((Player)combo_p1.SelectedIndex) {
                    case Player.Human:
                        player1 = null;
                        break;
                    case Player.Random:
                        player1 = new AIRandom();
                        break;
                    case Player.Max:
                        player1 = new AIMaximize();
                        break;
                }
                switch ((Player)combo_p2.SelectedIndex) {
                    case Player.Human:
                        player2 = null;
                        break;
                    case Player.Random:
                        player2 = new AIRandom();
                        break;
                    case Player.Max:
                        player2 = new AIMaximize();
                        break;
                }
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
            combo_p1.Enabled = true;
            combo_p2.Enabled = true;
            state = GameState.createInitialSetup();
            panel_Game.updateBoard(state);
            updateUI();
        }

        internal IReversiAI getPlayer() {
            if (state.nextTurn == 1) {
                return player1;
            } else {
                return player2;
            }
        }

        internal GameState getState() {
            return state;
        }
    }

    public class AIRunner {
        Form1 parent;

        public AIRunner(Form1 owner) {
            parent = owner;
        }
        public void ThreadRun() {
            byte move;
            while (true) {
                lock(this) {
                    Monitor.Wait(this);
                    IReversiAI engine = parent.getPlayer();
                    if (engine != null) {
                        move = engine.getNextMove(parent.getState());
                        if (move != 255) {
                            parent.turnTaken(move);
                        }
                    }
                }
            }
        }
    }
}
