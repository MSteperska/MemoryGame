using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory2
{
    public class Card
    {
        public int ID;
        public bool isRevealed;
        public bool isMatched;

        public Card(int id) {
            ID = id;
            isRevealed = false;
            isMatched = false;
        }

        public void Reveal()    // karta odslonieta
        {
            isRevealed = true;
        }

        public void Match()     // karta dopasowana, pozostaje odkryta
        {   
            isMatched = true;
            Reveal();
        }

        public void Hide()  //  karta po zakryciu
        {
            isRevealed = false;
        }

    }
}
