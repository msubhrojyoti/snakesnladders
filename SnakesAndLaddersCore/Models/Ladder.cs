using SnakesAndLadders.Core.Exceptions;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class Ladder : ICharacter
    {
        public Character Type { get; set; } = Character.Ladder;
        public int Start { get; }
        public int End { get; }
        public int Distance { get; }

        public Ladder(int start, int end)
        {
            Start = start;
            End = end;
            Distance = End - Start;
        }

        public void Validate()
        {
            if (End < Start)
            {
                throw new InvalidCharacterPositionException(
                    $"End '{End}' must be greater than start '{Start}' for a {Type}");
            }
        }

        public override string ToString()
        {
            return nameof(Type);
        }
    }
}
