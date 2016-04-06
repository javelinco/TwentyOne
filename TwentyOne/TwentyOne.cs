using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public enum GameResult
    {
        Win,
        Lose,
        Tie
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
            else if (initialSum >= 21)
                return initialSum;
            else
                return initialSum + 10;
        }

        public int PlayerTotal()
        {
            return HandTotal(PlayerHand);
        }

        public int DealerTotal()
        {
            return HandTotal(DealerHand);
        }

        public GameResult GetRoundResult()
        {
            if (!PlayerBusted()
                && (PlayerTotal() > DealerTotal()
                || DealerBusted()))
                return GameResult.Win;
            else if (PlayerTotal() == DealerTotal())
                return GameResult.Tie;
            else
                return GameResult.Lose;
        }

        public bool PlayerBusted()
        {
            return Busted(PlayerTotal());
        }

        public bool DealerBusted()
        {
            return Busted(DealerTotal());
        }
    }
}
