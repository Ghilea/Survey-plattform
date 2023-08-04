namespace Tengella.Survey.Web.Models
{
    public class ImportViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string OrganizationNumber { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}
