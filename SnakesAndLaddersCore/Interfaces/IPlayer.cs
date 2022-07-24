namespace SnakesAndLadders.Core.Interfaces
{
    /// <summary>
    /// Interface for players
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Get player position int the board
        /// </summary>
        int Position { get; set; }

        /// <summary>
        /// Is player a winner
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        bool IsWinner(IBoard board);
    }
}
