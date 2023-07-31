namespace Tengella.Survey.Web.Models
{
    public class CombinedViewModel
    {
        public IEnumerable<DistributionViewModel> Distributions { get; set; } = Enumerable.Empty<DistributionViewModel>();
        public IEnumerable<TemplateViewModel> Templates { get; set; } = Enumerable.Empty<TemplateViewModel>();
    }
}
