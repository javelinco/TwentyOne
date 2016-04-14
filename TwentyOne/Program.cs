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
            return dealerTotal <= 17;
        }

        private static void PlayerHandDisplay(List<Card> hand, int playerTotal)
        {
            var handList = new StringBuilder();
            foreach (var card in hand)
            {
                handList.AppendFormat("{0},", card.Name);
            }
            Console.WriteLine("Your hand: {0} Total: {1}\n", handList.ToString().Substring(0, handList.ToString().Length - 1), playerTotal);
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
            Console.WriteLine("Dealer's hand: {0} Total: {1}\n", handList.ToString().Substring(0, handList.ToString().Length - 1), dealerTotal);
            if (dealerTotal == 21)
                Console.WriteLine("Dealer has blackjack! :(");
        }

        public static Random random = new Random();

        static void Main(string[] args)
        {
            var gameChips = random.Next(1000,2001);
            var playAgain = false;

            do
            {
                var gameDeck = new Deck();
                var gamePlay = new TwentyOne();
                var bet = 0;

                Console.Clear();

                Console.WriteLine("Your Bankroll is currently: " + gameChips);
                do
                {
                    Console.WriteLine("How much do you want to bet?");
                    bet = Convert.ToInt32(Console.ReadLine());
                } while (bet > gameChips);

                gamePlay.PlayerHand.Add(gameDeck.GetCard());
                gamePlay.PlayerHand.Add(gameDeck.GetCard());

                gamePlay.DealerHand.Add(gameDeck.GetCard());
                gamePlay.DealerHand.Add(gameDeck.GetCard());

                Console.WriteLine("Welcome to our Casino's Blackjack Table!\n");
                PlayerHandDisplay(gamePlay.PlayerHand, gamePlay.PlayerTotal());

                Console.WriteLine("The dealer's visible hand is {0} Total: {1}\n", gamePlay.DealerHand.Skip(1).First().Name, gamePlay.DealerHand.Skip(1).First().Value);

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

                switch (gamePlay.GetRoundResult().ToString())
                {
                    case "Win":
                        Console.WriteLine("You gained " + bet + " chips!!!");
                        gameChips = gamePlay.addChips(bet, gameChips);
                        break;
                    case "Lose":
                        Console.WriteLine("You lost " + bet + " chips :(");
                        gameChips = gamePlay.subtractChips(bet, gameChips);
                        break;
                    case "Tie":
                        Console.WriteLine("Play again (Y/N)?");
                        break;
                    default:
                        break;
                }

                if (gameChips > 0)
                {
                    Console.WriteLine("Play again (Y/N)?");
                    playAgain = Console.ReadKey().Key == ConsoleKey.Y;
                }
                else
                {
                    Console.WriteLine("You have lost all of your money, hit the ATM and come back.");
                    Console.Read();
                    playAgain = false;
                }
            } while (playAgain);
        }
    }
}
