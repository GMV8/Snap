using System;
using System.Collections.Generic;
using System.Text;

namespace Snap
{
    public enum SnapConditions { SAME_SUITE=1, SAME_VALUE, SAME_SUITE_AND_VALUE }
    public class Snap : ISnap
    {
        public int Packs { get; set; }
        public SnapConditions SnapMethod { get; set; }
        private readonly Random _randomPlayer = new Random();

        private IPlayer _player1, _player2;
        public Snap(IPlayer player1, IPlayer player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
        public void Initialise()
        {
            Console.WriteLine("================ Snap =================");
            int packs;
            do
            {
                Console.WriteLine("Enter the number of packs to use");
                if (!int.TryParse(Console.ReadLine(), out packs))
                    Console.WriteLine("Please enter a number greater than 0");
            } while (packs == 0);
            Packs = packs;

            SnapConditions snapMethod;
            do
            {
                Console.WriteLine("Enter the Snap Method to use :-");
                Console.WriteLine("\t1) Declare Snap by drawing the same suit");
                Console.WriteLine("\t2) Declare Snap by drawing the same value card");
                Console.WriteLine("\t3) Declare Snap by drawing the same suit and value card");
                if (!(Enum.TryParse(Console.ReadLine(), out snapMethod) && Enum.IsDefined(typeof(SnapConditions), snapMethod)))
                    Console.WriteLine("Please enter a number between 1 and 3");
                else
                    break;
            } while (true);
            SnapMethod = snapMethod;
        }

        bool IsSnap(SnapConditions snapMethod, ICard card1, ICard card2)
        {
            switch (snapMethod)
            {
                case SnapConditions.SAME_SUITE:
                    return (card1.Suite == card2.Suite);
                case SnapConditions.SAME_VALUE:
                    return (card1.Value == card2.Value);
                case SnapConditions.SAME_SUITE_AND_VALUE:
                    return (card1.Suite == card2.Suite && card1.Value == card2.Value);
                default:
                    throw new CardException("Invalid Snap Method");
            }
        }

        private IPlayer WinningPlayer { get => _randomPlayer.Next(1000000) % 2 == 0 ? _player1 : _player2; }

        public void Play()
        {
            IDeck deck = new DeckofCards(Packs);


            deck.CreateDeck();
            deck.ShuffleDeck();

            ICard card1 = deck.DealCard();
            ICard card2 = deck.DealCard();

            while (card1 != null  || card2 != null)
            {
                if (IsSnap(SnapMethod, card1, card2))
                {
                        WinningPlayer.CardsWon += deck.NoOfCardsInPlay;
                        ++WinningPlayer.Wins;

                    deck.ClearCardsInPlay();
                }

                card1 = deck.DealCard();
                card2 = deck.DealCard();
            }

        }

        public void PrintResult()
        {
            if (_player1.CardsWon > _player2.CardsWon)
                Console.WriteLine($"Player1 wins by {_player1.CardsWon} cards to {_player2.CardsWon} cards !");
            else if (_player1.CardsWon < _player2.CardsWon)
                Console.WriteLine($"Player2 wins by {_player2.CardsWon} cards to {_player1.CardsWon} cards !");
            else
                Console.WriteLine($"It is a draw by {_player1.CardsWon} cards to {_player2.CardsWon} cards !");
        }
    }
}
