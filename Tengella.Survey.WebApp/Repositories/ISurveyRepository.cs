using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public interface ISurveyRepository
    {
        IEnumerable<SurveyViewModel> GetAllSurveys();

        SurveyViewModel GetSurveyById(int id);

        void CopySurveyById(int id);
        void AddSurvey(SurveyViewModel survey);
        void UpdateSurvey(SurveyViewModel survey);
        void DeleteSurveyById(int id);
        int GetQuestionIndexFromKey(string key);
        int GetOptionIndexFromKey(string key);
        SurveyViewModel SurveyFormConverter(IFormCollection form);
    }
}
