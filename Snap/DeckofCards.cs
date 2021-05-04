using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snap
{

    public class DeckofCards : IDeck
    {
        const int NUMBER_OF_CARDS = 52;
        const int NO_OF_SUITES = 4;
        const int CARDS_PER_SUITE = 13;

        private Card[] _deck;
        private int _cardsInPlay;
        private int _noDecks;
        private bool _hasJokers;
        private readonly Random _randomCard = new Random();

        private int DealtCardNo { get; set; } = 0;
        private bool InPlay { get => (DealtCardNo > 0) && (DealtCardNo < (NUMBER_OF_CARDS * _noDecks) - 1); }

        public int NoOfCardsInPack
        {
            get => _deck.Count(card => card != null);
        }

        public int NoOfCardsInPlay
        {
            get => _cardsInPlay;
        }

        public void ClearCardsInPlay() => _cardsInPlay = 0;
        public DeckofCards(int noDecks = 1, bool Jokers = default) //jokers not implemented
        {
            _noDecks = noDecks;
            _hasJokers = Jokers;
        }

        public IDeck CreateDeck()
        {
            _deck = new Card[NUMBER_OF_CARDS * _noDecks];

            for (int cardNo = 0; cardNo < NUMBER_OF_CARDS * _noDecks; ++cardNo)
            {
                _deck[cardNo] = new Card((CardSuite)(cardNo % NO_OF_SUITES), (CardValue)((cardNo % CARDS_PER_SUITE) + 1)); //+1 accounting for joker in enum
            }
            DealtCardNo = 0;
            return this;
        }

        public IDeck ShuffleDeck()
        {
            //All cards are in deck and game is not in play
            if (NoOfCardsInPack == NUMBER_OF_CARDS * _noDecks && !InPlay)
            {
                int card2Shuffle;

                for (int cardNo = 0; cardNo < NUMBER_OF_CARDS * _noDecks; ++cardNo)
                {
                    card2Shuffle = _randomCard.Next(0, NUMBER_OF_CARDS * _noDecks);
                    (_deck[cardNo], _deck[card2Shuffle]) = (_deck[card2Shuffle], _deck[cardNo]);
                }
            }
            DealtCardNo = 0;
            return this;
        }

        public ICard DealCard()
        {
            if (DealtCardNo < NoOfCardsInPack && NoOfCardsInPack == NUMBER_OF_CARDS * _noDecks)
            {
                ++_cardsInPlay;
                return _deck[DealtCardNo++];
            }
            else
                return null;
        }
    }
}
