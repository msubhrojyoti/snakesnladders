using Serilog.Core;
using SnakesAndLadders.Core.GameVariations;
using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    public enum Game
    {
        BasicSnakesAndLadder,
        SnakesAndLadderWithTrap
    }

    public static class GameFactory
    {
        public delegate void NotifyWinner(IPlayer player);

        public static IGame CreateGame(Game type, IBoard board, IEnumerable<IPlayer> players, IEnumerable<ICharacter> characters, IAdvancer advancer, IGameStats stats, Logger logger)
        {
            switch (type)
            {
                case Game.BasicSnakesAndLadder:
                    return new BasicSnakesAndLadders(board, players, characters, advancer, stats, logger);
                case Game.SnakesAndLadderWithTrap:
                    return new SnakesAndLaddersWithTraps(board, players, characters, advancer, stats, logger);
                default:
                    throw new ArgumentException($"Unsupported type of Game: {nameof(type)}");
            }
            
        }
    }
}
