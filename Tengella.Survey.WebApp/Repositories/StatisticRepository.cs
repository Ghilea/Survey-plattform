using Tengella.Survey.Data;
using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
 
        private readonly SurveyDbContext _surveyDbcontext;

        public StatisticRepository(SurveyDbContext surveyDbcontext)
        {
            _surveyDbcontext = surveyDbcontext;
        }

        public int GetStatisticIdByEmailAddress(string emailaddress)
        {
            var distributionId = _surveyDbcontext.Distribution.FirstOrDefault(e => e.Email == emailaddress);
            var statisticId = _surveyDbcontext.Statistics.FirstOrDefault(s => s.DistributionId == distributionId.Id);

            return statisticId.Id;
        }

        public int AmountReciversBySurveyId(int id)
        {
            var statistics = _surveyDbcontext.Statistics.Count(s => s.SurveyListId == id);
            return statistics;
        }

        public int AmountRetriversBySurveyId(int id)
        {
            var statistic = _surveyDbcontext.Statistics.Where(s => s.SurveyListId == id);

            var counted = 0;

            foreach (var countQuestion in statistic)
            {
                var countQuestions = _surveyDbcontext.StatisticsQuestions.Count(s => s.StatisticId == countQuestion.Id);
                
                counted += countQuestions;
            }
            
            return counted;
        }

        public Statistic GetStatisticById(int id)
        {
            
            var statistic = _surveyDbcontext.Statistics.FirstOrDefault(e => e.Id == id);
            if (statistic == null)
            {
                throw new ArgumentException("Hittade inget med det idt.", nameof(id));
            }
            
            return statistic;
        }

        public void AddStatistic(Statistic statistic)
        {
            _surveyDbcontext.Statistics.Add(statistic);
            _surveyDbcontext.SaveChanges();
        }

        public void UpdateStatistic(Statistic statistic)
        {
            var existingstatistic = _surveyDbcontext.Statistics.FirstOrDefault(e => e.Id == statistic.Id);
            
            if (existingstatistic != null)
            {
              
                _surveyDbcontext.SaveChanges();
            }
        }

        public void DeleteStatistic(int id)
        {
            var statistic = _surveyDbcontext.Statistics.Find(id);
            if (statistic != null)
            {
                _surveyDbcontext.Statistics.Remove(statistic);
                _surveyDbcontext.SaveChanges();
            }
        }

    }
}
