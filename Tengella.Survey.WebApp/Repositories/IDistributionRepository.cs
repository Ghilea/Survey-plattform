using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public interface IDistributionRepository
    {
        IEnumerable<Distribution> GetAllEmailAddresses();
        DistributionViewModel GetEmailAddressByAddress(string address);
        DistributionViewModel GetEmailAddressById(int id);
        List<DistributionViewModel> MapToViewModels(List<DistributionType> type, IEnumerable<Distribution> list);
        void AddEmailAddress(Distribution emailAddress);
        void UpdateEmailAddress(DistributionViewModel emailAddress);
        void DeleteEmailAddress(int id);
        List<ImportViewModel> ImportedFromFile(IFormFile importedFile);
    }
}
