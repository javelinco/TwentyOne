using System.Collections.Generic;
using System.Linq;

namespace TwentyOne
{
    public enum GameResult
    {
        BLACKJACK,
        Win,
        Lose,
        Push
    }

    public class TwentyOne
    {
        public const int MAX_VALID_CARD_VALUE = 21;

        public List<Card> DealerHand { get; set; }
        public List<Card> PlayerHand { get; set; }

        public TwentyOne()
        {
            DealerHand = new List<Card>();
            PlayerHand = new List<Card>();
        }

        private bool BlackJack(List<Card> hand)
        {
            var value = HandTotal(hand);
            var blackjack = false;

            if (hand.Count() == 2 && value == MAX_VALID_CARD_VALUE)
                blackjack = true;
            return blackjack;
        }

        private bool Busted(int cardTotal)
        {
            return cardTotal > MAX_VALID_CARD_VALUE;
        }

        private int HandTotal(List<Card> hand)
        {
            var initialSum = hand.Sum(x => x.Value);
            var aceExists = hand.Any(x => x.Value == 1);

            if (!aceExists)
                return initialSum;
            else if (initialSum >= MAX_VALID_CARD_VALUE)
                return initialSum;
            else
            {
                initialSum = initialSum + 10;
                if (initialSum > MAX_VALID_CARD_VALUE)
                    initialSum = initialSum - 10;
                return initialSum;
            }
        }

        public int DealerVisibleHand(int value)
        {
            if (value == 1)
                value = value + 10;
            return value;
        }

        public int PlayerTotal()
        {
            return HandTotal(PlayerHand);
        }

        public int DealerTotal()
        {
            return HandTotal(DealerHand);
        }

        public bool PlayerBlackjack()
        {
            return BlackJack(PlayerHand);
        }

        public bool DealerBlackjack()
        {
            return BlackJack(DealerHand);
        }

        public bool PlayerBusted()
        {
            return Busted(PlayerTotal());
        }

        public bool DealerBusted()
        {
            return Busted(DealerTotal());
        }
        
        public int addChips(int bet, int chips)
        {
            var total = chips + bet;
            return total;
        }

        public int subtractChips(int bet, int chips)
        {
            var total = chips - bet;
            return total;
        }

        public GameResult GetRoundResult()
        {
            if (PlayerBlackjack()
                && !DealerBlackjack())
                return GameResult.BLACKJACK;
            else if (!PlayerBusted()
                && (PlayerTotal() > DealerTotal()
                || DealerBusted()))
                return GameResult.Win;
            else if (PlayerTotal() == DealerTotal())
                return GameResult.Push;
            else
                return GameResult.Lose;
        }
    }
}
