namespace Snap
{
    public interface IPlayer
    {
        string Name { get; set; }
        int Wins { get; set; }
        int CardsWon { get; set; }
    }
}