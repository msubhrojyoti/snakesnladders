using Serilog.Core;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class GameStats : IGameStats
    {
        private class BoardPosition
        {
            public int Position { get; set; }
            public int DistanceFromStart { get; set; }
        }

        private readonly Logger _logger;
        private readonly Dictionary<string, Statistic> _stats;
        public GameStats(IEnumerable<IPlayer> players, Logger logger)
        {
            _stats = players.ToDictionary(k => k.ToString(), v => new Statistic());
            _logger = logger;
        }

        public void UpdateAmountOfSlidesAndClimbs(ICharacter character, IPlayer player)
        {
            if (character.Type == Character.Snake)
            {
                _logger.Information($"{nameof(character.Type)} moving '{player}' from '{player.Position}' to '{character.End}'");

                _stats[$"{player}"].AmountOfSlides += character.Distance;
            }

            if (character.Type == Character.Ladder)
            {
                _logger.Information($"{nameof(character.Type)} moving '{player}' from '{player.Position}' to '{character.End}'");

                _stats[$"{player}"].AmountOfClimbs += character.Distance;
            }
        }

        public void UpdateBiggestClimbOrSlideInASingleTurn(IEnumerable<ICharacter> characters, IPlayer player)
        {
            var climbs = 0;
            var slides = 0;

            foreach (var character in characters)
            {
                if (character.Type == Character.Ladder)
                {
                    climbs += character.Distance;
                }

                if (character.Type == Character.Snake)
                {
                    slides += character.Distance;
                }
            }

            _stats[$"{player}"].BiggestClimbInATurn = Math.Max(_stats[$"{player}"].BiggestClimbInATurn, climbs);
            _stats[$"{player}"].BiggestSlideInATurn = Math.Max(_stats[$"{player}"].BiggestSlideInATurn, slides);
        }

        public void UpdateLongestTurn(IPlayer player, IList<int> rolls)
        {
            if (_stats[$"{player}"].LongestTurn.Sum() < rolls.Sum())
            {
                _stats[$"{player}"].LongestTurn = rolls;
            }
        }

        public void UpdateUnluckyRoll(ICharacter character, IPlayer player, int roll)
        {
            if (character.Type == Character.Snake)
            {
                _logger.Debug($"'{player}' is unlucky with roll '{roll}'");
                _stats[$"{player}"].UnluckyRolls++;
            }
        }

        public void UpdateLuckyRoll(IDictionary<int, ICharacter> characters, IPlayer player, int roll, int boardSize)
        {
            var isLuckyRoll = false;
            var currentPos = player.Position;
            for (var i = 1; i < 3; i++)
            {
                if (characters.ContainsKey(currentPos - i) && characters[currentPos - i].Type == Character.Snake)
                {
                    isLuckyRoll = true;
                    break;
                }

                if (characters.ContainsKey(currentPos + i) && characters[currentPos + i].Type == Character.Snake)
                {
                    isLuckyRoll = true;
                    break;
                }
            }

            if (!isLuckyRoll)
            {
                isLuckyRoll = boardSize == currentPos + roll;
            }

            if (isLuckyRoll)
            {
                _logger.Debug($"'{player}' is lucky with roll '{roll}'");
                _stats[$"{player}"].LuckyRolls++;
            }
        }

        public void UpdateTotalRolls(IPlayer player)
        {
            _stats[$"{player}"].Rolls++;
        }

        public int GetTotalRolls(IPlayer player)
        {
            return _stats[$"{player}"].Rolls;
        }

        public int GetAmountOfClimbs(IPlayer player)
        {
            return _stats[$"{player}"].AmountOfClimbs;
        }

        public int GetAmountOfSlides(IPlayer player)
        {
            return _stats[$"{player}"].AmountOfSlides;
        }

        public IEnumerable<int> GetLongestTurn(IPlayer player)
        {
            return _stats[$"{player}"].LongestTurn;
        }

        public int GetUnluckyRolls(IPlayer player)
        {
            return _stats[$"{player}"].UnluckyRolls;
        }

        public int GetLuckyRolls(IPlayer player)
        {
            return _stats[$"{player}"].LuckyRolls;
        }

        public int GetBiggestClimbInATurn(IPlayer player)
        {
            return _stats[$"{player}"].BiggestClimbInATurn;
        }

        public int GetBiggestSlideInATurn(IPlayer player)
        {
            return _stats[$"{player}"].BiggestSlideInATurn;
        }

        public int GetMinimumRollsForWin(IList<ICharacter> characters, int boardSize)
        {
            var visited = new bool[boardSize];

            // pos and distance from start
            var node = new BoardPosition()
            {
                DistanceFromStart = 0,
                Position = 0
            };

            var queue = new Queue<BoardPosition>();
            queue.Enqueue(node);

            visited[0] = true;
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                if (node.Position == boardSize - 1)
                {
                    break;
                }

                for (var j = node.Position + 1; j <= node.Position + 6 && j < boardSize; ++j)
                {
                    if (!visited[j])
                    {
                        visited[j] = true;
                        var character = characters.SingleOrDefault(x => x.Start == j);
                        var newNode = new BoardPosition
                        {
                            DistanceFromStart = node.DistanceFromStart + 1,
                            Position = character?.End ?? j
                        };

                        queue.Enqueue(newNode);
                    }
                }
            }

            return node.DistanceFromStart;
        }

        public int UpdateTotalTraps(IPlayer player)
        {
            return _stats[$"{player}"].TotalTraps++;
        }

        public int GetTotalTraps(IPlayer player)
        {
            return _stats[$"{player}"].TotalTraps;
        }
    }
}
