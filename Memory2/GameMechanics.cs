using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Memory2
{
    public class GameMechanics
    {
        public int BoardSize;
        int NumberOfPlayers;

        public List<Player> PlayersList;
        public Player currentPlayer;

        public List<Card> CardsList;
        public CardButton FirstCard;
        public CardButton SecondCard;

        public int Moves;
        private int currPlayerID = 0;


        public GameMechanics(int boardSize, int numberOfPlayers, List<Player> playersList)
        {
            BoardSize = boardSize;
            NumberOfPlayers = numberOfPlayers;
            PlayersList = playersList;
        }
        public void InitializeGameForCurentPlayer()
        {
            currentPlayer = PlayersList[currPlayerID];
            CardsList = CreateShuffledCards(BoardSize);
            currentPlayer.Moves = 0;
            FirstCard = null;
            SecondCard = null;
            //InitializeGameBoard();
        }

        public void NextPlayer()
        {
            currPlayerID++;
            if(currPlayerID >= PlayersList.Count)
            {
                currPlayerID = -1;
            }
        }

        public bool AreAllPlayersDone()
        {
            return currPlayerID == -1;
        }

        //  utworzenie talii losowych kart do gry
        public List<Card> CreateShuffledCards(int boardSize)
        {
            List<Card> CardsList = new List<Card> ();
            int pairs = (boardSize * boardSize) / 2;

            for(int i = 0; i < pairs; i++)
            {
                CardsList.Add(new Card(i));
                CardsList.Add(new Card(i));
            }

            CardsList = CardsList.OrderBy(a => Guid.NewGuid()).ToList();

            if ((boardSize * boardSize) % 2 != 0)  // Jeśli plansza ma nieparzystą liczbę kart, dodaj dodatkową kartę
            {
                Card extraCard = new Card(-1);
                CardsList.Add(extraCard);
            }

            return CardsList;

        }

        public async void CheckForMatch(CardButton FirstCard, CardButton SecondCard)
        {
            if(FirstCard.Card.ID == SecondCard.Card.ID)
            {
                FirstCard.Card.Match();
                SecondCard.Card.Match();
            }
            else
            {
                await Task.Delay(1000);

                FirstCard.Card.Hide();
                SecondCard.Card.Hide();
                FirstCard.UpdateAppearance();
                SecondCard.UpdateAppearance();
            }
        }

        public void CountMoves()
        {
            currentPlayer.Moves++;
        }

        public bool CheckForWin(List<CardButton> CardsButtonsList)
        {
            if (BoardSize % 2 != 0)
            {
                List<CardButton> tempList = new List<CardButton>(CardsButtonsList);
                tempList.RemoveAt(tempList.Count - 1);

                if (tempList.All(button => button.Card.isMatched))
                {
                    CardButton extraCard = CardsButtonsList.Last();
                    extraCard.Card.Match();
                    extraCard.UpdateAppearance();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //  gdy jest parzysta liczba kart
            else
            {
                return CardsButtonsList.All(button => button.Card.isMatched);
            }

        }

    }
}
