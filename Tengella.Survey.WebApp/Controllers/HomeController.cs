using Microsoft.AspNetCore.Mvc;
using Tengella.Survey.Data;
using Tengella.Survey.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using Tengella.Survey.WebApp.Repositories;
using Tengella.Survey.Data.Models;

namespace Tengella.Survey.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IDistributionRepository _DistributionRepository;
        private readonly IStatisticRepository _StatisticRepository;
        private readonly ISurveyRepository _SurveyRepository;
        private readonly SurveyDbContext _surveyDbcontext;

        public HomeController(
            ITemplateRepository TemplateRepository,
            IStatisticRepository StatisticRepository,
            IDistributionRepository DistributionRepository,
            ISurveyRepository surveyRepository,
            SurveyDbContext surveyDbcontext)
        {
            _TemplateRepository = TemplateRepository;
            _DistributionRepository = DistributionRepository;
            _SurveyRepository = surveyRepository;
            _StatisticRepository = StatisticRepository;
            _surveyDbcontext = surveyDbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
