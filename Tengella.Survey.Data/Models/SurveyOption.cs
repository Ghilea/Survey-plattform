namespace Tengella.Survey.Data.Models
{
    public class SurveyOption
    {
        public SurveyOption()
        {
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SurveyQuestionId { get; set; } // Foreign key to CreatedSurvey

        public ICollection<SurveyQuestion> SurveyQuestions { get; set; } // Navigation property to the associated CreatedSurvey
    }
}