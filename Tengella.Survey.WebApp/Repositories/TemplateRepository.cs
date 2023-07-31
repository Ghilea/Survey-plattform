using Tengella.Survey.Data;
using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
 
        private readonly SurveyDbContext _surveyDbcontext;

        public TemplateRepository(SurveyDbContext surveyDbcontext)
        {
            _surveyDbcontext = surveyDbcontext;
        }

        public IEnumerable<Template> GetAllTemplates()
        {
            return _surveyDbcontext.Templates.ToList();
        }

        public TemplateViewModel GetTemplateById(int id)
        {
            
            var template = _surveyDbcontext.Templates.FirstOrDefault(e => e.Id == id);
            if (template == null)
            {
                throw new ArgumentException("Mall hittades inte med detta id.", nameof(id));
            }
            var list = _surveyDbcontext.SurveyList.ToList();

            var surveyName = "Okänd";
            var selectedType = list.FirstOrDefault(st => st.Id == template.SurveyId);
            if (selectedType != null)
            {
                surveyName = selectedType.Name;
            }

            var viewModel = new TemplateViewModel
            {
                Name = template.Name,
                Body = template.Body,
                Id = id,
                Subject = template.Subject,
                SurveyId = template.SurveyId,
                ListOfSurvey = list,
                SurveyName = surveyName
            };
            return viewModel ?? new TemplateViewModel();
        }

        public void AddTemplate(Template template)
        {
            _surveyDbcontext.Templates.Add(template);
            _surveyDbcontext.SaveChanges();
        }

        public void UpdateTemplate(TemplateViewModel template)
        {
            var existingTemplate = _surveyDbcontext.Templates.FirstOrDefault(e => e.Id == template.Id);
            if (existingTemplate != null)
            {
                existingTemplate.Name = template.Name;
                existingTemplate.Subject = template.Subject;
                existingTemplate.Body = template.Body;
                existingTemplate.SurveyId = template.SurveyId;

                _surveyDbcontext.SaveChanges();
            }
        }

        public void DeleteTemplate(int id)
        {
            var template = _surveyDbcontext.Templates.Find(id);
            if (template != null)
            {
                _surveyDbcontext.Templates.Remove(template);
                _surveyDbcontext.SaveChanges();
            }
        }

    }
}
