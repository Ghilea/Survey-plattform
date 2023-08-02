namespace Tengella.Survey.Data.Models
{
    public class SurveyQuestion
    {
        public SurveyQuestion()
        {
            SurveyLists = new HashSet<SurveyList>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<SurveyOption> Options { get; set; } = new List<SurveyOption>();
        public string AdditionalInfo { get; set; } = string.Empty;
        public int SurveyListId { get; set; } // Foreign key

        public ICollection<SurveyList> SurveyLists { get; set; } // Navigation property
    }
}