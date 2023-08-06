using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Tengella.Survey.Data.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Tengella.Survey.Data;
using Tengella.Survey.Web.Models;
using OfficeOpenXml;

namespace Tengella.Survey.WebApp.Repositories
{
    public class DistributionController : Controller
    {
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IDistributionRepository _DistributionRepository;
        private readonly SurveyDbContext _surveyDbcontext;

        public DistributionController(
            ITemplateRepository TemplateRepository, 
            IDistributionRepository DistributionRepository,
            SurveyDbContext surveyDbcontext)
        {
            _TemplateRepository = TemplateRepository;
            _DistributionRepository = DistributionRepository;
            _surveyDbcontext = surveyDbcontext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // license context, change NonCommerical to Commerical for the real deal
        }

        public IActionResult Index()
        {
            var listOfType = _surveyDbcontext.DistributionTypes.ToList();
            var distributions = _DistributionRepository.GetAllEmailAddresses();

            var distributionViewModels = new List<DistributionViewModel>();

            foreach (var distribution in distributions)
            {
                var typeName = "Okänd";
                var selectedType = listOfType.FirstOrDefault(st => st.Id == distribution.DistributionTypeId);
                if (selectedType != null)
                {
                    typeName = selectedType.Name;
                }

                var distributionViewModel = new DistributionViewModel
                {
                    Name = distribution.Name,
                    DistributionTypeId = distribution.DistributionTypeId,
                    Email = distribution.Email,
                    ListOfType = listOfType,
                    TypeName = typeName,
                    Id = distribution.Id
                };

                distributionViewModels.Add(distributionViewModel);
            }

            return View(distributionViewModels);
        }

        public IActionResult Create()
        {
            var list = _surveyDbcontext.DistributionTypes.ToList();
          
            var viewModel = new DistributionViewModel();

            viewModel.ListOfType = list;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Distribution distribution)
        {
            if (ModelState.IsValid)
            {
                _DistributionRepository.AddEmailAddress(distribution);

                return RedirectToAction("Index");
            }

            return View(distribution);
        }

        public IActionResult Edit(int id)
        {
            var distribution = _DistributionRepository.GetEmailAddressById(id);
            if (distribution == null)
            {
                return NotFound();
            }

            return View(distribution);
        }

        [HttpPost]
        public IActionResult Edit(DistributionViewModel distribution)
        {
            if (ModelState.IsValid)
            {
                _DistributionRepository.UpdateEmailAddress(distribution);
                return RedirectToAction("Index");
            }

            return View(distribution);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _DistributionRepository.DeleteEmailAddress(id);
            return RedirectToAction("Index");
        }

        public IActionResult ImportDistributionList()
        {
            return View();
        }

        public IActionResult ViewDistribution(int id)
        {
            var distribution = _DistributionRepository.GetEmailAddressById(id);
            if (distribution == null)
            {
                return NotFound();
            }

            return View(distribution);
        }

        [HttpPost]
        public IActionResult ImportFile(IFormFile importedFile)
        {
            if (importedFile != null)
            {
                var importedEmails = _DistributionRepository.ImportedFromFile(importedFile);

                foreach (var emailAddress in importedEmails)
                {
                    var getEmail = _DistributionRepository.GetEmailAddressByAddress(emailAddress.Email);
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
