﻿using Microsoft.EntityFrameworkCore;
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

        public int AmountReciversBySurveyId(int surveyId)
        {
            var statistics = _surveyDbcontext.Statistics.Count(s => s.SurveyListId == surveyId);
            return statistics;
        }

        public int AmountRetriversBySurveyId(int surveyId)
        {

            var statisticQuestion = _surveyDbcontext.StatisticsQuestions
            .Join(
                _surveyDbcontext.Statistics,
                question => question.StatisticId,
                statistic => statistic.Id,
                (question, statistic) => new { Question = question, Statistic = statistic })
            .Where(x => x.Statistic.SurveyListId == surveyId)
            .Select(x => x.Question.StatisticId)
            .Distinct()
            .Count();

            return statisticQuestion;
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

        public List<StatisticQuestionViewModel> GetAnswersById (int surveyId)
        {
            var questionStatistics = new List<StatisticQuestionViewModel>();
            var questionList = new Dictionary<string, Dictionary<string, int>>();

            var template = _surveyDbcontext.Templates.FirstOrDefault(s => s.SurveyId == surveyId);

            var statisticList = _surveyDbcontext.Statistics
                .Include(statistic => statistic.Questions)
                .Where(x => x.SurveyListId == surveyId && x.TemplateId == template.Id)
                .ToList();

            var statisticIds = statisticList.Select(statistic => statistic.Id).ToList();

            var statisticQuestions = _surveyDbcontext.StatisticsQuestions
                .Where(question => statisticIds.Contains(question.StatisticId))
                .ToList();

            foreach (var question in statisticQuestions)
            {
                if (questionList.ContainsKey(question.Name))
                {
                    if (questionList[question.Name].ContainsKey(question.Answer))
                    {
                        questionList[question.Name][question.Answer]++;
                    }
                    else
                    {
                        questionList[question.Name].Add(question.Answer, 1);
                    }
                }
                else
                {
                    questionList.Add(question.Name, new Dictionary<string, int> { { question.Answer, 1 } });
                }
            }

            foreach (var entry in questionList)
            {
                var addModel = new StatisticQuestionViewModel
                {
                    Name = entry.Key,
                    Answers = entry.Value,
                    AverageRating = GetAverageRating(entry.Value)
                };

                questionStatistics.Add(addModel);
            }

            return questionStatistics;

        }

        static double GetAverageRating(Dictionary<string, int> answers)
        {
            var totalSum = 0;
            var toBeDivided = 0;

            foreach (var answer in answers)
            {
                //Id = int.TryParse(form[$"Id.Question[{questionIndex}]"], out var questionId) ? questionId : 0,
                totalSum += (int.TryParse(answer.Key, out var answerValue) ? answerValue * answer.Value : 0);
                toBeDivided += answer.Value;  
            }

            return (totalSum / toBeDivided);
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
