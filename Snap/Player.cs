using System;
using System.Collections.Generic;
using System.Text;

namespace Snap
{
    public class Player : IPlayer
    {
        //Can add more player details
        public Player(string name) => Name = name;
        public string Name { get; set; }
        public int Wins { get; set; }
        public int CardsWon { get; set; }
     }
}
