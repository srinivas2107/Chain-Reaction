using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class ChainReaction : Form
    {
        Grid[][] grids;
        public int noOfPlayers;
        int activePlayer = 0;
        Color[] playerColors;
        public int NO_OF_ROWS = 9;
        public int NO_OF_COLUMNS = 6;
        public ChainReaction(int noOfPlayers)
        {
            InitializeComponent();
            this.noOfPlayers = noOfPlayers;
            playerColors = new Color[4];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Blue;
            playerColors[2] = Color.Green;
            playerColors[3] = Color.Yellow;
            grids = new Grid[NO_OF_ROWS][];
            for (int i = 0; i < NO_OF_ROWS; i++)
                grids[i] = new Grid[NO_OF_COLUMNS];
            for (int i = 0; i < NO_OF_ROWS; i++)
            {
                for (int j = 0; j < NO_OF_COLUMNS; j++)
                {
                    grids[i][j] = new Grid();
                    grids[i][j].rowIndex = i;
                    grids[i][j].columnIndex=j;
                }
            }
            for (int i = 0; i < NO_OF_ROWS; i++)
            {
                for (int j = 0; j < NO_OF_COLUMNS; j++)
                {
                    grids[i][j].btn = (Button)panel1.Controls[(panel1.Controls.Count - 1) - (i * NO_OF_COLUMNS + j)];
                    grids[i][j].btn.Click += OnButtonClicked;
                    grids[i][j].btn.TextChanged += OnButtonTextChanged;
                }
            }

            for (int i = 0; i < NO_OF_ROWS; i++)
            {
                for (int j = 0; j < NO_OF_COLUMNS; j++)
                {
                    if ((i == 0 && j == 0) || (i == 0 && j == NO_OF_COLUMNS - 1) || (i == NO_OF_ROWS - 1 && j == 0) || (i == NO_OF_ROWS - 1 && j == NO_OF_COLUMNS - 1))
                    {
                        grids[i][j].maxValue = 1;
                    }
                    else if (i == 0 || j == 0 || i == NO_OF_ROWS - 1 || j == NO_OF_COLUMNS - 1)
                    {
                        grids[i][j].maxValue = 2;
                    }
                    else
                    {
                        grids[i][j].maxValue = 3;
                    }
                }
            }
        }

        private void OnButtonTextChanged(object sender, EventArgs e)
        {
            Button btnSelected = (Button)sender;
            if (btnSelected.Text != "")
            {
                for (int i = 0; i < NO_OF_ROWS; i++)
                {
                    for (int j = 0; j < NO_OF_COLUMNS; j++)
                    {
                        if (grids[i][j].btn == btnSelected)
                        {
                            Grid g = grids[i][j];
                            if (g.value > g.maxValue)
                            {
                                g.value = 0;
                                g.btn.Text = "";
                                if (g.columnIndex < NO_OF_COLUMNS - 1)
                                {
                                    Grid right = grids[g.rowIndex][g.columnIndex + 1];
                                    right.value++;
                                    right.btn.Text = "" + right.value;
                                    //Thread.Sleep(100);
                                    right.btn.ForeColor = playerColors[activePlayer];
                                }
                                if (g.columnIndex > 0)
                                {
                                    Grid left = grids[g.rowIndex][g.columnIndex - 1];
                                    left.value++;
                                    left.btn.Text = "" + left.value;

                                    left.btn.ForeColor = playerColors[activePlayer];
                                }
                                if (g.rowIndex > 0)
                                {
                                    Grid up = grids[g.rowIndex - 1][g.columnIndex];
                                    up.value++;
                                    up.btn.Text = "" + up.value;

                                    up.btn.ForeColor = playerColors[activePlayer];
                                }
                                if (g.rowIndex < NO_OF_ROWS - 1)
                                {
                                    Grid down = grids[g.rowIndex + 1][g.columnIndex];
                                    down.value++;
                                    down.btn.Text = "" + down.value;

                                    down.btn.ForeColor = playerColors[activePlayer];
                                }
                            }
                            //Thread.Sleep(10);
                        }
                    }
                }
            }
        }

        int noOfClicks;
        private void OnButtonClicked(object sender, EventArgs e)
        {
            Button btnSelected = (Button)sender;
            if(btnSelected.Text!="" && btnSelected.ForeColor!=playerColors[activePlayer])
            {
                return;
            }
            noOfClicks++;
            for (int i=0;i<NO_OF_ROWS;i++)
            {
                for(int j=0;j<NO_OF_COLUMNS;j++)
                {
                    if(grids[i][j].btn==btnSelected)
                    {
                        grids[i][j].value++;
                        grids[i][j].btn.Text = "" + grids[i][j].value;
                        
                        grids[i][j].btn.ForeColor = playerColors[activePlayer];
                        //if (grids[i][j].value > grids[i][j].maxValue)
                        //{
                        //    Modify(grids[i][j]);                          
                        //}
                        if (IsPlayerWon() && noOfClicks >= noOfPlayers)
                        {
                            MessageBox.Show("player " + (activePlayer + 1) + " won ");
                            panel1.Enabled = false;
                            return;
                        }
                        activePlayer++;
                        if (activePlayer == noOfPlayers)
                            activePlayer = 0;
                        return;

                    }
                }
            }
        }

        private bool IsPlayerWon()
        {
            for(int i = 0; i < NO_OF_ROWS; i++)
            {
                for (int j = 0; j < NO_OF_COLUMNS; j++)
                {
                    if (grids[i][j].btn.Text != "")
                    {
                        if (grids[i][j].btn.ForeColor != playerColors[activePlayer])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void Modify(Grid grid)
        {
            ArrayList list = new ArrayList();
            list.Add(grid);
            while(!(list.Count==0))
            {
                Grid g =(Grid) list[0];
                if (g.value > g.maxValue)
                {
                    g.value = 0;
                    g.btn.Text = "";
                    if (g.columnIndex < NO_OF_COLUMNS - 1)
                    {
                        Grid right = grids[g.rowIndex][g.columnIndex + 1];
                        right.value++;
                        right.btn.Text = "" + right.value;
                        //Thread.Sleep(100);
                        right.btn.ForeColor = playerColors[activePlayer];
                        if (right.value > right.maxValue)
                        {
                            list.Add(right);
                        }
                    }
                    if (g.columnIndex > 0)
                    {
                        Grid left = grids[g.rowIndex][g.columnIndex - 1];
                        left.value++;
                        left.btn.Text = "" + left.value;

                        left.btn.ForeColor = playerColors[activePlayer];
                        if (left.value > left.maxValue)
                        {
                            list.Add(left);
                        }
                    }
                    if (g.rowIndex > 0)
                    {
                        Grid up = grids[g.rowIndex - 1][g.columnIndex];
                        up.value++;
                        up.btn.Text = "" + up.value;

                        up.btn.ForeColor = playerColors[activePlayer];
                        if (up.value > up.maxValue)
                        {
                            list.Add(up);
                        }
                    }
                    if (g.rowIndex < NO_OF_ROWS - 1)
                    {
                        Grid down = grids[g.rowIndex + 1][g.columnIndex];
                        down.value++;
                        down.btn.Text = "" + down.value;

                        down.btn.ForeColor = playerColors[activePlayer];
                        if (down.value > down.maxValue)
                        {
                            list.Add(down);
                        }
                    }
                }
                list.Remove(g);
            }
            
        }
    }

    public class Grid
    {
        public Button btn;
        public int value;
        public int maxValue;
        public int rowIndex;
        public int columnIndex;
    }
}
