using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReversiAI {
    public partial class configAI : Form {
        private Controller ctrl;
        private AIConfiguration config;

        public configAI() {
            InitializeComponent();
            comboBox1.Items.AddRange(Controller.players);
        }

        public void setController(Controller c, AIConfiguration cfg) {
            ctrl = c;
            config = cfg;
            comboBox1.SelectedIndex = (int)config.AI;
            numericUpDown1.Value = config.maxDepth;
            numericUpDown2.Value = config.maxTime;
            chk_ABPruning.Checked = config.ABPruning;
        }

        private void mouseEnterGlow(object sender, EventArgs e) {
            (sender as Label).BackColor = Color.OliveDrab;
        }

        private void mouseExitGlow(object sender, EventArgs e) {
            (sender as Label).BackColor = Color.Olive;
        }

        private void btn_Close(object sender, EventArgs e) {
            Close();
        }

        private void btn_Save(object sender, EventArgs e) {
            ctrl.saveConfig(config);
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            config.AI = (Controller.Player)comboBox1.SelectedIndex;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            config.maxDepth = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            config.maxTime = (int)numericUpDown2.Value;
        }

        private void chk_ABPruning_CheckedChanged(object sender, EventArgs e) {
            config.ABPruning = chk_ABPruning.Checked;
        }
    }
}
