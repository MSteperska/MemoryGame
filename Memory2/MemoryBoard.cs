using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Memory2
{
    public partial class MemoryBoard : Form
    {
        //int BoardSize;
        //int NumberOfPlayers;
        private Stopwatch stopwatch = new Stopwatch();

        public List<CardButton> CardsButtonsList;
        public CardButton FirstCardRev = null;
        public CardButton SecondCardRev = null;

        //public List<Player> PlayersList;
        private GameMechanics GameLogic;

        public MemoryBoard(int boardSize, List<Player> playersList)
        {
            InitializeComponent();

            //BoardSize = boardSize;
            //NumberOfPlayers = numberOfPlayers;
            //PlayersList = playersList;
            //int currentPlayerID = 0;

            CardsButtonsList = new List<CardButton>();   //  lista na przyciski z przypisanymi kartami

            GameLogic = new GameMechanics(boardSize, playersList.Count, playersList);
            StartNextPlayerGame();

        }
        private void StartNextPlayerGame()
        {
            if(GameLogic.AreAllPlayersDone())
            {
                ShowScoreboard();
            }
            else
            {
                ResetBoard();

                GameLogic.InitializeGameForCurentPlayer();
                InitializeBoard();
                label2.Text = GameLogic.currentPlayer.Name;
            }
        }

        private void ResetBoard()
        {
                foreach (var button in CardsButtonsList)
                {
                    this.Controls.Remove(button);
                }

                CardsButtonsList.Clear();
            FirstCardRev = null;
            SecondCardRev = null;
        }
        public void InitializeBoard()
        {
           // GameLogic.InitializeGame();


            //  lista z przetasowanymi kartami
            List<Card> CardsList = GameLogic.CreateShuffledCards(GameLogic.BoardSize);
            CardsButtonsList = new List<CardButton>();

            //  tworzenie przyciskow i przypisywanie im kart z listy
            for(int i = 0; i < GameLogic.BoardSize; i++)
            {
                for(int j = 0; j < GameLogic.BoardSize; j++)
                {
                    Card card = CardsList[i * GameLogic.BoardSize + j];
                    CardButton cardButton = new CardButton(card)
                    {
                        Width = 40,
                        Height = 40,
                        Left = (j + 2) * 40,
                        Top = (i + 2) * 40,
                        Enabled = false
                    };

                    cardButton.Click += CardButton_Click;  // Podpięcie eventu kliknięcia
                    CardsButtonsList.Add(cardButton);
                    this.Controls.Add(cardButton);  // Dodanie przycisku do formularza
                }
            }

        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            CardButton clickedButton = (CardButton)sender;

            // ignoruj klikniecie juz odkrytej karty
            if(clickedButton.Card.isRevealed || clickedButton.Card.isMatched)
            {
                return;
            }

            clickedButton.Card.Reveal();
            clickedButton.UpdateAppearance();

            if(FirstCardRev == null)
            {
                FirstCardRev = clickedButton;
            }
            else
            {
                SecondCardRev = clickedButton;

                //aktualizacja liczby ruchow
                GameLogic.CountMoves();
                label4.Text = GameLogic.currentPlayer.Moves.ToString();

                //  sprawdzenie czy karty do siebie pasuja
                GameLogic.CheckForMatch(FirstCardRev, SecondCardRev);

                if (GameLogic.CheckForWin(CardsButtonsList))
                {
                    GameWon();                                            //dodanie zachowania po wygraniu gry - wywołanie osobnej funkcji
                }

                FirstCardRev = null;
                SecondCardRev = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var button in CardsButtonsList)
            {
                button.Enabled = true;
            }

            stopwatch.Start();

        }

        private void GameWon()
        {
            stopwatch.Stop();
            GameLogic.currentPlayer.Time = stopwatch.Elapsed;
            stopwatch.Reset();

            GameLogic.NextPlayer();
            ResetBoard();
            //CardsButtonsList.Clear();
            StartNextPlayerGame();

        }

        private void ShowScoreboard()
        {

            ScoreBoard scoreboard = new ScoreBoard(GameLogic.PlayersList);
            this.Hide();
            scoreboard.ShowDialog();
            this.Close();
        }
    }
}
