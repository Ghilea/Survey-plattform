namespace Tengella.Survey.Data.Models
{
    public class StatisticQuestion
    {
        public StatisticQuestion()
        {
            Statistics = new HashSet<Statistic>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Answer { get; set; } = string.Empty;
        public int StatisticId { get; set; } // Foreign key
        public ICollection<Statistic> Statistics { get; set; } // Navigation property
    }
}