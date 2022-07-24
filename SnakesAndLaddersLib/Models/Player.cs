using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class Player : IPlayer
    {
        private readonly string _name;
        
        public Player(string name)
        {
            _name = name;
        }

        public int Position { get; set; }

        public bool IsWinner(IBoard board)
        {
            return Position == board.Size;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
