namespace Tengella.Survey.Data.Models
{
    public class TemplateSenderList
    {
        public TemplateSenderList()
        {
            Templates = new HashSet<Template>();
            Distributions = new HashSet<Distribution>();
        }
        public int Id { get; set; }

        // Foreign key
        public int TemplateId { get; set; }
        public int DistributionId { get; set; }

        // Navigation property
        public ICollection<Distribution> Distributions { get; set; } 
        public ICollection<Template> Templates { get; set; }
    }
}