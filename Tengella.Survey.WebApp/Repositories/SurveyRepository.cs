using Microsoft.EntityFrameworkCore;
using Tengella.Survey.Data;
using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveyDbContext _surveyDbcontext;
        private readonly IStatisticRepository _statisticRepository;

        public SurveyRepository(
            SurveyDbContext surveyDbcontext,
            IStatisticRepository statisticRepository)
        {
            _surveyDbcontext = surveyDbcontext;
            _statisticRepository = statisticRepository;
        }

        public IEnumerable<SurveyViewModel> GetAllSurveys()
        {
            var listOfType = _surveyDbcontext.SurveyTypes.ToList();
            var surveys = _surveyDbcontext.SurveyList.ToList();

            var surveyViewModels = new List<SurveyViewModel>();

            foreach (var survey in surveys)
            {
                var typeName = "Okänd";
                var selectedType = listOfType.FirstOrDefault(st => st.Id == survey.SurveyTypeId);
                if (selectedType != null)
                {
                    typeName = selectedType.Name;
                }

                var amountRecivers = _statisticRepository.AmountReciversBySurveyId(survey.Id);
                var amountRetrivers = _statisticRepository.AmountRetriversBySurveyId(survey.Id);

                var surveyViewModel = new SurveyViewModel
                {
                    Name = survey.Name,
                    ListOfType = listOfType,
                    SurveyTypeId = survey.SurveyTypeId,
                    EndDate = survey.EndDate,
                    StartDate = survey.StartDate,
                    TypeName = typeName,
                    Id = survey.Id,
                    AmountRecivers = amountRecivers,
                    AmountRetrivers = amountRetrivers,
                };

                surveyViewModels.Add(surveyViewModel);
            }

            return surveyViewModels;
        }

        public SurveyViewModel GetSurveyById(int id)
        {
            var survey = _surveyDbcontext.SurveyList.FirstOrDefault(e => e.Id == id);

            if (survey == null)
            {
                throw new ArgumentException("Enkäten hittades inte med detta id.", nameof(id));
            }

            var surveyQuestions = _surveyDbcontext.SurveyQuestions
                .Where(sq => sq.SurveyListId == id)
                .ToList();

            foreach (var surveyQuestion in surveyQuestions)
            {
                surveyQuestion.Options = _surveyDbcontext.SurveyOptions
                    .Where(so => so.SurveyQuestionId == surveyQuestion.Id)
                    .ToList();
            }

            var viewModel = new SurveyViewModel
            {
                Name = survey.Name,
                SurveyTypeId = survey.SurveyTypeId,
                EndDate = survey.EndDate,
                StartDate = survey.StartDate,
                Id = id,
                ListOfType = _surveyDbcontext.SurveyTypes.ToList(),
                Questions = new List<QuestionViewModel>()
            };

            foreach (var surveyQuestion in surveyQuestions)
            {
                var questionViewModel = new QuestionViewModel
                {
                    Id = surveyQuestion.Id,
                    Name = surveyQuestion.Name,
                    Options = new List<OptionViewModel>(),
                    AdditionalInfo = surveyQuestion.AdditionalInfo,
                };

                foreach (var surveyOption in surveyQuestion.Options)
                {
                    var optionViewModel = new OptionViewModel
                    {
                        Id = surveyOption.Id,
                        Name = surveyOption.Name
                    };

                    questionViewModel.Options.Add(optionViewModel);
                }

                viewModel.Questions.Add(questionViewModel);
            }

            return viewModel;
        }

        public void CopySurveyById(int id)
        {
            var originalSurvey = _surveyDbcontext.SurveyList.FirstOrDefault(s => s.Id == id);
            var originalQuestions = _surveyDbcontext.SurveyQuestions.Where(s => s.SurveyListId == id).ToList();

            if (originalSurvey != null)
            {
                var copySurvey = new SurveyList
                {
                    Name = $"Kopia av {originalSurvey.Name}",
                    SurveyTypeId = originalSurvey.SurveyTypeId,
                    SurveyTypes = originalSurvey.SurveyTypes,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    Questions = new List<SurveyQuestion>()
                };

                _surveyDbcontext.SurveyList.Add(copySurvey);
                _surveyDbcontext.SaveChanges();

                foreach (var question in originalQuestions)
                {
                    var originalOptions = _surveyDbcontext.SurveyOptions.Where(s => s.SurveyQuestionId == question.Id).ToList();

                    var copyQuestion = new SurveyQuestion
                    {
                        Name = question.Name,
                        AdditionalInfo = question.AdditionalInfo != null ? question.AdditionalInfo : "",
                        SurveyListId = copySurvey.Id,
                        Options = new List<SurveyOption>(),
                    };

                    _surveyDbcontext.SurveyQuestions.Add(copyQuestion);
                    _surveyDbcontext.SaveChanges();

                    foreach (var option in originalOptions)
                    {
                        var copyOption = new SurveyOption
                        {
                            Name = option.Name,
                            SurveyQuestionId = copyQuestion.Id,
                        };

                        _surveyDbcontext.SurveyOptions.Add(copyOption);
                        _surveyDbcontext.SaveChanges();
                    }
                }

            }
        }

        public void AddSurvey(SurveyViewModel survey)
        {
            var addSurvey = new SurveyList()
            {
                Name = survey.Name,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                SurveyTypeId = survey.SurveyTypeId,
                Questions = new List<SurveyQuestion>()
            };

            _surveyDbcontext.SurveyList.Add(addSurvey);
            _surveyDbcontext.SaveChanges();

            foreach (var questionViewModel in survey.Questions)
            {
                var surveyQuestion = new SurveyQuestion
                {
                    Name = questionViewModel.Name,
                    AdditionalInfo = questionViewModel.AdditionalInfo != null ? questionViewModel.AdditionalInfo : "",
                    Options = new List<SurveyOption>(),
                    SurveyListId = addSurvey.Id
                };

                _surveyDbcontext.SurveyQuestions.Add(surveyQuestion);
                _surveyDbcontext.SaveChanges();

                foreach (var optionViewModel in questionViewModel.Options)
                {
                    var surveyOption = new SurveyOption
                    {
                        Name = optionViewModel.Name,
                        SurveyQuestionId = surveyQuestion.Id,
                    };

                    _surveyDbcontext.SurveyOptions.Add(surveyOption);
                    _surveyDbcontext.SaveChanges();
                }
            }
        }

        public void UpdateSurvey(SurveyViewModel survey)
        {
            var originalSurvey = _surveyDbcontext.SurveyList
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(s => s.Id == survey.Id);

            if (originalSurvey != null)
            {
                originalSurvey.Name = survey.Name;
                originalSurvey.SurveyTypeId = survey.SurveyTypeId;
                originalSurvey.StartDate = survey.StartDate;
                originalSurvey.EndDate = survey.EndDate;

                foreach (var editedQuestion in survey.Questions)
                {
                    // Check if the question already exists in the original survey
                    var existingQuestion = _surveyDbcontext.SurveyQuestions.FirstOrDefault(q => q.Id == editedQuestion.Id);

                    if (existingQuestion == null)
                    {
                        // If the question does not exist, create a new one and add it to the survey
                        existingQuestion = new SurveyQuestion
                        {
                            Name = editedQuestion.Name,
                            AdditionalInfo = editedQuestion.AdditionalInfo,
                            SurveyListId = originalSurvey.Id,
                            Options = new List<SurveyOption>()
                        };
                        originalSurvey.Questions.Add(existingQuestion);
                    }
                    else
                    {
                        // If the question already exists, update its properties
                        existingQuestion.Name = editedQuestion.Name;
                        existingQuestion.AdditionalInfo = editedQuestion.AdditionalInfo;
                        existingQuestion.Options.Clear();
                    }

                    foreach (var editedOption in editedQuestion.Options)
                    {
                        // Check if the option already exists in the question
                        var existingOption = _surveyDbcontext.SurveyOptions.FirstOrDefault(o => o.Id == editedOption.Id);

                        if (existingOption == null)
                        {
                            // If the option does not exist, create a new one and add it to the question
                            existingOption = new SurveyOption
                            {
                                Name = editedOption.Name,
                                SurveyQuestionId = existingQuestion.Id
                            };
                            existingQuestion.Options.Add(existingOption);
                        }
                        else
                        {
                            // If the option already exists, update its properties
                            existingOption.Name = editedOption.Name;
                        }
                    }
                }

                // Remove any questions that are not present in the edited survey
                var removedQuestions = originalSurvey.Questions
                    .Where(q => !survey.Questions.Any(editedQuestion => editedQuestion.Id == q.Id))
                    .ToList();

                foreach (var removedQuestion in removedQuestions)
                {
                    originalSurvey.Questions.Remove(removedQuestion);
                }

                _surveyDbcontext.SaveChanges();
            }
        }


        public void DeleteSurveyById(int id)
        {
            var survey = _surveyDbcontext.SurveyList.Include(s => s.Questions).ThenInclude(o => o.Options).FirstOrDefault(s => s.Id == id);

            if (survey != null)
            {
                foreach (var question in survey.Questions)
                {
                    var surveyOptions = _surveyDbcontext.SurveyOptions.Where(option => option.SurveyQuestionId == question.Id);
                    _surveyDbcontext.SurveyOptions.RemoveRange(surveyOptions);
                    _surveyDbcontext.SaveChanges();
                }

                _surveyDbcontext.SurveyQuestions.RemoveRange(survey.Questions);
                _surveyDbcontext.SaveChanges();

                _surveyDbcontext.SurveyList.Remove(survey);
                _surveyDbcontext.SaveChanges();
            }
        }

        public int GetQuestionIndexFromKey(string key)
        {
            var indexString = key.Split("-")[1];
            return int.Parse(indexString);
        }

        public int GetOptionIndexFromKey(string key)
        {
            var startIndex = key.IndexOf(".Options[") + ".Options[".Length;
            var endIndex = key.IndexOf("]", startIndex);

            var indexString = key.Substring(startIndex, endIndex - startIndex);
            return int.Parse(indexString);
        }

        public SurveyViewModel SurveyFormConverter(IFormCollection form)
        {         
            var questionKeys = form.Keys.Where(key => key.StartsWith("question")).ToList();

            var questions = new List<QuestionViewModel>();

            foreach (var key in questionKeys)
            {
                var questionIndex = GetQuestionIndexFromKey(key);

                var getAdditionalInfo = form[$"additionalInfo-question-{questionIndex}"].FirstOrDefault()?.ToString() ?? "";


                var question = new QuestionViewModel
                {
                    Id = int.TryParse(form[$"Id.Question[{questionIndex}]"], out var questionId) ? questionId : 0,
                    Name = form[key],
                    AdditionalInfo = getAdditionalInfo,
                    Options = new List<OptionViewModel>()
                };

                // Process options for the current question
                var optionKeys = form.Keys.Where(optKey => optKey.Contains($"Questions[{questionIndex}].Options")).ToList();

                foreach (var optKey in optionKeys)
                {
                    var optionIndex = GetOptionIndexFromKey(optKey);

                    var optionValue = form[optKey];

                    var option = new OptionViewModel
                    {
                        Id = int.TryParse(form[$"Id.Question[{questionIndex}].Option[{optionIndex}]"], out var optionId) ? optionId : 0,
                        Name = optionValue
                    };

                    question.Options.Add(option);
                }

                questions.Add(question);
            }

            var newSurvey = new SurveyViewModel
            {
                Id = int.TryParse(form["Id"], out var surveysId) ? surveysId : 0,
                Name = form["Name"],
                SurveyTypeId = int.TryParse(form["SurveyTypeId"], out var surveyId) ? surveyId : 0,
                StartDate = DateTime.TryParse(form["StartDate"], out var startDate) ? startDate : DateTime.Now,
                EndDate = DateTime.TryParse(form["EndDate"], out var endDate) ? endDate : DateTime.Now,
                Questions = questions
            };

            return newSurvey;
        }
    }
}
