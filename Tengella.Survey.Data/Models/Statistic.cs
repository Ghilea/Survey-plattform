namespace Tengella.Survey.Data.Models
{
    public class Statistic
    {
        public Statistic()
        {
            SurveyLists = new HashSet<SurveyList>();
            Distributions = new HashSet<Distribution>();
        }

        public int Id { get; set; }
        public int SurveyListId { get; set; }
        public int DistributionId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<StatisticQuestion> Questions { get; set; } = new List<StatisticQuestion>();

        public ICollection<Distribution> Distributions { get; set; } // Navigation property
        public ICollection<SurveyList> SurveyLists { get; set; } // Navigation property
    }
}