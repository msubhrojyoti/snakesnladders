namespace SnakesAndLadders.Core.Models
{
    public class Statistic
    {
        public int Rolls { get; set; }
        public int AmountOfClimbs { get; set; }
        public int AmountOfSlides { get; set; }
        public IList<int> LongestTurn { get; set; } = new List<int>();
        public int UnluckyRolls { get; set; }
        public int LuckyRolls { get; set; }
        public int BiggestClimbInATurn { get; set; }
        public int BiggestSlideInATurn { get; set; }
    }
}
