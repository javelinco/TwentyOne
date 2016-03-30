using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TwentyOneTest
{
    [TestFixture]
    public class TwentyOneTest
    {
        [Test]
        public void ShouldWinIfPlayerCardsHigherThanDealerAndPlayerNotBusts()
        {
            GivenPlayerCard(9);
            GivenPlayerCard(2);
            GivenPlayerCard(6);
            GivenDealerCard(8);
            GivenDealerCard(7);

            WhenGetRoundResult();

            ThenWin();
        }

        [Test]
        public void ShouldWinIfPlayerDoesNotBustAndDealerBusts()
        {
            GivenPlayerCard(1);
            GivenPlayerCard(1);
            GivenPlayerCard(1);
            GivenDealerCard(11);
            GivenDealerCard(11);

            WhenGetRoundResult();

            ThenWin();
        }

        [Test]
        public void ShouldLoseIfPlayerBustsAndDealerDoesNotBust()
        {
            GivenPlayerCard(10);
            GivenPlayerCard(10);
            GivenPlayerCard(10);
            GivenDealerCard(1);
            GivenDealerCard(1);

            WhenGetRoundResult();

            ThenLose();
        }

        [Test]
        public void ShouldLoseIfPlayerBustsAndDealerBusts()
        {
            GivenPlayerCard(10);
            GivenPlayerCard(10);
            GivenPlayerCard(10);
            GivenDealerCard(11);
            GivenDealerCard(11);

            WhenGetRoundResult();

            ThenLose();
        }

        [Test]
        public void ShouldLoseIfPlayerLowerThanDealer()
        {
            GivenPlayerCard(1);
            GivenPlayerCard(1);
            GivenPlayerCard(1);
            GivenDealerCard(2);
            GivenDealerCard(2);

            WhenGetRoundResult();

            ThenLose();
        }

        [Test]
        public void ShouldLoseIfPlayerEqualToDealer()
        {
            GivenPlayerCard(2);
            GivenPlayerCard(2);
            GivenPlayerCard(2);
            GivenDealerCard(2);
            GivenDealerCard(4);

            WhenGetRoundResult();

            ThenLose();
        }

        private void GivenPlayerCard(int cardValue)
        {
            _twentyOne.PlayerHand.Add(new TwentyOne.Card { Name = "Test Card", Value = cardValue });
        }

        private void GivenDealerCard(int cardValue)
        {
            _twentyOne.DealerHand.Add(new TwentyOne.Card { Name = "Test Card", Value = cardValue });
        }

        private void WhenGetRoundResult()
        {
            _wonRound = _twentyOne.GetRoundResult();
        }

        private void ThenLose()
        {
            Assert.IsFalse(_wonRound);
        }

        private void ThenWin()
        {
            Assert.IsTrue(_wonRound);
        }

        [SetUp]
        public void SetupTests()
        {
            _twentyOne = new TwentyOne.TwentyOne();
            _wonRound = false;
        }

        private TwentyOne.TwentyOne _twentyOne;
        private bool _wonRound;
    }
}
