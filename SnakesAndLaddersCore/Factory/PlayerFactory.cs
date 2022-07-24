using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    public static class PlayerFactory
    {
        public static IPlayer CreatePlayer(string name)
        {
            return new Player(name);
        }
    }
}
