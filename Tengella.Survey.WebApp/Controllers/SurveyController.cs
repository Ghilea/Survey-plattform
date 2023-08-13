using Microsoft.AspNetCore.Mvc;
using Tengella.Survey.Data;
using Tengella.Survey.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using Tengella.Survey.WebApp.Repositories;
using Tengella.Survey.Data.Models;

namespace Tengella.Survey.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IDistributionRepository _DistributionRepository;
        private readonly IStatisticRepository _StatisticRepository;
        private readonly ISurveyRepository _SurveyRepository;
        private readonly SurveyDbContext _surveyDbcontext;

        public SurveyController(
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
            var survey = _SurveyRepository.GetAllSurveys();
            return View(survey);
        }

        public IActionResult Create()
        {
            var SurveyTypes = _surveyDbcontext.SurveyTypes.ToList();
            var surveyModel = new SurveyViewModel();

            if (TempData.TryGetValue("TempDataForCreateSurvey", out var createdSurvey) && createdSurvey is string surveyJson)
            {
                surveyModel = JsonConvert.DeserializeObject<SurveyViewModel>(surveyJson);
                surveyModel.ListOfType = SurveyTypes;
                return View("Create", surveyModel);
            }

            surveyModel.ListOfType = SurveyTypes;

            return View("Create", surveyModel);
        }

        public IActionResult Preview()
        {
            if (TempData.TryGetValue("TempDataForCreateSurvey", out var createdSurveyJson) && createdSurveyJson is string surveyJson)
            {
                var surveyModel = JsonConvert.DeserializeObject<SurveyViewModel>(surveyJson);
                return View(surveyModel);
            }

            return RedirectToAction("Create");
        }


        [HttpPost]
        public IActionResult Create(IFormCollection form, string preview, string fromPreview)
        {
            var survey = _SurveyRepository.SurveyFormConverter(form);

            if (!string.IsNullOrEmpty(preview))
            {
                TempData["TempDataForCreateSurvey"] = JsonConvert.SerializeObject(survey);
                return RedirectToAction("Preview");
            }

            if (!string.IsNullOrEmpty(fromPreview))
            {
                TempData["TempDataForCreateSurvey"] = JsonConvert.SerializeObject(survey);
                return RedirectToAction("Create");
            }

            _SurveyRepository.AddSurvey(survey);

            return RedirectToAction("Index", survey);
              
        }

        [HttpGet]
        public IActionResult ViewSurveyFromLink(int id, [FromQuery] string emailAddress)
        {
            ViewBag.EmailAddress = emailAddress;

            var survey = _SurveyRepository.GetSurveyById(id);

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        public IActionResult ViewSurvey(int id)
        {
            var survey = _SurveyRepository.GetSurveyById(id);

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        [HttpPost]
        public IActionResult SendSurvey(IFormCollection form, int id, string emailAddress)
        {
            int statisticId = _StatisticRepository.GetStatisticIdByEmailAddressAndSurveyId(id, emailAddress);

            var questionKeys = form.Keys.Where(key => key.StartsWith("Question-")).ToList();

            foreach (var key  in questionKeys)
            {
                string questionIndex = key.Replace("Question-", "");
                string nameKey = $"Name-{questionIndex}";
                string questionName = form[nameKey];

                var statisticQuestion = new StatisticQuestion
                {
                    Name = questionName,
                    Answer = form[key],
                    StatisticId = statisticId
                };

                _surveyDbcontext.StatisticsQuestions.Add(statisticQuestion);
            }

            var updateStatistic = _surveyDbcontext.Statistics.FirstOrDefault(x => x.Id == statisticId);

            if(updateStatistic != null)
            {
                updateStatistic.IsDone = true;
                updateStatistic.DateUpdated = DateTime.Now;
            }

            _surveyDbcontext.SaveChanges();

            

            return Content("Tack för att du gjorde undersökningen");
        }

        public IActionResult CreateSurvey(SurveyList surveyData)
        {
            return RedirectToAction("Create", surveyData);
        }

        public IActionResult Delete(int id)
        {
            _SurveyRepository.DeleteSurveyById(id);
            return RedirectToAction("Index");
        }

        public IActionResult Copy(int id)
        {
            _SurveyRepository.CopySurveyById(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var survey = _SurveyRepository.GetSurveyById(id);
            if (survey == null)
            {
                return NotFound();
            }

            return View("Edit", survey);
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            var survey = _SurveyRepository.SurveyFormConverter(form);
            _SurveyRepository.UpdateSurvey(survey);
            return RedirectToAction("Index");
        }

    }
}
