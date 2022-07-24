namespace SnakesAndLadders.Core.Interfaces
{
    public interface IPlayer
    {
        int Position { get; set; }
        bool IsWinner(IBoard board);
    }
}
