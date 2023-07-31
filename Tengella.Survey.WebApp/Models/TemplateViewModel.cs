using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Web.Models
{
    public class TemplateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int SurveyId { get; set; }
        public string SurveyName { get; set; } = string.Empty;
        public ICollection<SurveyList> ListOfSurvey { get; set; } = new List<SurveyList>();
    }

}
