using NUnit.Framework;
using Moq;
using Snap;
using System.Collections.Generic;
using System.Linq;

namespace SnapTestProject
{
    [TestFixture]
    public class SnapUnitTest
    {
        private IDeck _deck, _deck2;
        private ICard _card, _card2;
        private Mock<ICard> _mockCard1, _mockCard2;
        private Mock<ISnap> _snap;

        [OneTimeSetUp]
        public void Setup()
        {
            _snap = new Mock<ISnap>(MockBehavior.Strict);
            _mockCard1 = new Mock<ICard>(MockBehavior.Strict);
            _mockCard2 = new Mock<ICard>(MockBehavior.Strict);
        }


        [Test]
        public void TestAllCardsAreInDeck()
        {
            List<ICard> dealtCards = new List<ICard>();

            _snap.Setup(s => s.Packs).Returns(1);
            _deck = new DeckofCards(_snap.Object.Packs);
            _deck.CreateDeck();
            _card = _deck.DealCard();

            while (_card != null)
            {
                if (dealtCards.Count >0)
                    Assert.False(dealtCards.Any(dc => dc.Suite == _card.Suite && dc.Value == _card.Value));

                dealtCards.Add(_card);
                _card = _deck.DealCard();
            }

            Assert.True(dealtCards.Count == 52);
        }

        [Test]
        public void TestShuffle()
        {
            _snap.Setup(s => s.Packs).Returns(1);
            _deck = new DeckofCards(_snap.Object.Packs);
            _deck2 = new DeckofCards(_snap.Object.Packs);

            _deck.CreateDeck();
            _deck2.CreateDeck();
            _deck2.ShuffleDeck();

            _card = _deck.DealCard();
            _card2 = _deck2.DealCard();

            int  differentCards=0;

            while (_card != null && _card2 != null)
            {
                if (!(_card.Value == _card2.Value && _card.Suite == _card2.Suite))
                    ++differentCards;

                _card = _deck.DealCard();
                _card2 = _deck2.DealCard();
            }

            Assert.False(differentCards == 0);
            Assert.True(_deck.NoOfCardsInPlay == 52);
            Assert.True(_deck2.NoOfCardsInPlay == 52);

            string shuffledStatus = string.Empty;

            if (differentCards >= 25)
                shuffledStatus = "Well Shuffled";
            else if (differentCards >= 10 && differentCards < 25)
                 shuffledStatus = "Shuffled";
            else if (differentCards <10)
                shuffledStatus = "Hardly Shuffled";



            TestContext.Out.WriteLine($"Pack is {shuffledStatus}");
        }

    }
}


