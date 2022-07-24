using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SnakesAndLadders.Core.Exceptions;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Tests
{
    public class BasicSnakesAndLaddersSinglePlayerTest : BaseTest
    {
        private IGame _game;
        private IList<IPlayer> _players;
        private IBoard _board;
        private IList<ICharacter> _characters;
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

        [Test]
        public void Should_throw_exception_when_invalid_snake_position()
        {
            // arrange
            var invalidCharacters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 50, 10),
                
                //invalid snake position
                CharacterFactory.CreateCharacter(Character.Snake, 10, 30),
                
                CharacterFactory.CreateCharacter(Character.Ladder, 15, 25),
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 85)
            };

            // act
            var exception = Assert.Throws(typeof(InvalidCharacterPositionException), () =>
            {
                GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, invalidCharacters, _dice, _stats, _logger);
            });

            // assert
            Assert.IsNotNull(exception);
            exception.Message.Should().Contain(nameof(Character.Snake)).And.NotContain(nameof(Character.Ladder));
        }

        [Test]
        public void Should_throw_exception_when_invalid_ladder_position()
        {
            // arrange
            var invalidCharacters = new List<ICharacter>
            {
                CharacterFactory.CreateCharacter(Character.Snake, 20, 10),
                CharacterFactory.CreateCharacter(Character.Snake, 40, 25),
                CharacterFactory.CreateCharacter(Character.Ladder, 15, 65),
                
                //invalid one
                CharacterFactory.CreateCharacter(Character.Ladder, 45, 5)
            };

            // act
            var exception = Assert.Throws(typeof(InvalidCharacterPositionException), () =>
            {
                GameFactory.CreateGame(Game.BasicSnakesAndLadder, _board, _players, invalidCharacters, _dice, _stats, _logger);
            });

            // assert
            Assert.IsNotNull(exception);
            exception.Message.Should().Contain(nameof(Character.Ladder)).And.NotContain(nameof(Character.Snake));
        }

        [Test]
        public void Should_have_a_winner()
        {
            // arrange
            IPlayer winner = null;
            _game.OnWinner += (player) => winner = player;

            // act
            _game.Play();
            
            // assert
            winner.Should().NotBeNull();
            winner.ToString().Should().Be(PlayerName);
            winner.Position.Should().Be(_board.Size);
        }
    }
}