using SnakesAndLadders.Core.Interfaces;

namespace SnakesAndLadders.Core.Models
{
    public class Dice: IAdvancer
    {
        private readonly int _min;
        private readonly int _max;

        public Dice(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public int Next()
        {
            return Random.Shared.Next(_min, _max + 1);
        }
    }
}
