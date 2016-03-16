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
        private Controller controller;
        public delegate void GameState_InvokeDelegate(GameState state);

        public Form1() {
            InitializeComponent();
            panel_Game.eClicked += eGameClick;
            lbl_Overlay.BackColor = Color.FromArgb(192, 160, 192, 160);
            updateUI(GameState.createInitialSetup());
            combo_p1.Items.AddRange(Controller.players);
            combo_p2.Items.AddRange(Controller.players);
            combo_p1.SelectedIndex = 0;
            combo_p2.SelectedIndex = 0;
            controller = new Controller(this);
        }

        private delegate void setWinnerHandler(int winner);
        public void setWinner(int winner) {
            if (InvokeRequired) {
                setWinnerHandler h = new setWinnerHandler(setWinner);
                Invoke(h, winner);
                return;
            }

            if (winner == 3) {
                lbl_Overlay.Text = "Tie Game!";
                lbl_Overlay.Visible = true;
            } else if (winner != 0) {
                lbl_Overlay.Text = "Player " + winner + "\nWINS!";
                lbl_Overlay.Visible = true;
            }
        }

        private void eGameClick(object sender, GamePanel.BoardClick e) {
            if (controller.HumanTurn) {
                controller.applyMove(e.x, e.y);
            }
        }

        public Controller.Player getPlayerSelection(int player) {
            if (player == 1) {
                return (Controller.Player)combo_p1.SelectedIndex;
            }
            return (Controller.Player)combo_p2.SelectedIndex;
        }

        private void panel_nextPlayer_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Brush temp = new SolidBrush(panel_nextPlayer.ForeColor);
            int size = Math.Min(panel_nextPlayer.Width, panel_nextPlayer.Height);
            size = size * 8 / 10;
            g.FillEllipse(temp, panel_nextPlayer.Width / 2 - size / 2, panel_nextPlayer.Height / 2 - size / 2, size, size);
        }

        private delegate void updateUIHandler(GameState state);
        public void updateUI(GameState state) {
            if (InvokeRequired) {
                updateUIHandler h = new updateUIHandler(updateUI);
                Invoke(h, state);
                return;
            }


            panel_Game.updateBoard(state);

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

            lbl_Batch.Font = new Font(lbl_Batch.Font.Name, size * 3 / 5,
                lbl_Batch.Font.Style, lbl_Batch.Font.Unit);
            num_batch.Font = new Font(num_batch.Font.Name, size * 3 / 6,
                num_batch.Font.Style, num_batch.Font.Unit);

            lbl_Restart.Font = new Font(lbl_Restart.Font.Name, size / 2,
                lbl_Restart.Font.Style, lbl_Restart.Font.Unit);

            size = Math.Min(panel_Game.Width, panel_Game.Height);
            lbl_Overlay.Font = new Font(lbl_Overlay.Font.Name, size / 6,
                lbl_Overlay.Font.Style, lbl_Overlay.Font.Unit);

        }

        private void lbl_Overlay_Click(object sender, EventArgs e) {
            if (lbl_Overlay.Text.Equals("Start Game")) {
                controller.startGame();
                lbl_Overlay.Visible = false;
                combo_p1.Enabled = false;
                combo_p2.Enabled = false;
            }
        }

        internal void batchComplete(int[] batchResults) {
            MessageBox.Show("Batch Results:"
                + "\nError cases: " + batchResults[0]
                + "\nPlayer 1 wins: " + batchResults[1]
                + "\nPlayer 2 wins: " + batchResults[2]
                + "\nTie games: " + batchResults[3], "Batch Complete!");
        }

        private void lbl_Restart_MouseEnter(object sender, EventArgs e) {
            (sender as Label).BackColor = Color.OliveDrab;
        }

        private void lbl_Restart_MouseLeave(object sender, EventArgs e) {
            (sender as Label).BackColor = Color.Olive;
        }

        private void lbl_Restart_Click(object sender, EventArgs e) {
            lbl_Overlay.Text = "Start Game";
            lbl_Overlay.Visible = true;
            combo_p1.Enabled = true;
            combo_p2.Enabled = true;
            controller.resetGame();
        }

        private void lbl_Batch_Click(object sender, EventArgs e) {
            int numGames = (int)num_batch.Value;
            controller.doBatch(numGames);
        }
    }
}
