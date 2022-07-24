using Serilog.Core;
using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    /// <summary>
    /// Factory to create Stats
    /// </summary>
    public static class StatsFactory
    {
        public static IGameStats CreateStats(IEnumerable<IPlayer> players, Logger logger)
        {
            return new GameStats(players, logger);
        }
    }
}
