namespace SnakesAndLadders.Core.Interfaces
{
    /// <summary>
    /// Generic interface for all statistics
    /// </summary>
    public interface IGameStats
    {
        /// <summary>
        /// Amount of slides and climbs
        /// </summary>
        /// <param name="character"></param>
        /// <param name="player"></param>
        void UpdateAmountOfSlidesAndClimbs(ICharacter character, IPlayer player);

        /// <summary>
        /// Biggest climb or slide in a single turn (can have multiple rolls)
        /// </summary>
        /// <param name="character"></param>
        /// <param name="player"></param>
        void UpdateBiggestClimbOrSlideInASingleTurn(IEnumerable<ICharacter> character, IPlayer player);

        /// <summary>
        /// Longest turn in the same (consecutive 6s)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="rolls"></param>
        void UpdateLongestTurn(IPlayer player, IList<int> rolls);

        /// <summary>
        /// Update unlucky rolls e.g. getting on a snake :-)
        /// </summary>
        /// <param name="character"></param>
        /// <param name="player"></param>
        /// <param name="roll"></param>
        void UpdateUnluckyRoll(ICharacter character, IPlayer player, int roll);

        /// <summary>
        /// Lucky roll e.g. getting on a ladder, rolling a winning roll or escaping a snake by 1 or 2 positions.
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="player"></param>
        /// <param name="roll"></param>
        /// <param name="boardSize"></param>
        void UpdateLuckyRoll(IDictionary<int, ICharacter> characters, IPlayer player, int roll, int boardSize);

        /// <summary>
        /// Total rolls in a game
        /// </summary>
        /// <param name="player"></param>
        void UpdateTotalRolls(IPlayer player);

        /// <summary>
        /// Get total rolls in game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetTotalRolls(IPlayer player);

        /// <summary>
        /// Get minimum rolls for winning
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="boardSize"></param>
        /// <returns></returns>
        int GetMinimumRollsForWin(IList<ICharacter> characters, int boardSize);

        /// <summary>
        /// Get amount of climbs
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetAmountOfClimbs(IPlayer player);

        /// <summary>
        /// Get amount of slides
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetAmountOfSlides(IPlayer player);

        /// <summary>
        /// Get longest turn in game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        IEnumerable<int> GetLongestTurn(IPlayer player);

        /// <summary>
        /// Get unlucky rolls e.g. getting on a snake :-)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetUnluckyRolls(IPlayer player);

        /// <summary>
        /// Get lucky roll e.g. getting on a ladder, rolling a winning roll or escaping a snake by 1 or 2 positions.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetLuckyRolls(IPlayer player);

        /// <summary>
        /// Biggest climb in a single turn (can have multiple rolls)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetBiggestClimbInATurn(IPlayer player);

        /// <summary>
        /// Biggest slide in a single turn (can have multiple rolls)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetBiggestSlideInATurn(IPlayer player);

        /// <summary>
        /// Update total traps (game extension)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int UpdateTotalTraps(IPlayer player);

        /// <summary>
        /// Get total traps (game extension)
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetTotalTraps(IPlayer player);
    }
}
