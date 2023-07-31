namespace Tengella.Survey.Data.Models
{
    public class Distribution
    {

        public int Id { get; set; }
        public int DistributionTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? OrganizationNumber { get; set; }
        public bool IsToRecive { get; set; } = true;

        // Navigation Property
        public DistributionType? DistributionTypes { get; set; }
    }
}
