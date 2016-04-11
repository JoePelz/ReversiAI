namespace ReversiAI {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.layout_rightALL = new System.Windows.Forms.TableLayoutPanel();
            this.layout_NextUp = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurrentPlayer = new System.Windows.Forms.Label();
            this.panel_nextPlayer = new System.Windows.Forms.Panel();
            this.layout_Scores = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Black = new System.Windows.Forms.Label();
            this.lbl_White = new System.Windows.Forms.Label();
            this.lbl_WhiteCounter = new System.Windows.Forms.Label();
            this.lbl_BlackCounter = new System.Windows.Forms.Label();
            this.lbl_Restart = new System.Windows.Forms.Label();
            this.layout_Options = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_statsP2 = new System.Windows.Forms.Label();
            this.combo_p2 = new System.Windows.Forms.ComboBox();
            this.combo_p1 = new System.Windows.Forms.ComboBox();
            this.lbl_P1 = new System.Windows.Forms.Label();
            this.lbl_P2 = new System.Windows.Forms.Label();
            this.lbl_statsP1 = new System.Windows.Forms.Label();
            this.lbl_config_p1 = new System.Windows.Forms.Label();
            this.lbl_config_p2 = new System.Windows.Forms.Label();
            this.layout_Batch = new System.Windows.Forms.TableLayoutPanel();
            this.num_batch = new System.Windows.Forms.NumericUpDown();
            this.lbl_Batch = new System.Windows.Forms.Label();
            this.panel_Game = new ReversiAI.GamePanel(this.components);
            this.lbl_Overlay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.layout_rightALL.SuspendLayout();
            this.layout_NextUp.SuspendLayout();
            this.layout_Scores.SuspendLayout();
            this.layout_Options.SuspendLayout();
            this.layout_Batch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_batch)).BeginInit();
            this.panel_Game.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.layout_rightALL);
            this.splitContainer1.Size = new System.Drawing.Size(759, 457);
            this.splitContainer1.SplitterDistance = 457;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // layout_rightALL
            // 
            this.layout_rightALL.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.layout_rightALL.ColumnCount = 1;
            this.layout_rightALL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_rightALL.Controls.Add(this.layout_NextUp, 0, 0);
            this.layout_rightALL.Controls.Add(this.layout_Scores, 0, 1);
            this.layout_rightALL.Controls.Add(this.lbl_Restart, 0, 2);
            this.layout_rightALL.Controls.Add(this.layout_Options, 0, 3);
            this.layout_rightALL.Controls.Add(this.layout_Batch, 0, 4);
            this.layout_rightALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_rightALL.Location = new System.Drawing.Point(0, 0);
            this.layout_rightALL.Name = "layout_rightALL";
            this.layout_rightALL.RowCount = 5;
            this.layout_rightALL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layout_rightALL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout_rightALL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout_rightALL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout_rightALL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout_rightALL.Size = new System.Drawing.Size(298, 457);
            this.layout_rightALL.TabIndex = 0;
            // 
            // layout_NextUp
            // 
            this.layout_NextUp.BackColor = System.Drawing.Color.Green;
            this.layout_NextUp.ColumnCount = 2;
            this.layout_NextUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layout_NextUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.layout_NextUp.Controls.Add(this.lblCurrentPlayer, 1, 0);
            this.layout_NextUp.Controls.Add(this.panel_nextPlayer, 0, 0);
            this.layout_NextUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_NextUp.Location = new System.Drawing.Point(3, 3);
            this.layout_NextUp.Name = "layout_NextUp";
            this.layout_NextUp.RowCount = 1;
            this.layout_NextUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_NextUp.Size = new System.Drawing.Size(292, 85);
            this.layout_NextUp.TabIndex = 1;
            // 
            // lblCurrentPlayer
            // 
            this.lblCurrentPlayer.AutoSize = true;
            this.lblCurrentPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPlayer.Font = new System.Drawing.Font("Script MT Bold", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPlayer.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblCurrentPlayer.Location = new System.Drawing.Point(76, 0);
            this.lblCurrentPlayer.Name = "lblCurrentPlayer";
            this.lblCurrentPlayer.Size = new System.Drawing.Size(213, 85);
            this.lblCurrentPlayer.TabIndex = 0;
            this.lblCurrentPlayer.Text = "Next Up";
            this.lblCurrentPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_nextPlayer
            // 
            this.panel_nextPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_nextPlayer.Location = new System.Drawing.Point(3, 3);
            this.panel_nextPlayer.Name = "panel_nextPlayer";
            this.panel_nextPlayer.Size = new System.Drawing.Size(67, 79);
            this.panel_nextPlayer.TabIndex = 1;
            this.panel_nextPlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_nextPlayer_Paint);
            // 
            // layout_Scores
            // 
            this.layout_Scores.ColumnCount = 4;
            this.layout_Scores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.layout_Scores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.layout_Scores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.layout_Scores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.layout_Scores.Controls.Add(this.lbl_Black, 1, 0);
            this.layout_Scores.Controls.Add(this.lbl_White, 2, 0);
            this.layout_Scores.Controls.Add(this.lbl_WhiteCounter, 0, 0);
            this.layout_Scores.Controls.Add(this.lbl_BlackCounter, 3, 0);
            this.layout_Scores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_Scores.Location = new System.Drawing.Point(3, 94);
            this.layout_Scores.Name = "layout_Scores";
            this.layout_Scores.RowCount = 1;
            this.layout_Scores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_Scores.Size = new System.Drawing.Size(292, 39);
            this.layout_Scores.TabIndex = 2;
            // 
            // lbl_Black
            // 
            this.lbl_Black.AutoSize = true;
            this.lbl_Black.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Black.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Black.ForeColor = System.Drawing.Color.White;
            this.lbl_Black.Location = new System.Drawing.Point(46, 0);
            this.lbl_Black.Name = "lbl_Black";
            this.lbl_Black.Size = new System.Drawing.Size(96, 39);
            this.lbl_Black.TabIndex = 0;
            this.lbl_Black.Text = "White";
            this.lbl_Black.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_White
            // 
            this.lbl_White.AutoSize = true;
            this.lbl_White.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_White.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_White.ForeColor = System.Drawing.Color.Black;
            this.lbl_White.Location = new System.Drawing.Point(148, 0);
            this.lbl_White.Name = "lbl_White";
            this.lbl_White.Size = new System.Drawing.Size(96, 39);
            this.lbl_White.TabIndex = 1;
            this.lbl_White.Text = "Black";
            this.lbl_White.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_WhiteCounter
            // 
            this.lbl_WhiteCounter.BackColor = System.Drawing.Color.Green;
            this.lbl_WhiteCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_WhiteCounter.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WhiteCounter.ForeColor = System.Drawing.Color.White;
            this.lbl_WhiteCounter.Location = new System.Drawing.Point(3, 0);
            this.lbl_WhiteCounter.Name = "lbl_WhiteCounter";
            this.lbl_WhiteCounter.Size = new System.Drawing.Size(37, 39);
            this.lbl_WhiteCounter.TabIndex = 2;
            this.lbl_WhiteCounter.Text = "2";
            this.lbl_WhiteCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_BlackCounter
            // 
            this.lbl_BlackCounter.BackColor = System.Drawing.Color.Green;
            this.lbl_BlackCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_BlackCounter.Font = new System.Drawing.Font("Script MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BlackCounter.ForeColor = System.Drawing.Color.Black;
            this.lbl_BlackCounter.Location = new System.Drawing.Point(250, 0);
            this.lbl_BlackCounter.Name = "lbl_BlackCounter";
            this.lbl_BlackCounter.Size = new System.Drawing.Size(39, 39);
            this.lbl_BlackCounter.TabIndex = 3;
            this.lbl_BlackCounter.Text = "2";
            this.lbl_BlackCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Restart
            // 
            this.lbl_Restart.AutoSize = true;
            this.lbl_Restart.BackColor = System.Drawing.Color.Olive;
            this.lbl_Restart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Restart.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_Restart.Location = new System.Drawing.Point(3, 136);
            this.lbl_Restart.Name = "lbl_Restart";
            this.lbl_Restart.Size = new System.Drawing.Size(292, 45);
            this.lbl_Restart.TabIndex = 3;
            this.lbl_Restart.Text = "Restart Game";
            this.lbl_Restart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Restart.Click += new System.EventHandler(this.lbl_Restart_Click);
            this.lbl_Restart.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_Restart.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // layout_Options
            // 
            this.layout_Options.ColumnCount = 2;
            this.layout_Options.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout_Options.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout_Options.Controls.Add(this.lbl_statsP2, 1, 2);
            this.layout_Options.Controls.Add(this.combo_p2, 0, 1);
            this.layout_Options.Controls.Add(this.combo_p1, 0, 1);
            this.layout_Options.Controls.Add(this.lbl_P1, 0, 0);
            this.layout_Options.Controls.Add(this.lbl_P2, 1, 0);
            this.layout_Options.Controls.Add(this.lbl_statsP1, 0, 2);
            this.layout_Options.Controls.Add(this.lbl_config_p1, 0, 4);
            this.layout_Options.Controls.Add(this.lbl_config_p2, 1, 4);
            this.layout_Options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_Options.Location = new System.Drawing.Point(3, 184);
            this.layout_Options.Name = "layout_Options";
            this.layout_Options.RowCount = 7;
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.layout_Options.Size = new System.Drawing.Size(292, 222);
            this.layout_Options.TabIndex = 4;
            // 
            // lbl_statsP2
            // 
            this.lbl_statsP2.AutoSize = true;
            this.lbl_statsP2.BackColor = System.Drawing.Color.Olive;
            this.lbl_statsP2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_statsP2.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_statsP2.Location = new System.Drawing.Point(149, 62);
            this.lbl_statsP2.Name = "lbl_statsP2";
            this.lbl_statsP2.Size = new System.Drawing.Size(140, 31);
            this.lbl_statsP2.TabIndex = 12;
            this.lbl_statsP2.Tag = "p2";
            this.lbl_statsP2.Text = "Stats";
            this.lbl_statsP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_statsP2.Click += new System.EventHandler(this.lbl_stats_Click);
            this.lbl_statsP2.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_statsP2.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // combo_p2
            // 
            this.combo_p2.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "AI Random",
            "AI Max Captures"});
            this.combo_p2.Dock = System.Windows.Forms.DockStyle.Top;
            this.combo_p2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_p2.Enabled = false;
            this.combo_p2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_p2.FormattingEnabled = true;
            this.combo_p2.Location = new System.Drawing.Point(149, 34);
            this.combo_p2.Name = "combo_p2";
            this.combo_p2.Size = new System.Drawing.Size(140, 26);
            this.combo_p2.TabIndex = 8;
            // 
            // combo_p1
            // 
            this.combo_p1.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "AI Random",
            "AI Max Captures"});
            this.combo_p1.Dock = System.Windows.Forms.DockStyle.Top;
            this.combo_p1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_p1.Enabled = false;
            this.combo_p1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_p1.FormattingEnabled = true;
            this.combo_p1.Location = new System.Drawing.Point(3, 34);
            this.combo_p1.Name = "combo_p1";
            this.combo_p1.Size = new System.Drawing.Size(140, 26);
            this.combo_p1.TabIndex = 7;
            // 
            // lbl_P1
            // 
            this.lbl_P1.AutoSize = true;
            this.lbl_P1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_P1.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_P1.ForeColor = System.Drawing.Color.White;
            this.lbl_P1.Location = new System.Drawing.Point(3, 7);
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
            this.lbl_P2.ForeColor = System.Drawing.Color.Black;
            this.lbl_P2.Location = new System.Drawing.Point(149, 7);
            this.lbl_P2.Name = "lbl_P2";
            this.lbl_P2.Size = new System.Drawing.Size(140, 24);
            this.lbl_P2.TabIndex = 10;
            this.lbl_P2.Text = "Player 2";
            this.lbl_P2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_statsP1
            // 
            this.lbl_statsP1.AutoSize = true;
            this.lbl_statsP1.BackColor = System.Drawing.Color.Olive;
            this.lbl_statsP1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_statsP1.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_statsP1.Location = new System.Drawing.Point(3, 62);
            this.lbl_statsP1.Name = "lbl_statsP1";
            this.lbl_statsP1.Size = new System.Drawing.Size(140, 31);
            this.lbl_statsP1.TabIndex = 11;
            this.lbl_statsP1.Tag = "p1";
            this.lbl_statsP1.Text = "Stats";
            this.lbl_statsP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_statsP1.Click += new System.EventHandler(this.lbl_stats_Click);
            this.lbl_statsP1.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_statsP1.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // lbl_config_p1
            // 
            this.lbl_config_p1.AutoSize = true;
            this.lbl_config_p1.BackColor = System.Drawing.Color.Olive;
            this.lbl_config_p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_config_p1.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_config_p1.Location = new System.Drawing.Point(3, 124);
            this.lbl_config_p1.Name = "lbl_config_p1";
            this.lbl_config_p1.Size = new System.Drawing.Size(140, 31);
            this.lbl_config_p1.TabIndex = 13;
            this.lbl_config_p1.Tag = "p1";
            this.lbl_config_p1.Text = "Configure AI";
            this.lbl_config_p1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_config_p1.Click += new System.EventHandler(this.configure_AI);
            this.lbl_config_p1.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_config_p1.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // lbl_config_p2
            // 
            this.lbl_config_p2.AutoSize = true;
            this.lbl_config_p2.BackColor = System.Drawing.Color.Olive;
            this.lbl_config_p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_config_p2.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_config_p2.Location = new System.Drawing.Point(149, 124);
            this.lbl_config_p2.Name = "lbl_config_p2";
            this.lbl_config_p2.Size = new System.Drawing.Size(140, 31);
            this.lbl_config_p2.TabIndex = 14;
            this.lbl_config_p2.Tag = "p2";
            this.lbl_config_p2.Text = "Configure AI";
            this.lbl_config_p2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_config_p2.Click += new System.EventHandler(this.configure_AI);
            this.lbl_config_p2.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_config_p2.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // layout_Batch
            // 
            this.layout_Batch.ColumnCount = 2;
            this.layout_Batch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.layout_Batch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.layout_Batch.Controls.Add(this.num_batch, 0, 0);
            this.layout_Batch.Controls.Add(this.lbl_Batch, 1, 0);
            this.layout_Batch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_Batch.Location = new System.Drawing.Point(3, 412);
            this.layout_Batch.Name = "layout_Batch";
            this.layout_Batch.RowCount = 1;
            this.layout_Batch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_Batch.Size = new System.Drawing.Size(292, 42);
            this.layout_Batch.TabIndex = 5;
            // 
            // num_batch
            // 
            this.num_batch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.num_batch.Font = new System.Drawing.Font("Tahoma", 14F);
            this.num_batch.Location = new System.Drawing.Point(3, 3);
            this.num_batch.Maximum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.num_batch.Name = "num_batch";
            this.num_batch.Size = new System.Drawing.Size(123, 30);
            this.num_batch.TabIndex = 0;
            this.num_batch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_batch.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lbl_Batch
            // 
            this.lbl_Batch.AutoSize = true;
            this.lbl_Batch.BackColor = System.Drawing.Color.Olive;
            this.lbl_Batch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Batch.Font = new System.Drawing.Font("Script MT Bold", 15F);
            this.lbl_Batch.Location = new System.Drawing.Point(132, 0);
            this.lbl_Batch.Name = "lbl_Batch";
            this.lbl_Batch.Size = new System.Drawing.Size(157, 42);
            this.lbl_Batch.TabIndex = 1;
            this.lbl_Batch.Text = "Batch";
            this.lbl_Batch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Batch.Click += new System.EventHandler(this.lbl_Batch_Click);
            this.lbl_Batch.MouseEnter += new System.EventHandler(this.lbl_Restart_MouseEnter);
            this.lbl_Batch.MouseLeave += new System.EventHandler(this.lbl_Restart_MouseLeave);
            // 
            // panel_Game
            // 
            this.panel_Game.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.panel_Game.Controls.Add(this.lbl_Overlay);
            this.panel_Game.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Game.Location = new System.Drawing.Point(0, 0);
            this.panel_Game.Name = "panel_Game";
            this.panel_Game.Size = new System.Drawing.Size(457, 457);
            this.panel_Game.TabIndex = 0;
            // 
            // lbl_Overlay
            // 
            this.lbl_Overlay.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Overlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Overlay.Font = new System.Drawing.Font("Script MT Bold", 60F);
            this.lbl_Overlay.Location = new System.Drawing.Point(0, 0);
            this.lbl_Overlay.Name = "lbl_Overlay";
            this.lbl_Overlay.Size = new System.Drawing.Size(457, 457);
            this.lbl_Overlay.TabIndex = 0;
            this.lbl_Overlay.Text = "Start Game";
            this.lbl_Overlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Overlay.Click += new System.EventHandler(this.lbl_Overlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(759, 457);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Reversi";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.layout_rightALL.ResumeLayout(false);
            this.layout_rightALL.PerformLayout();
            this.layout_NextUp.ResumeLayout(false);
            this.layout_NextUp.PerformLayout();
            this.layout_Scores.ResumeLayout(false);
            this.layout_Scores.PerformLayout();
            this.layout_Options.ResumeLayout(false);
            this.layout_Options.PerformLayout();
            this.layout_Batch.ResumeLayout(false);
            this.layout_Batch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_batch)).EndInit();
            this.panel_Game.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private GamePanel panel_Game;
        private System.Windows.Forms.TableLayoutPanel layout_rightALL;
        private System.Windows.Forms.Label lblCurrentPlayer;
        private System.Windows.Forms.TableLayoutPanel layout_NextUp;
        private System.Windows.Forms.Panel panel_nextPlayer;
        private System.Windows.Forms.TableLayoutPanel layout_Scores;
        private System.Windows.Forms.Label lbl_Black;
        private System.Windows.Forms.Label lbl_White;
        private System.Windows.Forms.Label lbl_WhiteCounter;
        private System.Windows.Forms.Label lbl_BlackCounter;
        private System.Windows.Forms.Label lbl_Overlay;
        private System.Windows.Forms.Label lbl_Restart;
        private System.Windows.Forms.TableLayoutPanel layout_Options;
        private System.Windows.Forms.ComboBox combo_p2;
        private System.Windows.Forms.ComboBox combo_p1;
        private System.Windows.Forms.Label lbl_P1;
        private System.Windows.Forms.Label lbl_P2;
        private System.Windows.Forms.TableLayoutPanel layout_Batch;
        private System.Windows.Forms.NumericUpDown num_batch;
        private System.Windows.Forms.Label lbl_Batch;
        private System.Windows.Forms.Label lbl_statsP2;
        private System.Windows.Forms.Label lbl_statsP1;
        private System.Windows.Forms.Label lbl_config_p1;
        private System.Windows.Forms.Label lbl_config_p2;
    }
}

