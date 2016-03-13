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

            #region Validate
            //validate the click is okay.
            //Don't allow play if square is already taken.
            if (state.squares[index] > 0) {
                return;
            }
            bool any = false;
            for (int dx = -1; dx < 2; dx++) {
                for (int dy = -8; dy <= 8; dy += 8) {
                    //if (dx == 0 && dy == 0) continue;
                    if (index + dx + dy >= 0 && index + dx + dy < 64 && state.squares[index + dx + dy] == (state.nextTurn ^ 3)) {
                        any = true;
                        break;
                    }
                }
            }
            if (!any) return;
            #endregion Validate

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
            state.nextTurn = (byte)(state.nextTurn ^ 3);
            panel1.updateBoard(state);
        }
    }
}
