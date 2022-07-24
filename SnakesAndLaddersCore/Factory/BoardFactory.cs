using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    /// <summary>
    /// Factory to create Board
    /// </summary>
    public static class BoardFactory
    {
        public static IBoard CreateBoard(int size)
        {
            return new Board(size);
        }
    }
}
