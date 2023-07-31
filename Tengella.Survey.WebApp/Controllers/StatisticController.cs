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

        public IActionResult Index(int id)
        {
            var amountRecivers = _StatisticRepository.AmountReciversBySurveyId(id);
            var amountRetrivers = _StatisticRepository.AmountRetriversBySurveyId(id);
            var model = new StatisticViewModel
            {
                AmountRecivers = amountRecivers,
                AmountRetrivers = amountRetrivers,
            };

            return View(model);

        }

    }
}
