using System;

namespace Snap
{
    class Program
    {
        static void Main(string[] args)
        {
            IPlayer player1 = new Player { Name = "Player1" };
            IPlayer player2 = new Player { Name = "Player2" };

            ISnap snapGame = new Snap(player1, player2);

            snapGame.Initialise();
            snapGame.Play();
            snapGame.PrintResult();
        }
    }
}
