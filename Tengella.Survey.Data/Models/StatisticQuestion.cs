namespace Tengella.Survey.Data.Models
{
    public class StatisticQuestion
    {
        public StatisticQuestion()
        {
            Statistics = new HashSet<Statistic>();
        }

        public int Id { get; set; }
        public int StatisticId { get; set; } // Foreign key to CreatedSurvey
        public string Name { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        public ICollection<Statistic> Statistics { get; set; } // Navigation property to the associated CreatedSurvey
    }
}