using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    public static class BoardFactory
    {
        public static IBoard CreateBoard(int size)
        {
            return new Board(size);
        }
    }
}
