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
    public partial class Help : Form
    {
        SimulationScreen sm = new SimulationScreen();
        public Help()
        {
                      
            InitializeComponent();
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void EXT_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/brettsschmidt/crispy-pancake/issues");
        }

    }
}
