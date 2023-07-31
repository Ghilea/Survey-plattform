using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Web.Models
{
    public class StatisticViewModel
    {
        public int Id { get; set; }
        public List<StatisticQuestionViewModel> Questions { get; set; } = new List<StatisticQuestionViewModel>();
        public int AmountRecivers { get; set; }
        public int AmountRetrivers { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class StatisticQuestionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
