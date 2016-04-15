using System;
using System.Collections.Generic;

namespace TwentyOne
{
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class Deck
    {
        private string[] CardNameArray = new[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };
        private string[] SuiteArray = new[] { "Hearts", "Spades", "Diamonds", "Clubs" };
        public static int[] CardValuesArray = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 1 };

        public List<Card> RemainingDeck;

        private Random _Random = new Random();

        public Deck()
        {
            RemainingDeck = CreateNewDeck();
        }

        public List<Card> CreateNewDeck()
        {
            var deck = new List<Card>();
            foreach (var suite in SuiteArray)
            {
                for (var index = 0; index < CardNameArray.Length; index++)
                {
                    deck.Add(new Card
                    {
                        Name = string.Format("{0} of {1}", CardNameArray[index], suite),
                        Value = CardValuesArray[index]
                    });
                }
            }
            return deck;
        }

        public Card GetCard()
        {
            if (RemainingDeck == null)
                RemainingDeck = CreateNewDeck();
            var pullCard = _Random.Next(0, RemainingDeck.Count);
            var card = RemainingDeck[pullCard];
            RemainingDeck.Remove(card);
            return card;
        }

    }
}
