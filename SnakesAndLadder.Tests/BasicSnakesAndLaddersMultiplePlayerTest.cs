using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Tests
{
    public class BasicSnakesAndLaddersMultiplePlayerTest : BaseTest
    {
        private IGame _game;
        private IList<IPlayer> _players;
        private IBoard _board;
        private IList<ICharacter> _characters;
        private IAdvancer _dice;
        private IGameStats _stats;

        [SetUp]
        public void Setup()
        {
            _players = new List<IPlayer>()
            {
                PlayerFactory.CreatePlayer("James"),
                PlayerFactory.CreatePlayer("Harry"),
                PlayerFactory.CreatePlayer("Peter")
            };

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
            _stats = StatsFactory.CreateStats(_players, _logger);
            _game = GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, _characters, _dice, _stats, _logger);
        }

        [TearDown]
        public void Teardown()
        {
            _game.Dispose();
        }

        [Test]
        public void Should_have_winners()
        {
            // arrange
            List<IPlayer> winners = new List<IPlayer>();
            _game.OnWinner += (player) => winners.Add(player);

            // act
            _game.Play();

            // assert
            winners.Should().HaveCount(3);
            winners.Should().Contain(_players);
        }
    }
}