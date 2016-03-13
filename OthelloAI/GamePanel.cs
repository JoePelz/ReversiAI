using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloAI {
    public partial class GamePanel : Panel {
        Rectangle boardRect;
        Point[] locations;
        int size;
        Size squareSize;
        GameState oldState = GameState.createInitialSetup();
        public event EventHandler<BoardClick> eClicked;

        public GamePanel() {
            InitializeComponent();
            updateRect();
        }

        public GamePanel(IContainer container) {
            container.Add(this);

            InitializeComponent();
            updateRect();
        }

        private void updateRect() {
            size = Math.Min(ClientRectangle.Width, ClientRectangle.Height);
            boardRect = new Rectangle(
                (ClientRectangle.Width >> 1) - (size >> 1) + ClientRectangle.X,
                (ClientRectangle.Height >> 1) - (size >> 1) + ClientRectangle.Y, 
                size, size);

            locations = new Point[64];
            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    locations[x | y << 3] = new Point((size * x >> 3) + boardRect.X, (size * y >> 3) + boardRect.Y);
                }
            }
            squareSize = new Size(size/8, size/8);
        }

        public class BoardClick : EventArgs {
            public int x;
            public int y;
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            updateRect();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Green, boardRect);
            int offset;
            for (int i = 1; i < 8; i++) {
                offset = size * i >> 3;
                g.DrawLine(Pens.LimeGreen, boardRect.Left, boardRect.Top + offset, boardRect.Right, boardRect.Top + offset);
                g.DrawLine(Pens.LimeGreen, boardRect.Left + offset, boardRect.Top, boardRect.Left + offset, boardRect.Bottom);
            }
            for (int i = 0; i < 64; i++) {
                if (oldState.squares[i] == 1) {
                    g.FillEllipse(Brushes.Black, new Rectangle(locations[i], squareSize));
                } else if (oldState.squares[i] == 2) {
                    g.FillEllipse(Brushes.White, new Rectangle(locations[i], squareSize));
                }
            }
        }

        public void updateBoard(GameState newState) {
            Graphics g = CreateGraphics();
            for(int i = 0; i < 64; i++) {
                if (newState.squares[i] != oldState.squares[i]) {
                    if (newState.squares[i] == 1) {
                        g.FillEllipse(Brushes.Black, new Rectangle(locations[i], squareSize));
                    } else if (newState.squares[i] == 2) {
                        g.FillEllipse(Brushes.White, new Rectangle(locations[i], squareSize));
                    }
                    oldState.squares[i] = newState.squares[i];
                }
            }
            g.Dispose();
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);

            int x = e.X - boardRect.X;
            x = x * 8 / size;
            int y = e.Y - boardRect.Y;
            y = y * 8 / size;

            BoardClick args = new BoardClick();
            args.x = x;
            args.y = y;

            EventHandler<BoardClick> handler = eClicked;
            if (handler != null) {
                handler(this, args);
            }
        }
    }
}
