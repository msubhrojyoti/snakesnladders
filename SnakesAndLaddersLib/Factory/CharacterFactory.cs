using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    public enum Character
    {
        Snake,
        Ladder,
        Trap
    }

    public static class CharacterFactory
    {
        public static ICharacter CreateCharacter(Character type, int start, int end)
        {
            switch (type)
            {
                case Character.Ladder:
                    return new Ladder(start, end);
                case Character.Snake:
                    return new Snake(start, end);
                case Character.Trap:
                    return new Snake(start, end);
                default:
                    throw new ArgumentException($"Unsupported type of character: {nameof(type)}");
            }
        }
    }
}
