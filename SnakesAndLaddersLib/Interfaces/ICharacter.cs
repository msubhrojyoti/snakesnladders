using SnakesAndLadders.Core.Factory;

namespace SnakesAndLadders.Core.Interfaces
{
    public interface ICharacter
    {
        Character Type { get; }

        void Validate();

        int Start { get; }

        int End { get; }

        int Distance { get; }
    }
}
