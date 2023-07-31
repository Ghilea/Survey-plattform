namespace Tengella.Survey.Data.Models
{
    public class SurveyList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SurveyTypeId { get; set; }
        public ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();
        public ICollection<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Properties
        public SurveyType? SurveyTypes { get; set; }
    }
}