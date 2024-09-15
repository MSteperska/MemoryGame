using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory2
{
    public partial class Form1 : Form
    {
        int BoardSize;
        int NumberOfPlayers = 0;
        public List<Player> PlayersList;
        public Form1()
        {
            InitializeComponent();
        }
        //  pobieranie wielksci planszy i tworzenie listy do przechowywania graczy
        private void button3_Click_1(object sender, EventArgs e)
        {
            BoardSize = (int)numericUpDown1.Value;
            PlayersList = new List<Player>();
        }

        //  dodawanie graczy do listy + wyswietlanie
        private void button2_Click(object sender, EventArgs e)
        {

            Player p1 = new Player(textBox1.Text);
            PlayersList.Add(p1);
            NumberOfPlayers++;

            ListViewItem item = new ListViewItem(p1.Name);
            listView1.Items.Add(item);

            textBox1.Text = null;

        }

        //   rozpoczecie gry
        private void button1_Click(object sender, EventArgs e)
        {
            MemoryBoard memoryBoard = new MemoryBoard(BoardSize, PlayersList);
            this.Hide();
            memoryBoard.ShowDialog();
            this.Close();
        }
    }
}
