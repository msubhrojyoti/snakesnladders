namespace SnakesAndLadders.Core.Exceptions
{
    public class InvalidCharacterPositionException : Exception
    {
        public InvalidCharacterPositionException(string message)
            : base(message)
        {
        }
    }
}
