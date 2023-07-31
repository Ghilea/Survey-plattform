using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Web.Models
{
    public class SurveyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SurveyTypeId { get; set; }
        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public ICollection<SurveyType> ListOfType { get; set; } = new List<SurveyType>();
        public ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();
        public int AmountRecivers { get; set; }
        public int AmountRetrivers { get; set; }
    }

    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
        public List<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();
    }

    public class OptionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
