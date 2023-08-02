using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public interface ITemplateRepository
    {
        IEnumerable<Template> GetAllTemplates();
        TemplateViewModel GetTemplateById(int id);
        void AddTemplate(Template template);
        void AddOrUpdateSendersList(TemplateSenderList templateSenderList);
        void UpdateTemplate(TemplateViewModel template);
        void DeleteTemplate(int id);
    }
}
