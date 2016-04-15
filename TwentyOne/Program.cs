using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwentyOne
{
    class Program
    {
        private static string PlayerChoice(List<Card> hand, int handTotal, int bet, int chips)
        {
            int[] values = { 9, 10, 11 };
            var key = "";
            if (bet <= (chips/2)
                && (hand.Count() == 2
                || values.Contains(handTotal)))
            {
                Console.WriteLine("Do you want to hit, double-down, or stay?\n\t'H' to hit\n\t'D' to double-down\n\t'S' to stay");
                key = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Do you want to hit or stay?\n\t'H' to hit\n\t'S' to stay");
                key = Console.ReadLine();
            }
            return key;
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
            Console.WriteLine("Your hand: {0} Total: {1}", handList.ToString().Substring(0, handList.ToString().Length - 1), playerTotal);
            if (hand.Count() == 2 && playerTotal == 21)
                Console.WriteLine("BLACKJACK!!!");
        }

        private static void DealerHandDisplay(List<Card> hand, int dealerTotal)
        {
            var handList = new StringBuilder();
            foreach (var card in hand)
            {
                handList.AppendFormat("{0},", card.Name);
            }
            Console.WriteLine("Dealer's hand: {0} Total: {1}\n", handList.ToString().Substring(0, handList.ToString().Length - 1), dealerTotal);
            if (hand.Count() == 2 && dealerTotal == 21)
                Console.WriteLine("Bummer... Dealer has blackjack :(");
        }

        static void Main(string[] args)
        {
            var gameChips = 1000;
            var playAgain = false;

            do
            {
                var gameDeck = new Deck();
                var gamePlay = new TwentyOne();
                var bet = 0;

                Console.Clear();

                Console.WriteLine("Your chip count currently is: " + gameChips);
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

                Console.WriteLine("The dealer's visible hand is {0} Total: {1}\n", gamePlay.DealerHand.Skip(1).First().Name, gamePlay.DealerVisibleHand(gamePlay.DealerHand.Skip(1).First().Value));

                var playerBust = false;
                var Choice = "";
                while (!playerBust
                    && !(gamePlay.PlayerTotal() == 21)
                    && (Choice != "s" && Choice != "S"))
                {
                    Choice = PlayerChoice(gamePlay.PlayerHand, gamePlay.PlayerTotal(), bet, gameChips);
                    if (Choice == "H"
                        || Choice == "h")
                    {
                        gamePlay.PlayerHand.Add(gameDeck.GetCard());
                        playerBust = gamePlay.PlayerBusted();
                        if (playerBust)
                            Console.WriteLine("You're busted!");
                        PlayerHandDisplay(gamePlay.PlayerHand, gamePlay.PlayerTotal());
                    }
                    else if (Choice == "D"
                        || Choice == "d")
                    {
                        bet = bet * 2;
                        gamePlay.PlayerHand.Add(gameDeck.GetCard());
                        playerBust = gamePlay.PlayerBusted();
                        if (playerBust)
                            Console.WriteLine("You're busted!");
                        PlayerHandDisplay(gamePlay.PlayerHand, gamePlay.PlayerTotal());
                        break;
                    }
                    else
                    {
                        PlayerHandDisplay(gamePlay.PlayerHand, gamePlay.PlayerTotal());
                    }
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

                if (gamePlay.PlayerBlackjack()
                    && !gamePlay.DealerBlackjack())
                    Console.WriteLine(string.Format("You won with a {0}!", gamePlay.GetRoundResult()));
                else
                    Console.WriteLine(string.Format("You {0}!", gamePlay.GetRoundResult()));

                switch (gamePlay.GetRoundResult().ToString())
                {
                    case "BLACKJACK":
                        var blackjackWinnings = Convert.ToInt32(bet * 1.5);
                        Console.WriteLine("You gained " + blackjackWinnings + " chips!!!");
                        gameChips = gamePlay.addChips(blackjackWinnings, gameChips);
                        break;
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
