using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TwentyOneTest
{
    [TestFixture]
    public class DeckTest
    {
        [Test]
        public void ShouldReturnValidCard()
        {
            WhenGetCard();
            ThenCardIsValid();
        }

        private void WhenGetCard()
        {
            _card = _gameDeck.GetCard();
        }

        private void ThenCardIsValid()
        {
            Assert.GreaterOrEqual(_card.Value, TwentyOne.Deck.CardValuesArray.Min());
            Assert.LessOrEqual(_card.Value, TwentyOne.Deck.CardValuesArray.Max());
        }

        [SetUp]
        public void SetupTests()
        {
            _gameDeck = new TwentyOne.Deck();
        }

        private TwentyOne.Deck _gameDeck;
        private TwentyOne.Card _card;
    }
}
