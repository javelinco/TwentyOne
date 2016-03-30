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

        private static void HandDisplay(List<Card> hand)
        {
            var handList = new StringBuilder();
            foreach (var card in hand)
            {
                handList.AppendFormat("{0},", card.Name);
            }
            Console.WriteLine("Your hand is: {0}", handList.ToString().Substring(0, handList.ToString().Length - 1));
        }

        static void Main(string[] args)
        {
            var gameDeck = new Deck();

            var gamePlay = new TwentyOne();

            gamePlay.PlayerHand.Add(gameDeck.GetCard());
            gamePlay.PlayerHand.Add(gameDeck.GetCard());

            gamePlay.DealerHand.Add(gameDeck.GetCard());
            gamePlay.DealerHand.Add(gameDeck.GetCard());

            Console.WriteLine("Welcome to our Casino's Twenty Table!");
            HandDisplay(gamePlay.PlayerHand);
            Console.WriteLine("The dealer's visible hand is {0}", gamePlay.DealerHand.Skip(1).First().Name);

            var bust = false;
            while (!bust
                && PlayerHit())
            {
                gamePlay.PlayerHand.Add(gameDeck.GetCard());
                bust = gamePlay.PlayerBusted();
                if (bust)
                    Console.WriteLine("Your busted!");
                HandDisplay(gamePlay.PlayerHand);
            }

            Console.WriteLine("The dealer's hand is {0}, {1}", gamePlay.DealerHand.First().Name,
                gamePlay.DealerHand.Skip(1).First().Name);

            var won = gamePlay.GetRoundResult();
            if (won)
                Console.WriteLine("You win!");
            else
                Console.WriteLine("You lose!");
            Console.Read();
        }
    }
}
