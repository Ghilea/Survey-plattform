using Microsoft.AspNetCore.Mvc;
using Tengella.Survey.Data;
using Tengella.Survey.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using Tengella.Survey.WebApp.Repositories;
using System.Net.Mail;
using Tengella.Survey.Data.Models;
using System.Linq;

namespace Tengella.Survey.WebApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSurveyController : Controller
    {
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IDistributionRepository _DistributionRepository;
        private readonly IStatisticRepository _StatisticRepository;
        private readonly ISurveyRepository _SurveyRepository;
        private readonly SurveyDbContext _surveyDbcontext;

        public ApiSurveyController(
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

        [HttpPost("SaveFormData")]
        public IActionResult SaveFormData([FromBody] Dictionary<string, string> formData)
        {
            var groupedData = new Dictionary<string, Dictionary<string, string>>();

            if (formData.TryGetValue("id", out var idValue) && formData.TryGetValue("emailAddress", out var emailAddressValue))
            {
                if (int.TryParse(idValue, out var id))
                {
                    int statisticId = _StatisticRepository.GetStatisticIdByEmailAddressAndSurveyId(id, emailAddressValue);

                    var statisticData = _StatisticRepository.GetStatisticById(statisticId);

                    if (statisticData.IsDone == false)
                    {

                        // Group the data
                        foreach (var kvp in formData)
                        {
                            if (TryParseKeyNumber(kvp.Key, out var keyNumber))
                            {
                                var baseKeyNumber = GetBaseKeyNumber(kvp.Key);

                                // If the base key doesn't exist in the grouped data, add it
                                if (!groupedData.ContainsKey(baseKeyNumber))
                                {
                                    groupedData[baseKeyNumber] = new Dictionary<string, string>();
                                }

                                // Add or update the value in the existing dictionary
                                groupedData[baseKeyNumber][kvp.Key] = kvp.Value;
                            }
                        }

                        // Process the grouped data
                        foreach (var keyValuePair in groupedData)
                        {
                            var nameKey = $"Name-{keyValuePair.Key}";
                            var questionKey = $"Question-{keyValuePair.Key}";


                            if (keyValuePair.Value.TryGetValue(nameKey, out var nameValue) &&
                                keyValuePair.Value.TryGetValue(questionKey, out var questionValue))
                            {
                                if (!_StatisticRepository.DoesQuestionAnswerNameExist(statisticData.Id, nameValue))
                                {
                                    //add
                                    var statisticQuestion = new StatisticQuestion
                                    {
                                        Name = nameValue,
                                        Answer = questionValue,
                                        StatisticId = statisticId
                                    };

                                    _surveyDbcontext.StatisticsQuestions.Add(statisticQuestion);
 
                                }
                                else
                                {
                                    // Update the existing StatisticQuestion
                                    var existingQuestion = _surveyDbcontext.StatisticsQuestions
                                        .FirstOrDefault(question => question.StatisticId == statisticId && question.Name == nameValue);

                                    if (existingQuestion != null)
                                    {
                                        existingQuestion.Answer = questionValue;

                                    }
                                }

                                statisticData.DateUpdated = DateTime.Now;

                                _surveyDbcontext.SaveChanges();

                            }
                        }

                        return Ok(new { message = "Form data saved successfully." });
                    }
                    else
                    {
                        return Content("Survey already finished");
                    }
                    
                }
            }

            return BadRequest(new { error = "Invalid form data." });
        }


        // Helper function to parse the number from the key
        private bool TryParseKeyNumber(string key, out int number)
        {
            var parts = key.Split('-');
            if (parts.Length > 1 && int.TryParse(parts[1], out number))
            {
                return true;
            }
            number = -1;
            return false;
        }
        private string GetBaseKeyNumber(string key)
        {
            var parts = key.Split('-');
            return parts[1];
        }

    }
}
