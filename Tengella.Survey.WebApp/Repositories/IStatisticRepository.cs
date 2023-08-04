using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public interface IStatisticRepository
    {
        //IEnumerable<Statistic>
        int AmountReciversBySurveyId(int id);
        int AmountRetriversBySurveyId(int id);
        List<StatisticQuestionViewModel> GetAnswersById(int id);
        Statistic GetStatisticById(int id);
        void AddStatistic(Statistic template);
        void UpdateStatistic(Statistic template);
        void DeleteStatistic(int id);
        int GetStatisticIdByEmailAddress(string emailaddress);
    }
}
