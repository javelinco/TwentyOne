using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    class Program
    {
        private static bool PlayerHit()
        {
            Console.WriteLine("Do you want to hit or stay? Type 'Y' to hit, 'N' to stay");
            var key = Console.ReadKey();
            Console.WriteLine("");
            return (key.KeyChar == 'Y'
                || key.KeyChar == 'y');
        }

        private static bool DealerHit(int dealerTotal)
        {
            return dealerTotal <= 16;
        }

        private static void PlayerHandDisplay(List<Card> hand, int playerTotal)
        {
            var handList = new StringBuilder();
            foreach (var card in hand)
            {
                handList.AppendFormat("{0},", card.Name);
            }
            Console.WriteLine("Your hand is {0}: {1}", playerTotal, handList.ToString().Substring(0, handList.ToString().Length - 1));
            if (playerTotal == 21)
                Console.WriteLine("You have blackjack!");
        }

        private static void DealerHandDisplay(List<Card> hand, int dealerTotal)
        {
            var handList = new StringBuilder();
            foreach (var card in hand)
            {
                handList.AppendFormat("{0},", card.Name);
            }
            Console.WriteLine("Dealer's hand is {0}: {1}", dealerTotal, handList.ToString().Substring(0, handList.ToString().Length - 1));
            if (dealerTotal == 21)
                Console.WriteLine("Dealer has blackjack! :(");
        }

        static void Main(string[] args)
        {
            var playAgain = false;
            do
            {
                Console.Clear();

                var gameDeck = new Deck();

                var gamePlay = new TwentyOne();

                gamePlay.PlayerHand.Add(gameDeck.GetCard());
                gamePlay.PlayerHand.Add(gameDeck.GetCard());

                gamePlay.DealerHand.Add(gameDeck.GetCard());
                gamePlay.DealerHand.Add(gameDeck.GetCard());

                Console.WriteLine("Welcome to our Casino's Blackjack Table!");
                PlayerHandDisplay(gamePlay.PlayerHand, gamePlay.PlayerTotal());

                Console.WriteLine("The dealer's visible hand is {0}", gamePlay.DealerHand.Skip(1).First().Name);

                var playerBust = false;
                while (!playerBust
                    && !(gamePlay.PlayerTotal() == 21)
                    && PlayerHit())
                {
                    gamePlay.PlayerHand.Add(gameDeck.GetCard());
                    playerBust = gamePlay.PlayerBusted();
                    if (playerBust)
                        Console.WriteLine("Your busted!");
                    PlayerHandDisplay(gamePlay.PlayerHand, gamePlay.PlayerTotal());
                }

                var dealerBust = false;
                while (!dealerBust
                    && DealerHit(gamePlay.DealerTotal()))
                {
                    gamePlay.DealerHand.Add(gameDeck.GetCard());
                    dealerBust = gamePlay.DealerBusted();
                    if (dealerBust)
                        Console.WriteLine("Dealer busted!");
                }
                DealerHandDisplay(gamePlay.DealerHand, gamePlay.DealerTotal());

                Console.WriteLine(string.Format("You {0}!", gamePlay.GetRoundResult()));

                Console.WriteLine("Play again (Y/N)?");
                playAgain = Console.ReadKey().Key == ConsoleKey.Y;
            } while (playAgain);
        }
    }
}
