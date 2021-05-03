using System;
using System.Collections.Generic;
using System.Text;

namespace Snap
{
    public interface IDeck
    {
         int NoOfCardsInPlay { get; }
         IDeck CreateDeck ();
         IDeck ShuffleDeck ();
         ICard DealCard();

         void ClearCardsInPlay();
    }
}
