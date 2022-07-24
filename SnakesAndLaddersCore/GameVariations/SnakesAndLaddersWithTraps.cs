using Serilog.Core;
using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.GameVariations
{
    public class SnakesAndLaddersWithTraps : BasicSnakesAndLadders
    {
        public SnakesAndLaddersWithTraps(IBoard board, IEnumerable<IPlayer> players, IEnumerable<ICharacter> characters, IAdvancer advancer, IGameStats stats, Logger logger) : base(board, players, characters, advancer, stats, logger)
        {
        }
    }
}
