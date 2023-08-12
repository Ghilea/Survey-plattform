using Microsoft.AspNetCore.Mvc;
using Tengella.Survey.Data;
using Tengella.Survey.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using Tengella.Survey.WebApp.Repositories;
using Tengella.Survey.Data.Models;

namespace Tengella.Survey.WebApp.Controllers
{
    public class StatisticController : Controller
    {
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IDistributionRepository _DistributionRepository;
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IStatisticRepository _StatisticRepository;
        private readonly SurveyDbContext _surveyDbcontext;

        public StatisticController(
            ITemplateRepository TemplateRepository,
            IDistributionRepository DistributionRepository,
            ISurveyRepository surveyRepository,
            IStatisticRepository statisticRepository,
            SurveyDbContext surveyDbcontext)
        {
            _TemplateRepository = TemplateRepository;
            _DistributionRepository = DistributionRepository;
            _SurveyRepository = surveyRepository;
            _surveyDbcontext = surveyDbcontext;
            _StatisticRepository = statisticRepository;
        }

        public IActionResult Index(int surveyId)
        {
            var amountRecivers = _StatisticRepository.AmountReciversBySurveyId(surveyId);
            var amountRetrivers = _StatisticRepository.AmountRetriversBySurveyId(surveyId);
            var statisticQuestions = _StatisticRepository.GetAnswersById(surveyId);

            var model = new StatisticViewModel
            {
                Id = surveyId,
                AmountRecivers = amountRecivers,
                AmountRetrivers = amountRetrivers,
                Questions = statisticQuestions,
            };

            return View(model);

        }

    }
}
