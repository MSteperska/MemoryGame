using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory2
{
    public class CardButton : Button 
    {
        public Card Card {get; private set;}
        public CardButton(Card card) 
        {
            Card = card;
            UpdateAppearance();
        }

        public void UpdateAppearance()
        {
           if (Card.isMatched)
            {
                this.Text = Card.ID.ToString();
                this.Enabled = false;
            }
           if(Card.ID == -1)
            {
                this.Text = Card.ID.ToString();
            }

            if (Card.isRevealed)
            {
                this.Text = Card.ID.ToString();
            }
            else
            {
                this.Text = "";
            }
        }

    }

}
