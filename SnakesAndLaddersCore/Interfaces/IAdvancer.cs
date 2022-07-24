namespace SnakesAndLadders.Core.Interfaces
{
    /// <summary>
    /// Interface for generic advancer e.g. dice
    /// </summary>
    public interface IAdvancer
    {
        /// <summary>
        /// Next number to advance to
        /// </summary>
        /// <returns></returns>
        int Next();
    }
}
