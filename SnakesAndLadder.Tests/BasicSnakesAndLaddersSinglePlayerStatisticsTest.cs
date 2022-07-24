using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Tests
{
    public class BasicSnakesAndLaddersSinglePlayerStatisticsTest : BaseTest
    {
        private IList<IPlayer> _players;
        private IBoard _board;
        private IAdvancer _dice;
        private IGameStats _stats;
        private const string PlayerName = "Alpha";

        [SetUp]
        public void Setup()
        {
            _players = new List<IPlayer>()
            {
                PlayerFactory.CreatePlayer(PlayerName)
            };

            _board = BoardFactory.CreateBoard(100);
            _dice = AdvancerFactory.CreateAdvancer(1, 6);
            _stats = StatsFactory.CreateStats(_players, _logger);
        }

        [Test]
        public void Should_capture_biggest_climb_in_a_turn()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 50, 10),
                CharacterFactory.CreateCharacter(Character.Snake, 90, 30),
                CharacterFactory.CreateCharacter(Character.Ladder, 15, 25),
                CharacterFactory.CreateCharacter(Character.Ladder, 37, 80),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85)
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();
            rolls.Enqueue(4);
            rolls.Enqueue(5);
            rolls.Enqueue(6); // ladder 15 to 25 (part of biggest climb) 10

            rolls.Enqueue(6);
            rolls.Enqueue(6); // ladder 37 to 80 (part of biggest climb) 43

            rolls.Enqueue(5);
            rolls.Enqueue(5); // snake 85 to 30

            rolls.Enqueue(5);
            rolls.Enqueue(5);
            rolls.Enqueue(5); // ladder 45 to 85

            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(3); // win

            mockDice.Setup(x => x.Next()).Returns(()=> rolls.Dequeue());

            var stats = StatsFactory.CreateStats(_players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetBiggestClimbInATurn(_players.Single()).Should().Be(53);
        }

        [Test]
        public void Should_capture_biggest_slide_in_a_turn()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 90, 50),
                CharacterFactory.CreateCharacter(Character.Snake, 62, 30),
                CharacterFactory.CreateCharacter(Character.Snake, 34, 6),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85),
                CharacterFactory.CreateCharacter(Character.Ladder, 7, 92),
                CharacterFactory.CreateCharacter(Character.Ladder, 55, 84)
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();

            for (var i = 0; i < 14; i++) // to reach 84
            {
                rolls.Enqueue(6);
            }

            rolls.Enqueue(3);
            rolls.Enqueue(3); // snake 90 to 50

            rolls.Enqueue(5); // ladder 55 to 84
            rolls.Enqueue(6); // snake 90 to 50 (part of biggest slide) 40

            rolls.Enqueue(6);
            rolls.Enqueue(6); // snake 62 to 30 (part of biggest slide) 32
            rolls.Enqueue(4); // snake 34 to 6 (part of biggest slide) 28

            rolls.Enqueue(1);
            rolls.Enqueue(5); // ladder 7 to 92

            rolls.Enqueue(5);
            rolls.Enqueue(3); // win

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(_players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetBiggestSlideInATurn(_players.Single()).Should().Be(100);
        }

        [Test]
        public void Should_capture_maximum_amount_of_climbs()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 50, 10),
                CharacterFactory.CreateCharacter(Character.Snake, 90, 30),
                CharacterFactory.CreateCharacter(Character.Ladder, 15, 25),
                CharacterFactory.CreateCharacter(Character.Ladder, 37, 80),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85)
            };

            var players = new List<IPlayer>
            {
                PlayerFactory.CreatePlayer("John"),
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();
            rolls.Enqueue(4);
            rolls.Enqueue(5);
            rolls.Enqueue(6); // ladder 15 to 25 (climb) 10

            rolls.Enqueue(6);
            rolls.Enqueue(6); // ladder 37 to 80 (climb) 43

            rolls.Enqueue(5);
            rolls.Enqueue(5); // snake 85 to 30

            rolls.Enqueue(5);
            rolls.Enqueue(5);
            rolls.Enqueue(5); // ladder 45 to 85 (climb) 40

            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(3); // win

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetAmountOfClimbs(players.Single()).Should().Be(93);
        }

        [Test]
        public void Should_capture_maximum_amount_of_slides()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 90, 50),
                CharacterFactory.CreateCharacter(Character.Snake, 62, 30),
                CharacterFactory.CreateCharacter(Character.Snake, 34, 6),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85),
                CharacterFactory.CreateCharacter(Character.Ladder, 7, 92),
                CharacterFactory.CreateCharacter(Character.Ladder, 55, 84)
            };

            var players = new List<IPlayer>
            {
                PlayerFactory.CreatePlayer("John"),
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();

            for (var i = 0; i < 14; i++) // to reach 84
            {
                rolls.Enqueue(6);
            }

            rolls.Enqueue(3);
            rolls.Enqueue(3); // snake 90 to 50 (slide) 40

            rolls.Enqueue(5); // ladder 55 to 84
            rolls.Enqueue(6); // snake 90 to 50 (slide) 40

            rolls.Enqueue(6);
            rolls.Enqueue(6); // snake 62 to 30 (slide) 32
            rolls.Enqueue(4); // snake 34 to 6 (slide) 28

            rolls.Enqueue(1);
            rolls.Enqueue(5); // ladder 7 to 92

            rolls.Enqueue(5);
            rolls.Enqueue(3); // win

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetAmountOfSlides(players.Single()).Should().Be(140);
        }

        [Test]
        public void Should_capture_longest_turns()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 90, 50),
                CharacterFactory.CreateCharacter(Character.Snake, 62, 30),
                CharacterFactory.CreateCharacter(Character.Snake, 34, 6),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85),
                CharacterFactory.CreateCharacter(Character.Ladder, 7, 92),
                CharacterFactory.CreateCharacter(Character.Ladder, 55, 84)
            };

            var players = new List<IPlayer>
            {
                PlayerFactory.CreatePlayer("John"),
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();

            rolls.Enqueue(4);
            rolls.Enqueue(2);
            rolls.Enqueue(1);
            rolls.Enqueue(5);
            for (var i = 0; i < 12; i++) // to reach 84
            {
                rolls.Enqueue(6);
            }

            rolls.Enqueue(3);
            rolls.Enqueue(3);

            rolls.Enqueue(5);
            rolls.Enqueue(6);

            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(4);

            rolls.Enqueue(1);
            rolls.Enqueue(5);

            rolls.Enqueue(5);
            rolls.Enqueue(3);

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            var expected = new List<int>();
            for (var i = 0; i < 12; i++) // to reach 84
            {
                expected.Add(6);
            }

            expected.Add(3);

            game.GetGameStats().GetLongestTurn(players.Single()).ToArray().Should().BeEquivalentTo(expected.ToArray());
        }

        [Test]
        public void Should_capture_unlucky_rolls()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 90, 50),
                CharacterFactory.CreateCharacter(Character.Snake, 62, 30),
                CharacterFactory.CreateCharacter(Character.Snake, 34, 6),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85),
                CharacterFactory.CreateCharacter(Character.Ladder, 7, 92),
                CharacterFactory.CreateCharacter(Character.Ladder, 55, 84)
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();

            for (var i = 0; i < 14; i++) // to reach 84
            {
                rolls.Enqueue(6);
            }

            rolls.Enqueue(3);
            rolls.Enqueue(3); // snake

            rolls.Enqueue(5); // ladder
            rolls.Enqueue(6); // snake

            rolls.Enqueue(6);
            rolls.Enqueue(6); // snake
            rolls.Enqueue(4); // snake

            rolls.Enqueue(1);
            rolls.Enqueue(5); // ladder

            rolls.Enqueue(5);
            rolls.Enqueue(3); // win

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(_players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetUnluckyRolls(_players.Single()).Should().Be(4);
        }

        [Test]
        public void Should_capture_lucky_rolls()
        {
            // arrange
            var characters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 90, 50),
                CharacterFactory.CreateCharacter(Character.Snake, 98, 66),
                CharacterFactory.CreateCharacter(Character.Snake, 60, 30),
                CharacterFactory.CreateCharacter(Character.Snake, 20, 6),
                CharacterFactory.CreateCharacter(Character.Ladder, 10, 55),
                CharacterFactory.CreateCharacter(Character.Ladder, 68, 85),
                CharacterFactory.CreateCharacter(Character.Ladder, 35, 95)
            };

            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();

            for (var i = 0; i < 9; i++) // to reach 54
            {
                rolls.Enqueue(6);
            }

            rolls.Enqueue(3);
            rolls.Enqueue(3); // snake 60 to 30

            rolls.Enqueue(5); // ladder 35 to 95 (lucky)
            rolls.Enqueue(3); // snake 98 to 66

            rolls.Enqueue(2); // ladder 68 to 85 (lucky)
            rolls.Enqueue(3); // 2 less than snake at 90 (lucky)

            rolls.Enqueue(1); // 1 less than snake at 90 (lucky)
            rolls.Enqueue(5); // 94
            rolls.Enqueue(6); // winner exact (lucky)

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(_players, _logger);

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, characters, mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetLuckyRolls(_players.Single()).Should().Be(5);
        }

        [Test]
        public void Test_minimum_turns_for_win_with_multiple_minimum_wins()
        {
            // arrange
            var charactersWithMultipleMinimums = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 20, 5),
                CharacterFactory.CreateCharacter(Character.Snake, 50, 25),
                CharacterFactory.CreateCharacter(Character.Snake, 80, 35),

                //2 rolls (10) + 3 rolls (45) + 5 rolls (100) = 10
                //2 rolls (10) + 6 rolls (65) + 2 rolls (100) = 10
                CharacterFactory.CreateCharacter(Character.Ladder, 10, 30),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 70),
                CharacterFactory.CreateCharacter(Character.Ladder, 65, 90)
            };

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, charactersWithMultipleMinimums, _dice, _stats, _logger);
            
            // assert
            game.GetGameStats().GetMinimumRollsForWin(charactersWithMultipleMinimums, _board.Size).Should().Be(10);
        }

        [Test]
        public void Test_minimum_turns_for_win_with_single_minimum_wins()
        {
            // arrange
            var charactersWithSingleMinimums = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 20, 5),
                CharacterFactory.CreateCharacter(Character.Snake, 50, 25),
                CharacterFactory.CreateCharacter(Character.Snake, 80, 35),

                //2 rolls (10) + 3 rolls (45) + 3 rolls (100) = 8
                //2 rolls (10) + 6 rolls (65) + 2 rolls (100) = 10
                CharacterFactory.CreateCharacter(Character.Ladder, 10, 30),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85),
                CharacterFactory.CreateCharacter(Character.Ladder, 65, 90)
            };

            // act
            var game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, charactersWithSingleMinimums, _dice, _stats, _logger);

            // assert
            game.GetGameStats().GetMinimumRollsForWin(charactersWithSingleMinimums, _board.Size).Should().Be(8);
        }
    }
}