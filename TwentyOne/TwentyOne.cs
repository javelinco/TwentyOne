using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
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

        public bool GetRoundResult()
        {
            var dealerTotal = DealerHand.Sum(x => x.Value);
            var playerTotal = PlayerHand.Sum(x => x.Value);
            return !PlayerBusted()
                && (playerTotal > dealerTotal
                || DealerBusted());
        }

        public bool PlayerBusted()
        {
            return Busted(PlayerHand.Sum(x => x.Value));
        }

        public bool DealerBusted()
        {
            return Busted(DealerHand.Sum(x => x.Value));
        }
    }
}
