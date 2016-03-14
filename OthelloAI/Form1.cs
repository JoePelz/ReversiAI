using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloAI {
    public partial class Form1 : Form {
        GameState state;
        public Form1() {
            InitializeComponent();
            state = GameState.createInitialSetup();
            panel1.updateBoard(state);
            panel1.eClicked += gameClick;
        }

        private void gameClick(object sender, GamePanel.BoardClick e) {
            int index = e.x | e.y << 3;

            if (!GameState.validMove(state, state.nextTurn, e.x, e.y)) return;

            //update clicked square
            state.squares[index] = state.nextTurn;

            #region Cascade
            //cascade changes in all directions
            //left
            for (int dx = e.x - 1; dx >= 0; dx--) {
                if (state.squares[index - e.x + dx] == 0) {
                    break;
                }
                if (state.squares[index - e.x + dx] == state.nextTurn) {
                    while (++dx < e.x) {
                        state.squares[index - e.x + dx] = state.nextTurn;
                    }
                    break;
                }
            }
            //right
            for (int dx = e.x + 1; dx < 8; dx++) {
                if (state.squares[index - e.x + dx] == 0) {
                    break;
                }
                if (state.squares[index - e.x + dx] == state.nextTurn) {
                    while (--dx > e.x) {
                        state.squares[index - e.x + dx] = state.nextTurn;
                    }
                    break;
                }
            }
            //up
            for (int dy = e.y - 1; dy >= 0; dy--) {
                if (state.squares[index + (dy - e.y << 3)] == 0) {
                    break;
                }
                if (state.squares[index + (dy - e.y << 3)] == state.nextTurn) {
                    while (++dy < e.y) {
                        state.squares[index + (dy - e.y << 3)] = state.nextTurn;
                    }
                    break;
                }
            }
            //down
            for (int dy = e.y + 1; dy < 8; dy++) {
                if (state.squares[index + (dy - e.y << 3)] == 0) {
                    break;
                }
                if (state.squares[index + (dy - e.y << 3)] == state.nextTurn) {
                    while (--dy > e.y) {
                        state.squares[index + (dy - e.y << 3)] = state.nextTurn;
                    }
                    break;
                }
            }


            //upleft
            for (int dx = e.x - 1, dy = e.y - 1; dx >= 0 && dy >= 0; dx--, dy--) {
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == 0) {
                    break;
                }
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == state.nextTurn) {
                    while (++dy < e.y) {
                        ++dx;
                        state.squares[index + (dy - e.y << 3) + (dx - e.x)] = state.nextTurn;
                    }
                    break;
                }
            }
            //upright
            for (int dx = e.x + 1, dy = e.y - 1; dx < 8 && dy >= 0; dx++, dy--) {
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == 0) {
                    break;
                }
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == state.nextTurn) {
                    while (++dy < e.y) {
                        --dx;
                        state.squares[index + (dy - e.y << 3) + (dx - e.x)] = state.nextTurn;
                    }
                    break;
                }
            }
            //downright
            for (int dx = e.x + 1, dy = e.y + 1; dx < 8 && dy < 8; dx++, dy++) {
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == 0) {
                    break;
                }
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == state.nextTurn) {
                    while (--dy > e.y) {
                        --dx;
                        state.squares[index + (dy - e.y << 3) + (dx - e.x)] = state.nextTurn;
                    }
                    break;
                }
            }
            //downleft
            for (int dx = e.x - 1, dy = e.y + 1; dx >= 0 && dy < 8; dx--, dy++) {
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == 0) {
                    break;
                }
                if (state.squares[index + (dy - e.y << 3) + (dx - e.x)] == state.nextTurn) {
                    while (--dy > e.y) {
                        ++dx;
                        state.squares[index + (dy - e.y << 3) + (dx - e.x)] = state.nextTurn;
                    }
                    break;
                }
            }
            #endregion Cascade

            //switch players
            //check if the next player can actually go
            byte[] moves = GameState.getValidMoves(state, (byte)(state.nextTurn ^ 3));
            if (moves.Any((v) => { return v != 0; })) {
                state.nextTurn = (byte)(state.nextTurn ^ 3);
            }

            panel1.updateBoard(state);
            if (state.nextTurn == 1) {
                panel2.ForeColor = Color.Black;
            } else {
                panel2.ForeColor = Color.White;
            }
            panel2.Invalidate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Brush temp = new SolidBrush(panel2.ForeColor);
            int size = Math.Min(panel2.Width, panel2.Height);
            g.FillEllipse(temp, 20, 20, size-40, size-40);
        }
    }
}
