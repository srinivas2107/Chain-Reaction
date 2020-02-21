using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int noOfGrids;
        public int noOfPlayers;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(txtNumberOfPlayers.Text.Length==0)
            {
                MessageBox.Show("Please Enter Number of Players");
                return;
            }
            try
            {
                noOfPlayers = Convert.ToInt32(txtNumberOfPlayers.Text);
                ChainReaction chainReaction = new ChainReaction(noOfPlayers);
                this.Hide();
                chainReaction.ShowDialog();
                this.Show();

            }
            catch(FormatException ex)
            {
                MessageBox.Show("Please Enter Valid No Of Players");
                return;
            }
        }
    }
}
