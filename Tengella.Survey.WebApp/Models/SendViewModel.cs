using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Web.Models
{
    public class SendViewModel
    {
        public int SurveyId { get; set; }
        public int TemplateId { get; set; }
        public int DistributionId { get; set; }
        public IFormFile File { get; set; }
        public IEnumerable<Distribution> Distributions { get; set; }
        public SurveyViewModel Survey { get; set; }
    }
}
