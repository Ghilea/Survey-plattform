using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Web.Models
{
    public class StatisticViewModel
    {
        public int Id { get; set; }
        public List<StatisticQuestionViewModel> Questions { get; set; } = new List<StatisticQuestionViewModel>();
        public int AmountRecivers { get; set; }
        public int AmountRetrivers { get; set; }
        public int TemplateId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class StatisticQuestionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public string Answer { get; set; } = string.Empty;
        public List<QuestionTrendViewModel> TrendData { get; set; } = new List<QuestionTrendViewModel>();
        public Dictionary<string, int> Answers { get; set; } = new Dictionary<string, int>();
    }

    public class QuestionTrendViewModel
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }

}
