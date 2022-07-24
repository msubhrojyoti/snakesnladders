using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Tests
{
    public class SnakesAndLaddersWithTrapTest : BaseTest
    {
        private IList<IPlayer> _players;
        private IBoard _board;
        private IList<ICharacter> _characters;

        [SetUp]
        public void Setup()
        {
            _players = new List<IPlayer>()
            {
                PlayerFactory.CreatePlayer("James"),
            };

            _characters = new List<ICharacter>()
            {
                CharacterFactory.CreateCharacter(Character.Snake, 20, 5),
                CharacterFactory.CreateCharacter(Character.Snake, 50, 25),
                
                CharacterFactory.CreateCharacter(Character.Trap, 10, 10),
                CharacterFactory.CreateCharacter(Character.Trap, 15, 15),

                CharacterFactory.CreateCharacter(Character.Ladder, 45, 70),
                CharacterFactory.CreateCharacter(Character.Ladder, 65, 90)
            };
            
            _board = BoardFactory.CreateBoard(100);
        }

        [Test]
        public void Should_roll_on_trap()
        {
            // arrange
            var mockDice = new Mock<IAdvancer>();
            var rolls = new Queue<int>();
            rolls.Enqueue(5);
            rolls.Enqueue(5); // trap so it will be wasted (stays at 5)
            
            rolls.Enqueue(6);
            rolls.Enqueue(4); // trap so it will be wasted (stays at 11)

            rolls.Enqueue(5);
            rolls.Enqueue(5);
            rolls.Enqueue(5);
            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(1); // ladder 45 to 70

            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(6);
            rolls.Enqueue(3);
            rolls.Enqueue(3); // win

            mockDice.Setup(x => x.Next()).Returns(() => rolls.Dequeue());

            var stats = StatsFactory.CreateStats(_players, _logger);

            // act
            using var game = GameFactory.CreateGame(Game.SnakesAndLadderWithTrap, _board, _players, _characters,
                mockDice.Object, stats, _logger);
            game.Play();

            // assert
            game.GetGameStats().GetTotalTraps(_players.Single()).Should().Be(2);
        }
    }
}