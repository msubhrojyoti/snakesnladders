using Serilog.Core;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Games
{
    /// <summary>
    /// Variation of Main Snakes and Ladders Game. Traps will keep the player in the current position (they will not advance since there is a trap)
    /// </summary>
    public class SnakesAndLaddersWithTraps : BasicSnakesAndLadders
    {
        public SnakesAndLaddersWithTraps(IBoard board, IEnumerable<IPlayer> players, IEnumerable<ICharacter> characters, IAdvancer advancer, IGameStats stats, Logger logger) : base(board, players, characters, advancer, stats, logger)
        {
        }
        protected override void ExecuteRoll(IPlayer player, int roll)
        {
            var newPos = player.Position + roll;
            if (newPos <= _board.Size)
            {
                if (_characters.ContainsKey(newPos))
                {
                    if (_characters[newPos].Type == Character.Trap)
                    {
                        _stats.UpdateTotalTraps(player);
                        
                        // its a trap (turn is wasted)
                        return;
                    }

                    // snake or a ladder
                    _singleTurnClimbsSlidesTracker.Add(_characters[newPos]);
                    _stats.UpdateUnluckyRoll(_characters[newPos], player, roll);
                    _stats.UpdateAmountOfSlidesAndClimbs(_characters[newPos], player);

                    player.Position = _characters[newPos].End;
                }
                else
                {
                    // non character position
                    _logger.Debug($"'{player}' moving from '{player.Position}' to '{newPos}'");
                    player.Position = newPos;
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                base.Dispose(false);
            }
        }
    }
}
