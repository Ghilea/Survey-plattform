using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Web.Models
{
    public class DistributionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DistributionTypeId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string? OrganizationNumber { get; set; }
        public bool IsToRecive { get; set; } = true;
        public ICollection<DistributionType> ListOfType { get; set; } = new List<DistributionType>();
    }
}
