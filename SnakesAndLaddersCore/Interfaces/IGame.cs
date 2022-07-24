using SnakesAndLadders.Core.Factory;

namespace SnakesAndLadders.Core.Interfaces
{
    public interface IGame : IDisposable
    {
        event GameFactory.NotifyWinner OnWinner;
        void Play();
        int Roll();
        IGameStats GetGameStats();
    }
}
