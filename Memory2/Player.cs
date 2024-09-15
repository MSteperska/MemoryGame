using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory2
{
    public class Player
    {
        public string Name;
        public int Moves;
        public TimeSpan Time;

        public Player(string name) {
            Name = name;
            Moves = 0;
            Time = TimeSpan.Zero;
        }
    }
}
