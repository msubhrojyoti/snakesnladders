using SnakesAndLadders.Core.Factory;

namespace SnakesAndLadders.Core.Interfaces
{
    public interface ICharacter
    {
        /// <summary>
        /// Type of character - Snake, Ladder, Trap (extension)
        /// </summary>
        Character Type { get; }

        /// <summary>
        /// Validate character positions
        /// </summary>
        void Validate();

        /// <summary>
        /// Start point of character
        /// </summary>
        int Start { get; }

        /// <summary>
        /// End point for character
        /// </summary>
        int End { get; }

        /// <summary>
        /// Length of the character
        /// </summary>
        int Distance { get; }
    }
}
