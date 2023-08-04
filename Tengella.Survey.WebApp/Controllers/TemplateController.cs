using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Tengella.Survey.Data;
using OfficeOpenXml;

namespace Tengella.Survey.WebApp.Repositories
{
    public class TemplateController : Controller
    {
        private readonly ITemplateRepository _TemplateRepository;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributionRepository _DistributionRepository;
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IStatisticRepository _StatisticRepository;
        private readonly SurveyDbContext _surveyDbcontext;

        public TemplateController(
            ITemplateRepository TemplateRepository, 
            IUrlHelperFactory urlHelperFactory, 
            IHttpContextAccessor httpContextAccessor, 
            IDistributionRepository DistributionRepository,
            ISurveyRepository surveyRepository,
            IStatisticRepository statisticRepository,
            SurveyDbContext surveyDbcontext)
        {
            _TemplateRepository = TemplateRepository;
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
            _DistributionRepository = DistributionRepository;
            _SurveyRepository = surveyRepository;
            _StatisticRepository = statisticRepository;
            _surveyDbcontext = surveyDbcontext;
        }

        public IActionResult Index()
        {
            var templates = _TemplateRepository.GetAllTemplates();
            return View(templates);
        }

        public IActionResult Create()
        {
            var list = _surveyDbcontext.SurveyList.ToList();

            var viewModel = new TemplateViewModel();

            viewModel.ListOfSurvey = list;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Template template)
        {
            if (ModelState.IsValid)
            {
                _TemplateRepository.AddTemplate(template);

                return RedirectToAction("Index");
            }

            return View(template);
        }

        public IActionResult Send(int id)
        {
            var templateById = _TemplateRepository.GetTemplateById(id);
            var senders = _surveyDbcontext.TemplateSenderLists.ToList();

            if (templateById != null)
            {
                var viewModel = new SendViewModel()
                {
                    TemplateId = templateById.Id,
                    Survey = _SurveyRepository.GetSurveyById(templateById.SurveyId),
                    Distributions = _DistributionRepository.GetAllEmailAddresses(),
                    Senders = senders
                };

                return View(viewModel);
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult SendEmailByTemplateId(int id)
        {
            var template = _TemplateRepository.GetTemplateById(id);
            if (template == null)
            {
                return NotFound();
            }

            ViewBag.TemplateId = id;
            var emailAddresses = _DistributionRepository.GetAllEmailAddresses();
            return View("Index", emailAddresses);
        }

        [HttpPost]
        public IActionResult SendEmails(int[] selectedEmails, int templateId, IFormFile importedFile)
        {
            if (importedFile != null)
            {
                var importedEmails = _DistributionRepository.ImportedFromFile(importedFile);
                
                foreach (var emailAddress in importedEmails)
                {
                    var getEmail = _DistributionRepository.GetEmailAddressByAddress(emailAddress.Email);
                    if (getEmail != null)
                    {
                        SendEmailById(templateId, getEmail.Id);
                    }
                }
                return RedirectToAction("Index");
            }

            if (selectedEmails != null && selectedEmails.Length > 0)
            {
                var existingSenders = _surveyDbcontext.TemplateSenderLists.Where(x => x.TemplateId == templateId).ToList();
                var idsToRemove = existingSenders.Select(x => x.DistributionId).Except(selectedEmails);
                
                var sendersToRemove = existingSenders.Where(x => idsToRemove.Contains(x.DistributionId));
                _surveyDbcontext.TemplateSenderLists.RemoveRange(sendersToRemove);

                foreach (var emailId in selectedEmails)
                {
                    var emailAddress = _DistributionRepository.GetEmailAddressById(emailId);

                    if (emailAddress != null)
                    {
                        SendEmailById(templateId, emailAddress.Id);

                        var addSender = new TemplateSenderList
                        {
                            DistributionId = emailAddress.Id,
                            TemplateId = templateId
                        };

                        _TemplateRepository.AddOrUpdateSendersList(addSender);
                    }
                }

            }
            else
            {
                var allEmailAddresses = _DistributionRepository.GetAllEmailAddresses();
                foreach (var emailAddress in allEmailAddresses)
                {
                    SendEmailById(templateId, emailAddress.Id);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SendEmailById(int templateId, int emailAddressId)
        {
            var template = _TemplateRepository.GetTemplateById(templateId);
            var emailAddress = _DistributionRepository.GetEmailAddressById(emailAddressId);

            if (template == null || emailAddress == null)
            {
                return NotFound();
            }

            if (emailAddress.IsToRecive != false)
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Coleman Windler", "coleman.windler95@ethereal.email"));
                emailMessage.To.Add(new MailboxAddress(emailAddress.Name, emailAddress.Email));
                emailMessage.Subject = template.Subject;

                var surveyViewLink = Url.Action("ViewSurveyFromLink", "Survey", new { id = template.SurveyId, emailAddress = emailAddress.Email }, Request.Scheme);
                var unsubscribeViewLink = Url.Action("Unsubscribe", "Template", new { emailAddress = emailAddress.Email }, Request.Scheme);

                var body = template.Body
                    .Replace("{SurveyLink}", surveyViewLink)
                    .Replace("{UnsubscribeLink}", unsubscribeViewLink);

                emailMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = body
                };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("coleman.windler95@ethereal.email", "sNutgT4eWrfEfMGCsM");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);

                var statistic = new Statistic
                {
                    DistributionId = emailAddress.Id,
                    SurveyListId = template.SurveyId,
                    TemplateId = template.Id,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                };

                _StatisticRepository.AddStatistic(statistic);
            }

            return RedirectToAction("SendEmail", new { id = templateId });
        }

        public IActionResult Edit(int id)
        {
            var template = _TemplateRepository.GetTemplateById(id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        [HttpPost]
        public IActionResult Edit(TemplateViewModel template)
        {
            if (ModelState.IsValid)
            {
                _TemplateRepository.UpdateTemplate(template);
                return RedirectToAction("Index");
            }

            return View(template);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _TemplateRepository.DeleteTemplate(id);
            return RedirectToAction("Index");
        }

        public IActionResult Unsubscribe()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unsubscribe(string emailAddress)
        {
            var distribution = _DistributionRepository.GetEmailAddressByAddress(emailAddress);
            if (distribution != null)
            {
                if(distribution.IsToRecive == true)
                {
                    Content("Du är redan avregistrerad.");
                }
                distribution.IsToRecive = false;
                _DistributionRepository.UpdateEmailAddress(distribution);
                return RedirectToAction("Unsubscribe");
            }

            return Content("Hittade inte din e-postadress.");
        }
    }
}
