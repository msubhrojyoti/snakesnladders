using SnakesAndLadders.Core.Interfaces;
using SnakesAndLadders.Core.Models;

namespace SnakesAndLadders.Core.Factory
{
    /// <summary>
    /// Factory to create advancers
    /// </summary>
    public static class AdvancerFactory
    {
        public static IAdvancer CreateAdvancer(int min, int max)
        {
            return new Dice(min, max);
        }
    }
}
