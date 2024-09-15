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
    public partial class ScoreBoard : Form
    {
        public ScoreBoard(List<Player> players)
        {
            InitializeComponent();
            ShowScores(players);
        }

        private void ShowScores(List<Player> players)
        {
            var sortedByMoves = players.OrderBy(player => player.Moves).ToList();
            var sortedByTime = players.OrderBy(player => player.Time).ToList();
            int i = 1;

             foreach(var player in sortedByMoves)
             {
             
                dataGridView1.Rows.Add(i++, player.Name, player.Moves);

             }

            i = 1;
            foreach(var player in sortedByTime)
            {
                dataGridView2.Rows.Add(i++, player.Name, player.Time);
            }
        }
    }
}
