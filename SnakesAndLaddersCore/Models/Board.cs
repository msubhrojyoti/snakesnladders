using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class Board : IBoard
    {
        public Board(int size)
        {
            Size = size;
        }

        public int Size { get; }
    }
}
