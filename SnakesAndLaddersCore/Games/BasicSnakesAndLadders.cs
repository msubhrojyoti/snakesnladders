using Serilog.Core;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Games
{
    public class BasicSnakesAndLadders : IGame
    {
        protected readonly IEnumerable<IPlayer> _players;
        protected readonly Dictionary<int, ICharacter> _characters;
        protected readonly IBoard _board;
        protected readonly IAdvancer _advancer;
        protected readonly IGameStats _stats;
        protected readonly Logger _logger;
        protected readonly List<ICharacter> _singleTurnClimbsSlidesTracker = new();

        public BasicSnakesAndLadders(
            IBoard board,
            IEnumerable<IPlayer> players,
            IEnumerable<ICharacter> characters,
            IAdvancer advancer,
            IGameStats stats,
            Logger logger)
        {
            _players = players;

            _characters = characters.ToDictionary(k=> k.Start, v =>
            {
                v.Validate();
                return v;
            });

             _board = board;
            _advancer = advancer;
            _stats = stats;
            _logger = logger;
        }

        public virtual int Roll()
        {
            return _advancer.Next();
        }

        public IGameStats GetGameStats()
        {
            return _stats;
        }

        public event GameFactory.NotifyWinner OnWinner;

        public virtual void Play()
        {
            while (_players.Any(x => !x.IsWinner(_board)))
            {
                foreach (var player in _players.Where(x => !x.IsWinner(_board)))
                {
                    _singleTurnClimbsSlidesTracker.Clear();
                    int roll;
                    var longestTurn = new List<int>();
                    do
                    {
                        roll = Roll();

                        _logger.Debug($"'{player}' rolled a '{roll}'");

                        // play the roll
                        ExecuteRoll(player, roll);

                        _stats.UpdateLuckyRoll(_characters, player, roll, _board.Size);
                        _stats.UpdateTotalRolls(player);

                        longestTurn.Add(roll);

                        if (player.IsWinner(_board))
                        {
                            // winner
                            OnWinner?.Invoke(player);
                            _logger.Information($"=====(^_^) Player '{player}' is winner.(^_^)====");
                            break;
                        }
                    } while (roll == 6);

                    _stats.UpdateLongestTurn(player, longestTurn);

                    if (_singleTurnClimbsSlidesTracker.Count > 0)
                    {
                        _stats.UpdateBiggestClimbOrSlideInASingleTurn(_singleTurnClimbsSlidesTracker, player);
                    }
                }
            }
        }

        protected virtual void ExecuteRoll(IPlayer player, int roll)
        {
            var newPos = player.Position + roll;
            if (newPos <= _board.Size)
            {
                if (_characters.ContainsKey(newPos))
                {
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _logger.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Disposable types implement a finalizer.
        ~BasicSnakesAndLadders()
        {
            Dispose(false);
        }
    }
}
