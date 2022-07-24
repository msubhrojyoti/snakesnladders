using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Tests
{
    public class BasicSnakesAndLaddersSimulationsTest : BaseTest
    {
        private IGame _game;
        private IBoard _board;
        private IList<ICharacter> _characters;
        private IAdvancer _dice;

        [SetUp]
        public void Setup()
        {
            _characters = new List<ICharacter>()
            {
                CharacterFactory.CreateCharacter(Character.Snake, 20, 5),
                CharacterFactory.CreateCharacter(Character.Snake, 50, 25),
                CharacterFactory.CreateCharacter(Character.Snake, 80, 35),
                CharacterFactory.CreateCharacter(Character.Ladder, 10, 30),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 70),
                CharacterFactory.CreateCharacter(Character.Ladder, 65, 90)
            };
            
            _board = BoardFactory.CreateBoard(100);
            _dice = AdvancerFactory.CreateAdvancer(1, 6);
        }

        [Test]
        [TestCase(1, new[]{"Josh"})]
        [TestCase(5, new[] { "Josh", "Don", "Harry" })]
        public void Should_do_simulations(int simulation, string[] playersNames)
        {
            var minimumNoOfRollsToWin = new List<int>();
            var amountOfClimbs = new List<int>();
            var amountOfSlides = new List<int>();
            var biggestClimbInASingleTurn = new List<int>();
            var biggestSlideInASingleTurn = new List<int>();
            var longestTurn = new List<List<int>>();
            var unluckyRolls = new List<int>();
            var luckyRolls = new List<int>();
            var winnersInOrder = new List<string>();

            while (simulation-- >=0 )
            {
                var players = playersNames.Select(PlayerFactory.CreatePlayer).ToList();
                _game = GameFactory.CreateGame(
                    Game.BasicSnakesAndLadder,
                    _board,
                    players,
                    _characters,
                    _dice, 
                    StatsFactory.CreateStats(players, _logger),
                    _logger);

                StringBuilder winnerBuilder = new StringBuilder();
                _game.OnWinner += (p) =>
                {
                    winnerBuilder.Append(p);
                    winnerBuilder.Append(", ");
                };

                _game.Play();

                minimumNoOfRollsToWin.Add(_game.GetGameStats().GetMinimumRollsForWin(_characters, _board.Size));
                foreach (var player in players)
                {
                    amountOfClimbs.Add(_game.GetGameStats().GetAmountOfClimbs(player));
                    amountOfSlides.Add(_game.GetGameStats().GetAmountOfSlides(player));
                    biggestClimbInASingleTurn.Add(_game.GetGameStats().GetBiggestClimbInATurn(player));
                    biggestSlideInASingleTurn.Add(_game.GetGameStats().GetBiggestSlideInATurn(player));
                    longestTurn.Add(_game.GetGameStats().GetLongestTurn(player).ToList());
                    luckyRolls.Add(_game.GetGameStats().GetLuckyRolls(player));
                    unluckyRolls.Add(_game.GetGameStats().GetUnluckyRolls(player));
                }

                winnersInOrder.Add(winnerBuilder.ToString());
            }

            minimumNoOfRollsToWin.Should().HaveCountGreaterThan(1);
            amountOfClimbs.Should().HaveCountGreaterThan(1);
            amountOfSlides.Should().HaveCountGreaterThan(1);
            biggestClimbInASingleTurn.Should().HaveCountGreaterThan(1);
            biggestSlideInASingleTurn.Should().HaveCountGreaterThan(1);
            longestTurn.Should().HaveCountGreaterThan(1);
            unluckyRolls.Should().HaveCountGreaterThan(1);
            luckyRolls.Should().HaveCountGreaterThan(1);

            Calculate(minimumNoOfRollsToWin, out var min, out var max, out var avg);
            _logger.Information($"Simulation result of minimum rolls to win: Min: {min}, Max: {max}, Avg: {avg}");

            Calculate(amountOfClimbs, out min, out max, out avg);
            _logger.Information($"Simulation result of amount of climbs: Min: {min}, Max: {max}, Avg: {avg}");

            Calculate(amountOfSlides, out min, out max, out avg);
            _logger.Information($"Simulation result of amount of slides: Min: {min}, Max: {max}, Avg: {avg}");

            Calculate(biggestClimbInASingleTurn, out min, out max, out avg);
            _logger.Information($"Simulation result of biggest climb in a single turn: Min: {min}, Max: {max}, Avg: {avg}");

            Calculate(biggestSlideInASingleTurn, out min, out max, out avg);
            _logger.Information($"Simulation result of biggest slide in a single turn: Min: {min}, Max: {max}, Avg: {avg}");

            Calculate(unluckyRolls, out min, out max, out avg);
            _logger.Information($"Simulation result of unlucky rolls: Min: {min}, Max: {max}, Avg: {avg}");

            Calculate(luckyRolls, out min, out max, out avg);
            _logger.Information($"Simulation result of lucky rolls: Min: {min}, Max: {max}, Avg: {avg}");

            _logger.Information($"Simulation result of longest turn: {string.Join(',', longestTurn.OrderByDescending(x => x.Sum()).First())}");
            
            _logger.Information($"Simulation result of winners in order: {string.Join(',', winnersInOrder)}");
        }

        private void Calculate(IList<int> input, out int min, out int max, out double avg)
        {
            min = input.Min();
            max = input.Max();
            avg = input.Average();
        }
    }
}