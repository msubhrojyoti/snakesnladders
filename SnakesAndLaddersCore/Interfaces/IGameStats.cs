namespace SnakesAndLadders.Core.Interfaces
{
    public interface IGameStats
    {
        void UpdateAmountOfSlidesAndClimbs(ICharacter character, IPlayer player);
        void UpdateBiggestClimbOrSlideInASingleTurn(IEnumerable<ICharacter> character, IPlayer player);
        void UpdateLongestTurn(IPlayer player, IList<int> rolls);
        void UpdateUnluckyRoll(ICharacter character, IPlayer player, int roll);
        void UpdateLuckyRoll(IDictionary<int, ICharacter> characters, IPlayer player, int roll, int boardSize);
        void UpdateTotalRolls(IPlayer player);
        int GetTotalRolls(IPlayer player);
        int GetMinimumRollsForWin(IList<ICharacter> characters, int boardSize);
        int GetAmountOfClimbs(IPlayer player);
        int GetAmountOfSlides(IPlayer player);
        IEnumerable<int> GetLongestTurn(IPlayer player);
        int GetUnluckyRolls(IPlayer player);
        int GetLuckyRolls(IPlayer player);
        int GetBiggestClimbInATurn(IPlayer player);
        int GetBiggestSlideInATurn(IPlayer player);
        int UpdateTotalTraps(IPlayer player);
        int GetTotalTraps(IPlayer player);
    }
}
