namespace ReversiAI {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel_Game = new ReversiAI.GamePanel(this.components);
            this.lbl_Overlay = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurrentPlayer = new System.Windows.Forms.Label();
            this.panel_nextPlayer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Black = new System.Windows.Forms.Label();
            this.lbl_White = new System.Windows.Forms.Label();
            this.lbl_BlackCounter = new System.Windows.Forms.Label();
            this.lbl_WhiteCounter = new System.Windows.Forms.Label();
            this.lbl_Restart = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.combo_p1 = new System.Windows.Forms.ComboBox();
            this.combo_p2 = new System.Windows.Forms.ComboBox();
            this.lbl_P1 = new System.Windows.Forms.Label();
            this.lbl_P2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel_Game.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel_Game);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(759, 405);
            this.splitContainer1.SplitterDistance = 457;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // panel_Game
            // 
            this.panel_Game.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.panel_Game.Controls.Add(this.lbl_Overlay);
            this.panel_Game.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Game.Location = new System.Drawing.Point(0, 0);
            this.panel_Game.Name = "panel_Game";
            this.panel_Game.Size = new System.Drawing.Size(457, 405);
            this.panel_Game.TabIndex = 0;
            // 
            // lbl_Overlay
            // 
            this.lbl_Overlay.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Overlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Overlay.Font = new System.Drawing.Font("Script MT Bold", 60F);
            this.lbl_Overlay.Location = new System.Drawing.Point(0, 0);
            this.lbl_Overlay.Name = "lbl_Overlay";
            this.lbl_Overlay.Size = new System.Drawing.Size(457, 405);
            this.lbl_Overlay.TabIndex = 0;
            this.lbl_Overlay.Text = "Start Game";
            this.lbl_Overlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Overlay.Click += new System.EventHandler(this.lbl_Overlay_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Restart, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 405);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Green;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentPlayer, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel_nextPlayer, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(292, 95);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblCurrentPlayer
            // 
            this.lblCurrentPlayer.AutoSize = true;
            this.lblCurrentPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPlayer.Font = new System.Drawing.Font("Script MT Bold", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPlayer.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblCurrentPlayer.Location = new System.Drawing.Point(76, 0);
            this.lblCurrentPlayer.Name = "lblCurrentPlayer";
            this.lblCurrentPlayer.Size = new System.Drawing.Size(213, 95);
            this.lblCurrentPlayer.TabIndex = 0;
            this.lblCurrentPlayer.Text = "Next Up";
            this.lblCurrentPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_nextPlayer
            // 
            this.panel_nextPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_nextPlayer.Location = new System.Drawing.Point(3, 3);
            this.panel_nextPlayer.Name = "panel_nextPlayer";
            this.panel_nextPlayer.Size = new System.Drawing.Size(67, 89);
            this.panel_nextPlayer.TabIndex = 1;
            this.panel_nextPlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_nextPlayer_Paint);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_Black, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_White, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_BlackCounter, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_WhiteCounter, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 104);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(292, 42);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // lbl_Black
            // 
            this.lbl_Black.AutoSize = true;
            this.lbl_Black.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Black.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Black.Location = new System.Drawing.Point(46, 0);
            this.lbl_Black.Name = "lbl_Black";
            this.lbl_Black.Size = new System.Drawing.Size(96, 42);
            this.lbl_Black.TabIndex = 0;
            this.lbl_Black.Text = "Black";
            this.lbl_Black.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_White
            // 
            this.lbl_White.AutoSize = true;
            this.lbl_White.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_White.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_White.Location = new System.Drawing.Point(148, 0);
            this.lbl_White.Name = "lbl_White";
            this.lbl_White.Size = new System.Drawing.Size(96, 42);
            this.lbl_White.TabIndex = 1;
            this.lbl_White.Text = "White";
            this.lbl_White.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_BlackCounter
            // 
            this.lbl_BlackCounter.BackColor = System.Drawing.Color.Green;
            this.lbl_BlackCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_BlackCounter.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BlackCounter.ForeColor = System.Drawing.Color.Black;
            this.lbl_BlackCounter.Location = new System.Drawing.Point(3, 0);
            this.lbl_BlackCounter.Name = "lbl_BlackCounter";
            this.lbl_BlackCounter.Size = new System.Drawing.Size(37, 42);
            this.lbl_BlackCounter.TabIndex = 2;
            this.lbl_BlackCounter.Text = "2";
            this.lbl_BlackCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WhiteCounter
            // 
            this.lbl_WhiteCounter.BackColor = System.Drawing.Color.Green;
            this.lbl_WhiteCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_WhiteCounter.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WhiteCounter.ForeColor = System.Drawing.Color.White;
            this.lbl_WhiteCounter.Location = new System.Drawing.Point(250, 0);
            this.lbl_WhiteCounter.Name = "lbl_WhiteCounter";
            this.lbl_WhiteCounter.Size = new System.Drawing.Size(39, 42);
            this.lbl_WhiteCounter.TabIndex = 3;
            this.lbl_WhiteCounter.Text = "2";
            this.lbl_WhiteCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Restart
            // 
            this.lbl_Restart.AutoSize = true;
            this.lbl_Restart.BackColor = System.Drawing.Color.Olive;
            this.lbl_Restart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Restart.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_Restart.Location = new System.Drawing.Point(3, 149);
            this.lbl_Restart.Name = "lbl_Restart";
            this.lbl_Restart.Size = new System.Drawing.Size(292, 48);
            this.lbl_Restart.TabIndex = 3;
            this.lbl_Restart.Text = "Restart Game";
            this.lbl_Restart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Restart.Click += new System.EventHandler(this.lbl_Restart_Click);
            this.lbl_Restart.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_Restart.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.combo_p1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.combo_p2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_P1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_P2, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 200);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(292, 202);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // combo_p1
            // 
            this.combo_p1.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "AI Random",
            "AI Max Captures"});
            this.combo_p1.Dock = System.Windows.Forms.DockStyle.Top;
            this.combo_p1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_p1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_p1.FormattingEnabled = true;
            this.combo_p1.Location = new System.Drawing.Point(3, 43);
            this.combo_p1.Name = "combo_p1";
            this.combo_p1.Size = new System.Drawing.Size(140, 31);
            this.combo_p1.TabIndex = 8;
            // 
            // combo_p2
            // 
            this.combo_p2.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "AI Random",
            "AI Max Captures"});
            this.combo_p2.Dock = System.Windows.Forms.DockStyle.Top;
            this.combo_p2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_p2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_p2.FormattingEnabled = true;
            this.combo_p2.Location = new System.Drawing.Point(149, 43);
            this.combo_p2.Name = "combo_p2";
            this.combo_p2.Size = new System.Drawing.Size(140, 31);
            this.combo_p2.TabIndex = 7;
            // 
            // lbl_P1
            // 
            this.lbl_P1.AutoSize = true;
            this.lbl_P1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_P1.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_P1.Location = new System.Drawing.Point(3, 16);
            this.lbl_P1.Name = "lbl_P1";
            this.lbl_P1.Size = new System.Drawing.Size(140, 24);
            this.lbl_P1.TabIndex = 9;
            this.lbl_P1.Text = "Player 1";
            this.lbl_P1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_P2
            // 
            this.lbl_P2.AutoSize = true;
            this.lbl_P2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_P2.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_P2.Location = new System.Drawing.Point(149, 16);
            this.lbl_P2.Name = "lbl_P2";
            this.lbl_P2.Size = new System.Drawing.Size(140, 24);
            this.lbl_P2.TabIndex = 10;
            this.lbl_P2.Text = "Player 2";
            this.lbl_P2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(759, 405);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Reversi";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel_Game.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private GamePanel panel_Game;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCurrentPlayer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel_nextPlayer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbl_Black;
        private System.Windows.Forms.Label lbl_White;
        private System.Windows.Forms.Label lbl_BlackCounter;
        private System.Windows.Forms.Label lbl_WhiteCounter;
        private System.Windows.Forms.Label lbl_Overlay;
        private System.Windows.Forms.Label lbl_Restart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox combo_p1;
        private System.Windows.Forms.ComboBox combo_p2;
        private System.Windows.Forms.Label lbl_P1;
        private System.Windows.Forms.Label lbl_P2;
    }
}

