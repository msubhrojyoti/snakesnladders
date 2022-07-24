using SnakesAndLadders.Core.Factory;

namespace SnakesAndLadders.Core.Interfaces
{
    /// <summary>
    /// Interface for game
    /// </summary>
    public interface IGame : IDisposable
    {
        /// <summary>
        /// Event to notify of winners
        /// </summary>
        event GameFactory.NotifyWinner OnWinner;

        /// <summary>
        /// Start playing the game
        /// </summary>
        void Play();

        /// <summary>
        /// Roll (from the advancer)
        /// </summary>
        /// <returns></returns>
        int Roll();

        /// <summary>
        /// Get game stats
        /// </summary>
        /// <returns></returns>
        IGameStats GetGameStats();
    }
}
