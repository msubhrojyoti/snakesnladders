using SnakesAndLadders.Core.Exceptions;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class Snake : ICharacter
    {
        public Character Type { get; set; } = Character.Snake;
        public int Start { get; }
        public int End { get; }
        public int Distance { get; }

        public Snake(int start, int end)
        {
            Start = start;
            End = end;
            Distance = Start - End;
        }

        public void Validate()
        {
            if (Start < End)
            {
                throw new InvalidCharacterPositionException(
                    $"Start '{Start}' must be greater than end '{End}' for a {Type}");
            }
        }

        public override string ToString()
        {
            return nameof(Type);
        }
    }
}
