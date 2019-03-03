using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoamingOJ
{

    public partial class SimulationScreen : Form
    {

        public SimulationScreen()
        {
            InitializeComponent();

        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region skin changes
        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DimGray;
            picture.BackColor = Color.GhostWhite;
            TextBox.BackColor = Color.GhostWhite;
            TextBox.ForeColor = Color.Black;
            OptionOne.BackColor = Color.GhostWhite;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.GhostWhite;
            OptionTwo.ForeColor = Color.Black;

        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            picture.BackColor = Color.Purple;
            TextBox.BackColor = Color.Purple;
            TextBox.ForeColor = Color.White;
            OptionOne.BackColor = Color.Purple;
            OptionOne.ForeColor = Color.White;
            OptionTwo.BackColor = Color.Purple;
            OptionTwo.ForeColor = Color.White;

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            picture.BackColor = Color.LightGray;
            TextBox.BackColor = Color.LightGray;
            TextBox.ForeColor = Color.Black;
            OptionOne.BackColor = Color.LightGray;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.LightGray;
            OptionTwo.ForeColor = Color.Black;

        }

        private void rainbowCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.PaleVioletRed;
            picture.BackColor = Color.Blue;
            TextBox.BackColor = Color.Yellow;
            TextBox.ForeColor = Color.Black;
            OptionOne.BackColor = Color.Green;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.Fuchsia;
            OptionTwo.ForeColor = Color.Black;

        }

        private void peasInTheDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            picture.BackColor = Color.ForestGreen;
            TextBox.BackColor = Color.ForestGreen;
            TextBox.ForeColor = Color.Black;
            OptionOne.BackColor = Color.ForestGreen;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.ForestGreen;
            OptionTwo.ForeColor = Color.Black;

        }

        private void codeRedWhoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            picture.BackColor = Color.DarkRed;
            TextBox.BackColor = Color.DarkRed;
            TextBox.ForeColor = Color.GhostWhite;
            OptionOne.BackColor = Color.DarkRed;
            OptionOne.ForeColor = Color.GhostWhite;
            OptionTwo.BackColor = Color.DarkRed;
            OptionTwo.ForeColor = Color.GhostWhite;


        }

        #endregion

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hp = new Help();
            hp.ShowDialog();
        }
    }
}
