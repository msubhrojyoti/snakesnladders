using SnakesAndLadders.Core.Factory;

namespace SnakesAndLadders.Core.Interfaces
{
    public interface IGame
    {
        event GameFactory.NotifyWinner OnWinner;
        void Play();
        int Roll();
        IGameStats GetGameStats();
    }
}
