using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Tengella.Survey.Data;
using Tengella.Survey.Data.Models;
using Tengella.Survey.Web.Models;

namespace Tengella.Survey.WebApp.Repositories
{
    public class DistributionRepository : IDistributionRepository
    {
        private readonly SurveyDbContext _surveyDbcontext;

        public DistributionRepository(SurveyDbContext surveyDbcontext)
        {
            _surveyDbcontext = surveyDbcontext;
        }

        public IEnumerable<Distribution> GetAllEmailAddresses()
        {
            return _surveyDbcontext.Distribution.ToList();
        }

        public DistributionViewModel GetEmailAddressByAddress(string address)
        {
            var emailAddress = _surveyDbcontext.Distribution.FirstOrDefault(e => e.Email == address);
            if (emailAddress == null)
            {
                throw new ArgumentException("Det fanns ingen e-post med den adressen", address);
            }
            var list = _surveyDbcontext.DistributionTypes.ToList();

            var typeName = "Okänd";
            var selectedType = list.FirstOrDefault(st => st.Id == emailAddress.DistributionTypeId);
            if (selectedType != null)
            {
                typeName = selectedType.Name;
            }

            var viewModel = new DistributionViewModel
            {
                Name = emailAddress.Name,
                DistributionTypeId = emailAddress.DistributionTypeId,
                Email = emailAddress.Email,
                ListOfType = list,
                Id = emailAddress.Id,
                OrganizationNumber = emailAddress.OrganizationNumber,
                TypeName = typeName,
                IsToRecive = emailAddress.IsToRecive,
            };
            return viewModel;
        }

        public List<DistributionViewModel> MapToViewModels(List<DistributionType> type, IEnumerable<Distribution> list)
        {

            var distributionViewModels = new List<DistributionViewModel>();

            foreach (var distribution in list)
            {
                var typeName = "Okänd";
                var selectedType = type.FirstOrDefault(st => st.Id == distribution.DistributionTypeId);
                if (selectedType != null)
                {
                    typeName = selectedType.Name;
                }

                var distributionViewModel = new DistributionViewModel
                {
                    Name = distribution.Name,
                    DistributionTypeId = distribution.DistributionTypeId,
                    Email = distribution.Email,
                    ListOfType = type,
                    TypeName = typeName,
                    Id = distribution.Id
                };

                distributionViewModels.Add(distributionViewModel);
            }

            return distributionViewModels;
        }

        public DistributionViewModel GetEmailAddressById(int id)
        {
            var emailAddress = _surveyDbcontext.Distribution.FirstOrDefault(e => e.Id == id);
            if (emailAddress == null)
            { 
                throw new ArgumentException("E-postadress hittades inte med det id.", nameof(id));
            }
            var list = _surveyDbcontext.DistributionTypes.ToList();

            var typeName = "Okänd";
            var selectedType = list.FirstOrDefault(st => st.Id == emailAddress.DistributionTypeId);
            if (selectedType != null)
            {
                typeName = selectedType.Name;
            }

            var viewModel = new DistributionViewModel
            {
                Name = emailAddress.Name,
                DistributionTypeId = emailAddress.DistributionTypeId,
                Email = emailAddress.Email,
                ListOfType = list,
                Id = emailAddress.Id,
                OrganizationNumber = emailAddress.OrganizationNumber,
                TypeName = typeName,
                IsToRecive = emailAddress.IsToRecive,
            };
            return viewModel;
        }

        public void AddEmailAddress(Distribution emailAddress)
        {
            _surveyDbcontext.Distribution.Add(emailAddress);
            _surveyDbcontext.SaveChanges();
        }

        public void UpdateEmailAddress(DistributionViewModel emailAddress)
        {
            var existingEmail = _surveyDbcontext.Distribution.FirstOrDefault(e => e.Id == emailAddress.Id);
            if (existingEmail != null)
            {
                existingEmail.Name = emailAddress.Name;
                existingEmail.Email = emailAddress.Email;
                existingEmail.DistributionTypeId = emailAddress.DistributionTypeId;
                existingEmail.OrganizationNumber = emailAddress.OrganizationNumber;
                existingEmail.IsToRecive = emailAddress.IsToRecive;

                _surveyDbcontext.SaveChanges();
            }
        }

        public void DeleteEmailAddress(int id)
        {
            var emailToRemove = _surveyDbcontext.Distribution.FirstOrDefault(e => e.Id == id);
            if (emailToRemove != null)
            {
                _surveyDbcontext.Distribution.Remove(emailToRemove);
                _surveyDbcontext.SaveChanges();
            }
        }

        public List<ImportViewModel> ImportedFromFile(IFormFile importedFile)
        {
            var importedData = new List<ImportViewModel>();

            if (importedFile.ContentType == "application/vnd.ms-excel" || importedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                using (var stream = new MemoryStream())
                {
                    importedFile.CopyTo(stream);
                    stream.Position = 0;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Assuming the email addresses start from the 2nd row
                        {
                            string name = worksheet.Cells[row, 1]?.Text?.Trim();
                            string email = worksheet.Cells[row, 2]?.Text?.Trim();
                            string type = worksheet.Cells[row, 3]?.Text?.Trim();
                            string organizationNumber = worksheet.Cells[row, 4]?.Text?.Trim();

                            if (!string.IsNullOrEmpty(email))
                            {
                                var rowModel = new ImportViewModel
                                {
                                    Name = name,
                                    Email = email,
                                    Type = type,
                                    OrganizationNumber = organizationNumber
                                };

                                importedData.Add(rowModel);
                            }
                        }
                    }
                }
            }
            else if (importedFile.ContentType == "text/csv")
            {
                using (var reader = new StreamReader(importedFile.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            var values = line.Split(','); // Split the line using comma as the separator

                            // Assuming the columns are in order: Name, Email, Type, OrganizationNumber
                            if (values.Length >= 4)
                            {
                                var rowModel = new ImportViewModel
                                {
                                    Name = values[0].Trim(),
                                    Email = values[1].Trim(),
                                    Type = values[2].Trim(),
                                    OrganizationNumber = values[3].Trim()
                                };

                                importedData.Add(rowModel);
                            }
                        }
                    }
                }
            }

            foreach (var rowModel in importedData)
            {
                var distribution = new Distribution
                {
                    Name = rowModel.Name,
                    Email = rowModel.Email,
                    DistributionTypeId = rowModel.Type == "Privatperson" ? 1 : rowModel.Type == "Företag" ? 2 : 3,
                    OrganizationNumber = rowModel.OrganizationNumber
                };

               AddEmailAddress(distribution);
            }

            return importedData;

        }
    }
}
