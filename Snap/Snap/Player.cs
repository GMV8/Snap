using System;
using System.Collections.Generic;
using System.Text;

namespace Snap
{
    class Player : IPlayer
    {
        //Can add more player details

        public string Name { get; set; }
        public int Wins { get; set; }
        public int CardsWon { get; set; }
     }
}
