using SnakesAndLadders.Core.Exceptions;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class Trap : ICharacter
    {
        public Character Type { get; set; } = Character.Trap;
        public int Start { get; }
        public int End { get; }
        public int Distance { get; }

        public Trap(int start, int end)
        {
            Start = start;
            End = end;
            Distance = End - Start;
        }

        public void Validate()
        {
            if (Start != End)
            {
                throw new InvalidCharacterPositionException(
                    $"Start '{Start}' must be equal to end '{End}' for a {nameof(Type)}");
            }
        }

        public override string ToString()
        {
            return nameof(Type);
        }
    }
}
