namespace Tengella.Survey.Web.Models
{
    public class ImportViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string OrganizationNumber { get; set; }
        public IFormFile File { get; set; }
    }
}
