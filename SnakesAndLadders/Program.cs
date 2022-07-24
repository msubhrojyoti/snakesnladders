// See https://aka.ms/new-console-template for more information

using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;
using SnakesAndLadders.Core.Factory;
using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

logger.Information("Welcome to Snakes and Ladders!");

var characters = new List<ICharacter>()
{
    CharacterFactory.CreateCharacter(Character.Snake, 24, 3),
    CharacterFactory.CreateCharacter(Character.Snake, 55, 18),
    CharacterFactory.CreateCharacter(Character.Snake, 61, 35),
    CharacterFactory.CreateCharacter(Character.Snake, 77, 27),
    CharacterFactory.CreateCharacter(Character.Snake, 96, 48),

    CharacterFactory.CreateCharacter(Character.Ladder, 4, 50),
    CharacterFactory.CreateCharacter(Character.Ladder, 13, 56),
    CharacterFactory.CreateCharacter(Character.Ladder, 33, 73),
    CharacterFactory.CreateCharacter(Character.Ladder, 51, 87),
    CharacterFactory.CreateCharacter(Character.Ladder, 66, 92)
};

var players = new List<IPlayer>()
{
    PlayerFactory.CreatePlayer("James"),
    PlayerFactory.CreatePlayer("Tom"),
    PlayerFactory.CreatePlayer("Harry")
};

var board = BoardFactory.CreateBoard(100);

var game = new BasicSnakesAndLadders(
    board,
    players, 
    characters,
    AdvancerFactory.CreateAdvancer(1, 6), 
    StatsFactory.CreateStats(players, logger), logger);

StringBuilder winnerBuilder = new StringBuilder();
game.OnWinner += (p) =>
{
    winnerBuilder.Append(p);
    winnerBuilder.Append(", ");
};

game.Play();

/*
 * Following stats are covered in tests. Here its same and only for console demonstration.
 */
var minimumNoOfRollsToWin = new List<int>();
var amountOfClimbs = new List<int>();
var amountOfSlides = new List<int>();
var biggestClimbInASingleTurn = new List<int>();
var biggestSlideInASingleTurn = new List<int>();
var longestTurn = new List<List<int>>();
var unluckyRolls = new List<int>();
var luckyRolls = new List<int>();
var winnersInOrder = new List<string>();

minimumNoOfRollsToWin.Add(game.GetGameStats().GetMinimumRollsForWin(characters, board.Size));
foreach (var player in players)
{
    amountOfClimbs.Add(game.GetGameStats().GetAmountOfClimbs(player));
    amountOfSlides.Add(game.GetGameStats().GetAmountOfSlides(player));
    biggestClimbInASingleTurn.Add(game.GetGameStats().GetBiggestClimbInATurn(player));
    biggestSlideInASingleTurn.Add(game.GetGameStats().GetBiggestSlideInATurn(player));
    longestTurn.Add(game.GetGameStats().GetLongestTurn(player).ToList());
    luckyRolls.Add(game.GetGameStats().GetLuckyRolls(player));
    unluckyRolls.Add(game.GetGameStats().GetUnluckyRolls(player));
}

winnersInOrder.Add(winnerBuilder.ToString());

logger.Information($"Single simulation result of minimum rolls to win: Min: {minimumNoOfRollsToWin.Min()}, Max: {minimumNoOfRollsToWin.Max()}, Avg: {minimumNoOfRollsToWin.Average()}");
logger.Information($"Single simulation result of amount of climbs: Min: {amountOfClimbs.Min()}, Max: {amountOfClimbs.Max()}, Avg: {amountOfClimbs.Average()}");
logger.Information($"Single simulation result of amount of slides: Min: {amountOfSlides.Min()}, Max: {amountOfSlides.Max()}, Avg: {amountOfSlides.Average()}");
logger.Information($"Single simulation result of biggest climb in a single turn: Min: {biggestClimbInASingleTurn.Min()}, Max: {biggestClimbInASingleTurn.Min()}, Avg: {biggestClimbInASingleTurn.Average()}");
logger.Information($"Single simulation result of biggest slide in a single turn: Min: {biggestSlideInASingleTurn.Min()}, Max: {biggestSlideInASingleTurn.Max()}, Avg: {biggestSlideInASingleTurn.Average()}");
logger.Information($"Single simulation result of unlucky rolls: Min: {unluckyRolls.Min()}, Max: {unluckyRolls.Max()}, Avg: {unluckyRolls.Average()}");
logger.Information($"Single simulation result of lucky rolls: Min: {luckyRolls.Min()}, Max: {luckyRolls.Max()}, Avg: {luckyRolls.Average()}");
logger.Information($"Single simulation result of longest turn: {string.Join(',', longestTurn.OrderByDescending(x => x.Sum()).First())}");
logger.Information($"Single simulation result of winners in order: {string.Join(',', winnersInOrder)}");
