using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    /// <summary>
    /// Factory to create Players
    /// </summary>
    public static class PlayerFactory
    {
        public static IPlayer CreatePlayer(string name)
        {
            return new Player(name);
        }
    }
}
