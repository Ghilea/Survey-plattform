namespace Tengella.Survey.Data.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SurveyId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public ICollection<TemplateSenderList> Senders { get; set; } = new List<TemplateSenderList>();

        // Navigation Properties
        public SurveyList? SurveyLists { get; set; }
    }
}