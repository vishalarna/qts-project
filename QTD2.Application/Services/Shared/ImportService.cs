using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurveyEmployeeResponse;
using QTD2.Infrastructure.Model.Import_CSV_VM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDIFSurveyDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurveyService;
using IDIFSurvey_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurvey_EmployeeService;
using IDifSurveyTaskDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurvey_TaskService;
using IDIFSurveyEmployeeResponseDomainService = QTD2.Domain.Interfaces.Service.Core.IDIFSurvey_Employee_ResponseService;
using ILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using ProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using PersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using OrganizationDomainService = QTD2.Domain.Interfaces.Service.Core.IOrganizationService;
using CertificationDomainService = QTD2.Domain.Interfaces.Service.Core.ICertificationService;
using PositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using InstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using LocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using IEmployeeCertificationDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeCertificationService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IDeliveryMethodDomainService = QTD2.Domain.Interfaces.Service.Core.IDeliveryMethodService;
using System.Text.RegularExpressions;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation;
using QTD2.Infrastructure.ExtensionMethods;

namespace QTD2.Application.Services.Shared
{
    public class ImportService : IImportService
    {
        private readonly IDIFSurveyDomainService _difSurveyDomainService;
        private readonly IDIFSurvey_EmployeeDomainService _difSurvey_EmployeeDomainService;
        private readonly IDifSurveyTaskDomainService _difSurveyTaskDomainService;
        private readonly IDIFSurveyEmployeeResponseDomainService _difSurveyEmployeeResponseDomainService;
        private readonly ILADomainService _ilaDomainService;
        private readonly ProviderDomainService _providerDomainService;
        private readonly PersonDomainService _personDomainService;
        private readonly OrganizationDomainService _organizationDomainService;
        private readonly CertificationDomainService _certificationDomainService;
        private readonly PositionDomainService _positionDomainService;
        private readonly EmployeeDomainService _employeeDomainService;
        private readonly IHasher _hasher;
        private readonly InstructorDomainService _instructorDomainService;
        private readonly LocationDomainService _locationDomainService;
        private readonly IEmployeeCertificationDomainService _employeeCertificationDomainService;
        private readonly IClassScheduleDomainService _classScheduleDomainService;
        private readonly IDeliveryMethodDomainService _deliveryMethodDomainService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IClientUserSettings_GeneralSettingService _clientGeneralSettingService;
        public ImportService(
            IDIFSurveyDomainService dIFSurveyDomainService,
            IDIFSurvey_EmployeeDomainService difSurvey_EmployeeDomainService,
            IDifSurveyTaskDomainService difSurveyTaskDomainService,
            IDIFSurveyEmployeeResponseDomainService difSurveyEmployeeResponseDomainService,
            ILADomainService ilaDomainService,
            ProviderDomainService providerDomainService,
            PersonDomainService personDomainService,
            OrganizationDomainService organizationDomainService,
            CertificationDomainService certificationDomainService,
            PositionDomainService positionDomainService,
            EmployeeDomainService employeeDomainService,
            InstructorDomainService instructorDomainService,
            LocationDomainService locationDomainService,
            IEmployeeCertificationDomainService employeeCertificationDomainService,
            IClassScheduleDomainService classScheduleDomainService,
            IDeliveryMethodDomainService deliveryMethodDomainService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IHasher hasher,
            IClientUserSettings_GeneralSettingService clientGeneralSettingService
            )
        {
            _difSurveyDomainService = dIFSurveyDomainService;
            _difSurvey_EmployeeDomainService = difSurvey_EmployeeDomainService;
            _difSurveyTaskDomainService = difSurveyTaskDomainService;
            _difSurveyEmployeeResponseDomainService = difSurveyEmployeeResponseDomainService;
            _ilaDomainService = ilaDomainService;
            _providerDomainService = providerDomainService;
            _personDomainService = personDomainService;
            _organizationDomainService = organizationDomainService;
            _certificationDomainService = certificationDomainService;
            _positionDomainService = positionDomainService;
            _employeeDomainService = employeeDomainService;
            _hasher = hasher;
            _instructorDomainService = instructorDomainService;
            _locationDomainService = locationDomainService;
            _employeeCertificationDomainService = employeeCertificationDomainService;
            _classScheduleDomainService = classScheduleDomainService;
            _deliveryMethodDomainService = deliveryMethodDomainService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _clientGeneralSettingService = clientGeneralSettingService;
        }
        public async Task<ValidateCSV_DIFSurveyEmployeeResponse_Results_VM> ValidateDIFEmployeeResponseCSVFileAsync(ValidateCSV_DIFSurveyEmployeeResponse_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var difsurveyId= _hasher.Decode(options.DifSurveyId);
            string[] expectedColumnNames = new string[] { "EmployeeNumber", "TaskNumber", "Difficulty", "Importance", "Frequency", "NA", "Comments" };
            ValidateCSV_DIFSurveyEmployeeResponse_Results_VM result = new ValidateCSV_DIFSurveyEmployeeResponse_Results_VM();
            List<ValidationError_VM> fileValidationErrors =  CommonValidation(options.File, expectedColumnNames, labelReplacement);

            if(fileValidationErrors.Any())
            {
                result.FileValidationErrors = fileValidationErrors;
                return result;
            }
            
            var data = await ReadDIFEmployeeResponseCSVDataAsync(options.File, labelReplacement);
            result.Data = await DIFEmployeeResponseModelToDomainAsync(data, Convert.ToInt32(difsurveyId), false);
            return result;
        }

        public async Task<ValidateCSV_ILA_Results_VM> ValidateILAAsync(ValidateCSV_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            string[] expectedColumnNames = new string[] { "ILA Name", "ILA Num", "ILA Desc","Self Paced", "Total Hours", "Provider Name", "Delivery Method Name", "Effective Date", "NercIsIncludeSimulation", "NercEmergencyOperatingTraining", "NercIsPartialCred", "NercTotalCEH", "NercStandards", "NercSimulation", "Reg", "Reg2" };
            ValidateCSV_ILA_Results_VM result = new ValidateCSV_ILA_Results_VM();
            List<ValidationError_VM> fileValidationErrors =  CommonValidation(options.File, expectedColumnNames, labelReplacement);

            if (fileValidationErrors.Any())
            {
                result.FileValidationErrors = fileValidationErrors;
                return result;
            }

            var data = await ReadILACSVDataAsync(options.File, labelReplacement);
            result.Data = await ILAModelToDomainAsync(data, false, labelReplacement);
            return result;
        }

        public async Task<ValidateCSV_Employee_Results_VM> ValidateEmployeeAsync(ValidateCSV_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            string[] expectedColumnNames = new string[] { "Last Name", "First Name","Middle", "Email", "Emp Num", "Phone", "Cert Name", "Cert Num", "Issue Date", "Recert Date", "Cert Exp Date", "Position Num", "Position Start Date", "Pos Abbrev", "Organization Name" };
            ValidateCSV_Employee_Results_VM result = new ValidateCSV_Employee_Results_VM();
            List<ValidationError_VM> fileValidationErrors =  CommonValidation(options.File, expectedColumnNames, labelReplacement);

            if (fileValidationErrors.Any())
            {
                result.FileValidationErrors = fileValidationErrors;
                return result;
            }
            var data = await ReadEmployeeCSVDataAsync(options.File, labelReplacement);
            result.Data = await EmployeeModelToDomainAsync(data, false, labelReplacement);
            return result;
        }

        public async Task<ValidateCSV_Class_Results_VM> ValidateClassAsync(ValidateCSV_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            string[] expectedColumnNames = new string[] { "Class ILA Num", "Start Date", "Class End Date", "Instructor Name", "Location", "Emp Num", "Comp Grade" };
            ValidateCSV_Class_Results_VM result = new ValidateCSV_Class_Results_VM();
            List<ValidationError_VM> fileValidationErrors =  CommonValidation(options.File, expectedColumnNames, labelReplacement);

            if (fileValidationErrors.Any())
            {
                result.FileValidationErrors = fileValidationErrors;
                return result;
            }

            var data = await ReadClassCSVDataAsync(options.File, labelReplacement);
            result.Data = await ClassModelToDomainAsync(data, false, labelReplacement);
            return result;
        }

        private async Task<List<ImportDatum_DIFSurveyEmployeeResponse_VM>> ReadDIFEmployeeResponseCSVDataAsync(IFormFile file, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            var importDataList = new List<ImportDatum_DIFSurveyEmployeeResponse_VM>();
            using (Stream stream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        await csvReader.ReadAsync();
                        csvReader.ReadHeader();
                        while (await csvReader.ReadAsync())
                        {
                            var importDatum = new ImportDatum_DIFSurveyEmployeeResponse_VM
                            {
                                // Populate properties based on CSV column names
                                EmployeeNumber = csvReader.GetField<string>("EmployeeNumber".DynamicReplaceLabel(labelReplacement)),
                                TaskNumber = csvReader.GetField<string>("TaskNumber".DynamicReplaceLabel(labelReplacement)),
                                Difficulty = csvReader.GetField<string>("Difficulty"),
                                Importance = csvReader.GetField<string>("Importance"),
                                Frequency = csvReader.GetField<string>("Frequency"),
                                NA = csvReader.GetField<string>("NA"),
                                Comments = csvReader.GetField<string>("Comments"),
                                ValidationErrors = new List<ValidationError_VM>()
                            };

                            importDataList.Add(importDatum);
                        }
                    }
                }
            }
            return importDataList;
        }

        private async Task<List<ImportDatum_ILA_VM>> ReadILACSVDataAsync(IFormFile file, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            var importDataList = new List<ImportDatum_ILA_VM>();
            var generalSettings = await _clientGeneralSettingService.GetGeneralSettings();
            var defaultTimeZone = generalSettings.DefaultTimeZone ?? "Central Standard Time";

            using (Stream stream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        await csvReader.ReadAsync();
                        csvReader.ReadHeader();
                        while (await csvReader.ReadAsync())
                        {
                            var importDatum = new ImportDatum_ILA_VM
                            {
                                // Populate properties based on CSV column names
                                IlaName = csvReader.GetField<string>("ILA Name".DynamicReplaceLabel(labelReplacement)),
                                IlaNum = csvReader.GetField<string>("ILA Num".DynamicReplaceLabel(labelReplacement)),
                                IlaDesc = csvReader.GetField<string>("ILA Desc".DynamicReplaceLabel(labelReplacement)),
                                SelfPaced = csvReader.GetField<string>("Self Paced"),
                                TotalHours = csvReader.GetField<string>("Total Hours"),
                                ProviderName = csvReader.GetField<string>("Provider Name"),
                                DeliveryMethodName = csvReader.GetField<string>("Delivery Method Name"),
                                EffectiveDate = csvReader.GetField<string>("Effective Date"),
                                NercIsIncludeSimulation = csvReader.GetField<string>("NercIsIncludeSimulation"),
                                NercEmergencyOperatingTraining = csvReader.GetField<string>("NercEmergencyOperatingTraining"),
                                NercIsPartialCred = csvReader.GetField<string>("NercIsPartialCred"),
                                NercTotalCEH = csvReader.GetField<string>("NercTotalCEH"),
                                NercStandards = csvReader.GetField<string>("NercStandards"),
                                NercSimulation = csvReader.GetField<string>("NercSimulation"),
                                Reg = csvReader.GetField<string>("Reg"),
                                Reg2 = csvReader.GetField<string>("Reg2"),
                                ValidationErrors = new List<ValidationError_VM>()
                            };

                            importDataList.Add(importDatum);
                        }
                    }
                }
            }
            return importDataList;
        }

        private async Task<List<ImportDatum_Employee_VM>> ReadEmployeeCSVDataAsync(IFormFile file, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            var importDataList = new List<ImportDatum_Employee_VM>();
            var generalSettings = await _clientGeneralSettingService.GetGeneralSettings();
            var defaultTimeZone = generalSettings.DefaultTimeZone ?? "Central Standard Time";
            using (Stream stream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        await csvReader.ReadAsync();
                        csvReader.ReadHeader();
                        while (await csvReader.ReadAsync())
                        {
                            var importDatum = new ImportDatum_Employee_VM
                            {
                                // Populate properties based on CSV column names
                                LastName = csvReader.GetField<string>("Last Name"),
                                EmpNum = csvReader.GetField<string>("Emp Num"),
                                FirstName = csvReader.GetField<string>("First Name"),
                                Email = csvReader.GetField<string>("Email"),
                                Middle = csvReader.GetField<string>("Middle"),
                                Phone = csvReader.GetField<string>("Phone"),
                                CertName = csvReader.GetField<string>("Cert Name"),
                                CertNum = csvReader.GetField<string>("Cert Num"),
                                IssueDate = csvReader.GetField<string>("Issue Date"),
                                RecertDate = csvReader.GetField<string>("Recert Date"),
                                CertExpDate = csvReader.GetField<string>("Cert Exp Date"),
                                PositionNum = csvReader.GetField<string>("Position Num".DynamicReplaceLabel(labelReplacement)),
                                PositionStartDate = csvReader.GetField<string>("Position Start Date".DynamicReplaceLabel(labelReplacement)),
                                PosAbbrev = csvReader.GetField<string>("Pos Abbrev"),
                                OrganizationName = csvReader.GetField<string>("Organization Name"),
                                ValidationErrors = new List<ValidationError_VM>()
                            };

                            importDataList.Add(importDatum);
                        }
                    }
                }
            }
            return importDataList;
        }

        private async Task<List<ImportDatum_Class_VM>> ReadClassCSVDataAsync(IFormFile file, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            var importDataList = new List<ImportDatum_Class_VM>();
            var generalSettings = await _clientGeneralSettingService.GetGeneralSettings();
            var defaultTimeZone = generalSettings.DefaultTimeZone ?? "Central Standard Time";
            using (Stream stream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        await csvReader.ReadAsync();
                        csvReader.ReadHeader();
                        while (await csvReader.ReadAsync())
                        {
                            var importDatum = new ImportDatum_Class_VM
                            {
                                // Populate properties based on CSV column names
                                ClassILANum = csvReader.GetField<string>("Class ILA Num".DynamicReplaceLabel(labelReplacement)),
                                StartDate = csvReader.TryGetField<DateTime>("Start Date", out DateTime startDate) ? startDate.ConvertFromDefaultTimeZone(defaultTimeZone).ToString("MM-dd-yyyy hh:mm:ss tt ") : csvReader.GetField<string>("Start Date"),
                                ClassEndDate = csvReader.TryGetField<DateTime>("Class End Date", out DateTime classEndDate) ? classEndDate.ConvertFromDefaultTimeZone(defaultTimeZone).ToString("MM-dd-yyyy hh:mm:ss tt ")  : csvReader.GetField<string>("Class End Date"),
                                InstructorName = csvReader.GetField<string>("Instructor Name".DynamicReplaceLabel(labelReplacement)),
                                Location = csvReader.GetField<string>("Location".DynamicReplaceLabel(labelReplacement)),
                                EmpNum = csvReader.GetField<string>("Emp Num"),
                                CompGrade = csvReader.GetField<string>("Comp Grade"),
                                ValidationErrors = new List<ValidationError_VM>()
                            };
                            importDataList.Add(importDatum);
                        }
                    }
                }
            }
            return importDataList;
        }

        private async Task<List<ImportDatum_DIFSurveyEmployeeResponse_VM>> DIFEmployeeResponseModelToDomainAsync(List<ImportDatum_DIFSurveyEmployeeResponse_VM> csvData, int difSurveyId, bool persistData)
        {
            foreach(var row in csvData)
            {
                List<ValidationError_VM> recordRelatedErrors = new List<ValidationError_VM>();
                var difSurveyEmployees = await _difSurvey_EmployeeDomainService.GetDifSurveyEmployeesBySurveyIdAsync(difSurveyId);
                var difSurveyTasks = await _difSurveyTaskDomainService.GetDifSurveyTasksBySurveyIdAsync(difSurveyId);
                var currenDifTask = difSurveyTasks.FirstOrDefault(x => x.Task.FullNumber == row.TaskNumber);
                var currentDifEmployee = difSurveyEmployees.FirstOrDefault(x => x.Employee.EmployeeNumber == row.EmployeeNumber);
                bool nA;
                if (currentDifEmployee == null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("No employee found for the employee number in the DIF Survey"));
                }
                string taskPattern = @"[^\d]+[0-9]+\.[0-9]+\.[0-9]+";
                if (!(Regex.Match(row.TaskNumber, taskPattern).Length == row.TaskNumber.Length))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Task number not in the correct format"));
                }
                else if (currenDifTask == null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("No task found for the task number in the DIF Survey"));
                }
                if (row.Difficulty != "" && !float.TryParse(row.Difficulty,out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Difficulty not in the correct format"));
                }
                if (row.Importance != "" && !float.TryParse(row.Importance, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Importance not in the correct format"));
                }
                if (row.Frequency != "" && !float.TryParse(row.Frequency, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Frequency not in the correct format"));
                }
                if (row.Frequency != "" && !float.TryParse(row.Frequency, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Frequency not in the correct format"));
                }
                if (!bool.TryParse(row.NA, out nA))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("NA not in the correct format"));
                }

                if (recordRelatedErrors.Any())
                {
                    row.ValidationErrors=recordRelatedErrors;
                    continue;
                }
                float? difficulty = row.Difficulty == "" ? null : Convert.ToSingle(row.Difficulty);
                float? importance = row.Importance == "" ? null : Convert.ToSingle(row.Importance);
                float? frequency = row.Frequency == "" ? null : Convert.ToSingle(row.Frequency);
                if (!persistData)
                {
                    var newEmployee_Response = new DIFSurvey_Employee_Response(currentDifEmployee.Id, currenDifTask.Id, difficulty, importance, frequency, nA, row.Comments);
                    var validationResult = _difSurveyEmployeeResponseDomainService.Validate(newEmployee_Response);
                    if (!validationResult.IsValid)
                    {
                        row.ValidationErrors = validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList();
                    }
                }
                else if(persistData && !row.ValidationErrors.Any())
                {
                    var newEmployee_Response = new DIFSurvey_Employee_Response(currentDifEmployee.Id, currenDifTask.Id);
                    newEmployee_Response.UpdateResponse(difficulty, importance, frequency, nA, row.Comments);
                    var validationResult = await _difSurveyEmployeeResponseDomainService.AddAsync(newEmployee_Response);
                    if (!validationResult.IsValid)
                    {
                        row.ValidationErrors = validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList();
                    }
                }

            }
            return csvData;
        }


        private async Task<List<ImportDatum_ILA_VM>> ILAModelToDomainAsync(List<ImportDatum_ILA_VM> csvData, bool persistData, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            foreach (var row in csvData)
            {
                row.ValidationErrors = new List<ValidationError_VM>();
                List<ValidationError_VM> recordRelatedErrors = new List<ValidationError_VM>();
                var provider = (await _providerDomainService.FindAsync(x => x.Name == row.ProviderName)).FirstOrDefault();
                var deliveryMethod = (await _deliveryMethodDomainService.FindAsync(x => x.Name == row.DeliveryMethodName)).FirstOrDefault();
                if ((await _ilaDomainService.GetCount(x => x.Name == row.IlaName && x.Number == row.IlaNum)) > 0)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("ILA already exists"));
                }
                if (string.IsNullOrEmpty(row.IlaName))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("ILA Name is required "));
                }
                if (string.IsNullOrEmpty(row.IlaNum))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("ILA Number is required"));
                }
                if (string.IsNullOrEmpty(row.ProviderName))
                    recordRelatedErrors.Add(new ValidationError_VM("Provider Name is required"));
                else if (provider == null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Provider not found"));
                }
                if (string.IsNullOrEmpty(row.DeliveryMethodName)) {
                    recordRelatedErrors.Add(new ValidationError_VM("Delivery Method is required"));
                }
                else if (deliveryMethod == null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Delivery Method not found"));
                }
                if (row.TotalHours != "" && !double.TryParse(row.TotalHours, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("TotalHours is not in the correct format"));
                }
                if (row.SelfPaced != "" && !bool.TryParse(row.SelfPaced, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("SelfPaced is not in the correct format"));
                }
                if (row.EffectiveDate != "" && !DateTime.TryParse(row.EffectiveDate, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("EffectiveDate is not in the correct format"));
                }
                var nercCertsIncluded = row.NercTotalCEH != "" && row.NercTotalCEH != "0";
                var regIncluded = row.Reg != "" && row.Reg != "0";
                var reg2Included = row.Reg2 != "" && row.Reg2 != "0";
                var certifications = await _certificationDomainService.FindWithIncludeAsync(x => (nercCertsIncluded && x.CertifyingBody.Name == "NERC") || (reg2Included && x.CertDesc == "Reg2") || (regIncluded && x.CertDesc == "Reg"), new string[] { "CertificationSubRequirements", "CertifyingBody" });
                if (certifications.Count() > 0)
                {
                    if (row.NercIsIncludeSimulation != "" && !bool.TryParse(row.NercIsIncludeSimulation, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("NercIsIncludeSimulation is not in the correct format"));
                    }
                    if (row.NercEmergencyOperatingTraining != "" && !bool.TryParse(row.NercEmergencyOperatingTraining, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("NercEmergencyOperatingTraining is not in the correct format"));
                    }
                    if (row.NercIsPartialCred != "" && !bool.TryParse(row.NercIsPartialCred, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("NercIsPartialCred is not in the correct format"));
                    }
                    if (certifications.Where(x => nercCertsIncluded && x.CertifyingBody.Name == "NERC").Count() > 0 && row.NercTotalCEH != "" && !double.TryParse(row.NercTotalCEH, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("NercTotalCEH is not in the correct format"));
                    }
                    if (certifications.Where(x => regIncluded && x.CertDesc == "Reg").Count() > 0 && row.Reg != "" && !double.TryParse(row.Reg, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("Reg is not in the correct format"));
                    }
                    if (certifications.Where(x => reg2Included && x.CertDesc == "Reg2").Count() > 0 && row.Reg2 != "" && !double.TryParse(row.Reg2, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("Reg2 is not in the correct format"));
                    }
                    var standardSubReq = certifications.SelectMany(x=>x.CertificationSubRequirements.Where(x => row.NercStandards != "" && x.ReqName == "Standards"));
                    if (standardSubReq.Count() > 0 && !double.TryParse(row.NercStandards, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("NercStandards is not in the correct format"));
                    }
                    var simulationSubReq = certifications.SelectMany(x=>x.CertificationSubRequirements.Where(x => row.NercSimulation != "" && x.ReqName == "Simulations"));
                    if (simulationSubReq.Count() > 0 && !double.TryParse(row.NercSimulation, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("NercSimulation is not in the correct format"));
                    }
                }
                if (recordRelatedErrors.Any())
                {
                    row.ValidationErrors.AddRange(recordRelatedErrors);
                    continue;
                }
                double? totalHours = row.TotalHours == "" ? null : Convert.ToDouble(row.TotalHours);
                bool selfPaced = row.SelfPaced == "" ? false : bool.Parse(row.SelfPaced);
                DateTime effectiveDate = row.EffectiveDate == "" ? DateTime.UtcNow : DateTime.Parse(row.EffectiveDate);
                if (!persistData)
                {
                    var newILA = new ILA(row.IlaName, row.IlaNum,row.IlaDesc, totalHours,provider.Id,deliveryMethod.Id, selfPaced, effectiveDate);
                    var validationResult = _ilaDomainService.Validate(newILA);
                    if (!validationResult.IsValid)
                    {
                        row.ValidationErrors.AddRange(validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                    }
                }
                else if (!row.ValidationErrors.Any())
                {
                    var newILA = new ILA(row.IlaName, row.IlaNum, row.IlaDesc, totalHours, provider.Id,deliveryMethod.Id, selfPaced, effectiveDate);
                    var validationResult = await _ilaDomainService.AddAsync(newILA);
                    if (!validationResult.IsValid)
                    {
                        row.ValidationErrors.AddRange(validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                        continue;
                    }
                    
                    bool isIncludeSimulation = row.NercIsIncludeSimulation == "" ? false : bool.Parse(row.NercIsIncludeSimulation);
                    bool isEmergencyOperatingTraining = row.NercEmergencyOperatingTraining == "" ? false : bool.Parse(row.NercEmergencyOperatingTraining);
                    bool isPartialCred = row.NercIsPartialCred == "" ? false : bool.Parse(row.NercIsPartialCred);
                    
                    foreach (var certification in certifications)
                    {
                        string hours = "";

                        if (certification.CertifyingBody.Name == "NERC") {
                            hours = row.NercTotalCEH;
                        }
                        else if (certification.CertDesc == "Reg") {
                            hours = row.Reg;
                        }
                        else if (certification.CertDesc == "Reg2") {
                            hours = row.Reg2;
                        } 

                        double? cehHours = hours == "" ? null : Convert.ToDouble(hours);
                        var newILACertificationLink = new ILACertificationLink(certification.Id, newILA.Id, isIncludeSimulation, isEmergencyOperatingTraining, isPartialCred, cehHours);
                        newILA.ILACertificationLinks.Add(newILACertificationLink);
                        var standardSubReq = certification.CertificationSubRequirements.Where(x => row.NercStandards != "" && x.ReqName == "Standards");
                        if (standardSubReq.Count() > 0)
                        {
                            double standardHours = Convert.ToDouble(row.NercStandards);
                            var newILACertificationSubReq = new ILACertificationSubRequirementLink(newILACertificationLink.Id, standardSubReq.First().Id, standardHours);
                            newILACertificationLink.ILACertificationSubRequirementLink.Add(newILACertificationSubReq);
                        }
                        var simulationSubReq = certification.CertificationSubRequirements.Where(x => row.NercSimulation != "" && x.ReqName == "Simulations");
                        if (simulationSubReq.Count() > 0)
                        {
                            double simulationHours = Convert.ToDouble(row.NercSimulation);
                            var newILACertificationSubReq = new ILACertificationSubRequirementLink(newILACertificationLink.Id, simulationSubReq.First().Id, simulationHours);
                            newILACertificationLink.ILACertificationSubRequirementLink.Add(newILACertificationSubReq);
                        }
                    }
                    var validationUpdateResult = await _ilaDomainService.UpdateAsync(newILA);
                    if (!validationUpdateResult.IsValid)
                    {
                        row.ValidationErrors.AddRange(validationUpdateResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                        var validationDeleteResult = await _ilaDomainService.DeleteAsync(newILA);
                        if (!validationDeleteResult.IsValid)
                        {
                            row.ValidationErrors.AddRange(validationDeleteResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                        }
                    }
                }

            }
            foreach (var row in csvData)
            {
                foreach (var validationError in row.ValidationErrors)
                {
                    validationError.Error = validationError.Error.DynamicReplaceLabel(labelReplacement);
                }
            }
            return csvData;
        }

        private async Task<List<ImportDatum_Employee_VM>> EmployeeModelToDomainAsync(List<ImportDatum_Employee_VM> csvData, bool persistData, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            foreach (var row in csvData)
            {
                List<ValidationError_VM> recordRelatedErrors = new List<ValidationError_VM>();
                row.ValidationErrors = new List<ValidationError_VM>();
                var person =  (await _personDomainService.FindAsync(x => x.Username == row.Email)).FirstOrDefault();
                var position = (await _positionDomainService.FindAsync(x => x.PositionAbbreviation == row.PosAbbrev && x.PositionNumber.ToString() == row.PositionNum)).FirstOrDefault();
                var organization = (await _organizationDomainService.FindAsync(x => x.Name == row.OrganizationName)).FirstOrDefault();
                var certification = (await _certificationDomainService.FindAsync(x => x.Name == row.CertName)).FirstOrDefault();
                var employeeRecord = (await _employeeDomainService.FindAsync(x => x.EmployeeNumber == row.EmpNum)).FirstOrDefault();

                if (person != null)
                {
                    var employee = (await _employeeDomainService.FindAsync(x => x.PersonId == person.Id)).FirstOrDefault();
                    if (employee != null)
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("Employee already exists for this Person"));
                    }
                }
                if(employeeRecord != null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Employee Number is already in use"));
                }
             
                if (string.IsNullOrEmpty(row.FirstName))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("First Name is required"));
                }
                if (string.IsNullOrEmpty(row.LastName))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Last Name is required"));
                }
                if (string.IsNullOrEmpty(row.Email))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Email is required"));
                }
                if ((row.PosAbbrev != "" || row.PositionNum != ""))
                {
                    if (position == null)
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("Position does not exist"));
                    }
                    if (!DateTime.TryParse(row.PositionStartDate, out _))
                    {
                        recordRelatedErrors.Add(new ValidationError_VM("Position Start Date is required"));
                    }
                }
                if ( row.OrganizationName != "" && organization == null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Organization does not exist"));
                }
                if ( row.CertName != "" && certification == null)
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Certification does not exist"));
                }
                if (string.IsNullOrEmpty(row.CertName) && (!string.IsNullOrEmpty(row.CertNum) || !string.IsNullOrEmpty(row.IssueDate) || !string.IsNullOrEmpty(row.RecertDate) || !string.IsNullOrEmpty(row.CertExpDate)))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("Certification Name is required"));
                }

                if (row.CertName != "" && !DateTime.TryParse(row.IssueDate, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("IssueDate is not in the correct format"));
                }
                if (row.RecertDate != "" && !DateTime.TryParse(row.RecertDate, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("RecertDate is not in the correct format"));
                }
                if (row.CertExpDate != "" && !DateTime.TryParse(row.CertExpDate, out _))
                {
                    recordRelatedErrors.Add(new ValidationError_VM("CertExpDate is not in the correct format"));
                }
                
                if (recordRelatedErrors.Any())
                {
                    row.ValidationErrors.AddRange(recordRelatedErrors);
                    continue;
                }
                
                DateTime? recertDate = row.RecertDate == "" ? null : DateTime.Parse(row.RecertDate);
                DateTime? certExpDate = row.CertExpDate == "" ? null : DateTime.Parse(row.CertExpDate);  
                if (!persistData)
                {
                    if(person == null)
                    {
                        var newPerson = new Person(row.FirstName, row.Middle, row.LastName, row.Email, "");
                        var validationResult = _personDomainService.Validate(newPerson);
                        if (!validationResult.IsValid)
                        {
                            row.ValidationErrors.AddRange(validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                        }
                    }
                    else
                    {
                        var newEmployee = new Employee(person.Id, row.EmpNum, row.Phone);
                        var validationResult = _employeeDomainService.Validate(newEmployee);
                        if (!validationResult.IsValid)
                        {
                            row.ValidationErrors.AddRange(validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                        }
                    }
                }
                else if (persistData && !row.ValidationErrors.Any())
                {
                    var newPerson = person;
                    if (person == null)
                    {
                        newPerson = new Person(row.FirstName, row.Middle, row.LastName, row.Email, "");
                        var personValidationResult = await _personDomainService.AddAsync(newPerson);
                        if (!personValidationResult.IsValid)
                        {
                            row.ValidationErrors.AddRange(personValidationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                            continue;
                        }
                    }

                    var newEmployee = new Employee(newPerson.Id, row.EmpNum, row.Phone);
                    if (organization != null) { newEmployee.LinkOrganization(organization); }
                    if(position != null) { 
                        var empPosition= newEmployee.LinkPosition(position);
                        empPosition.StartDate = DateOnly.Parse(row.PositionStartDate);
                    }
                    if(certification != null) { 
                        DateTime issueDate = DateTime.Parse(row.IssueDate);
                        newEmployee.LinkCerificate(certification,row.CertNum, DateOnly.FromDateTime(issueDate), DateOnly.FromDateTime(recertDate.GetValueOrDefault()), DateOnly.FromDateTime(certExpDate.GetValueOrDefault()));
                    }
                    var validationResult = await _employeeDomainService.AddAsync(newEmployee);
                    if (!validationResult.IsValid)
                    {
                        row.ValidationErrors.AddRange(validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                    }
                }
            }
            foreach (var row in csvData)
            {
                foreach (var validationError in row.ValidationErrors)
                {
                    validationError.Error = validationError.Error.DynamicReplaceLabel(labelReplacement);
                }
            }
            return csvData;
        }

        private async Task<List<ImportDatum_Class_VM>> ClassModelToDomainAsync(List<ImportDatum_Class_VM> csvData, bool persistData, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            var groupedData = csvData.GroupBy(x => new { x.ClassILANum, x.StartDate, x.ClassEndDate, x.InstructorName, x.Location })
           .Select(g => new
           {
               ClassILANum = g.Key.ClassILANum,
               StartDate = g.Key.StartDate,
               ClassEndDate = g.Key.ClassEndDate,
               InstructorName = g.Key.InstructorName,
               Location = g.Key.Location,
               Employees = g.ToList()
           });

            foreach (var classData in groupedData)
            {
                var ilaExist = (await _ilaDomainService.FindAsync(x => x.Number == classData.ClassILANum)).FirstOrDefault();
                var instructor = (await _instructorDomainService.FindAsync(x => x.InstructorName == classData.InstructorName)).FirstOrDefault();
                var location = (await _locationDomainService.FindAsync(x => x.LocName == classData.Location)).FirstOrDefault();
                List<ValidationError_VM> classRecordRelatedErrors = new List<ValidationError_VM>();
                var newClassSchedule = new ClassSchedule();
                classData.Employees.ForEach(x =>
                {
                    x.ValidationErrors = new List<ValidationError_VM>();
                });
                if (ilaExist == null)
                {
                    classRecordRelatedErrors.Add(new ValidationError_VM("ILA does not exist"));
                }
                if (classData.InstructorName != "" && instructor == null)
                {
                    classRecordRelatedErrors.Add(new ValidationError_VM("Instructor does not exist"));
                }
                if (classData.Location != "" && location == null)
                {
                    classRecordRelatedErrors.Add(new ValidationError_VM("Location does not exist"));
                }
                if (!DateTime.TryParse(classData.StartDate, out _))
                {
                    classRecordRelatedErrors.Add(new ValidationError_VM("StartDate is not in the correct format"));
                }
                if (!DateTime.TryParse(classData.ClassEndDate, out _))
                {
                    classRecordRelatedErrors.Add(new ValidationError_VM("ClassEndDate is not in the correct format"));
                }
                if (classRecordRelatedErrors.Any())
                {
                    classData.Employees.ForEach(x => x.ValidationErrors.AddRange(classRecordRelatedErrors));
                }

                foreach (var employeeData in classData.Employees)
                {
                    List<ValidationError_VM> recordRelatedErrors = new List<ValidationError_VM>();
                    if (employeeData.EmpNum != "")
                    {
                        var employee = (await _employeeDomainService.FindAsync(x => x.EmployeeNumber == employeeData.EmpNum)).FirstOrDefault();
                        if (employee == null)
                        {
                            recordRelatedErrors.Add(new ValidationError_VM("No Employee found"));
                        }
                    }
                    if (recordRelatedErrors.Any())
                    {
                        employeeData.ValidationErrors.AddRange(recordRelatedErrors);
                    }
                }

                if (classData.Employees.All(x => x.ValidationErrors.Any())) { continue; }

                DateTime classStartDate = DateTime.Parse(classData.StartDate);
                DateTime classEndDate = DateTime.Parse(classData.ClassEndDate);
                if (!persistData)
                {
                    newClassSchedule = new ClassSchedule(ilaExist.ProviderId, ilaExist.Id, classStartDate, classEndDate, instructor?.Id, location?.Id, false);
                    var validationResult = _classScheduleDomainService.Validate(newClassSchedule);
                    if (!validationResult.IsValid)
                    {
                        classRecordRelatedErrors = validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList();
                        classData.Employees.ForEach(x => x.ValidationErrors.AddRange(classRecordRelatedErrors));
                        continue;
                    }
                }
                else if (persistData && !classRecordRelatedErrors.Any())
                {
                    newClassSchedule = new ClassSchedule(ilaExist.ProviderId, ilaExist.Id, classStartDate, classEndDate, instructor?.Id, location?.Id, true);
                    var validationResult = await _classScheduleDomainService.AddAsync(newClassSchedule);
                    if (!validationResult.IsValid)
                    {
                        classRecordRelatedErrors = validationResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList();
                        classData.Employees.ForEach(x => x.ValidationErrors.AddRange(classRecordRelatedErrors));
                        continue;
                    }
                }
                foreach (var employeeData in classData.Employees)
                {

                    if (persistData && !employeeData.ValidationErrors.Any())
                    {
                        if (employeeData.EmpNum != "")
                        {
                            var employee = (await _employeeDomainService.FindAsync(x => x.EmployeeNumber == employeeData.EmpNum)).FirstOrDefault();
                            if (employee != null)
                            {
                                var classScheduleEmployee = newClassSchedule.LinkEmployee(employee);
                                classScheduleEmployee.EnrollStudent(null);
                                classScheduleEmployee.CompleteClass(classEndDate, employeeData.CompGrade, null);
                                var validationEmployeeResult = await _classScheduleDomainService.UpdateAsync(newClassSchedule);
                                if (!validationEmployeeResult.IsValid)
                                {
                                    employeeData.ValidationErrors.AddRange(validationEmployeeResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList());
                                }
                            }
                        }
                    }
                }

                if (persistData && classData.Employees.All(x => x.ValidationErrors != null && x.ValidationErrors.Any()))
                {
                    var validationDeleteResult = await _classScheduleDomainService.DeleteAsync(newClassSchedule);
                    if (!validationDeleteResult.IsValid)
                    {
                        var deleteErrors = validationDeleteResult.Errors.Select(x => new ValidationError_VM(x.Message)).ToList();
                        classData.Employees.ForEach(x => x.ValidationErrors.AddRange(deleteErrors));
                    }
                }
            }
            var dataToReturn = groupedData.SelectMany(x => x.Employees).ToList();
            foreach (var row in dataToReturn)
            {
                foreach (var validationError in row.ValidationErrors)
                {
                    validationError.Error = validationError.Error.DynamicReplaceLabel(labelReplacement);
                }
            }
            return dataToReturn;
        }
        public  List<ValidationError_VM> CommonValidation(IFormFile file, string[] expectedColumnNames, List<ClientSettings_LabelReplacement> labelReplacement)
        {
            for (int i = 0; i < expectedColumnNames.Length; i++)
            {
                expectedColumnNames[i] = expectedColumnNames[i].DynamicReplaceLabel(labelReplacement);
            }
            List<ValidationError_VM> commonFileErrors = new List<ValidationError_VM>();
            if (file == null)
            {
                commonFileErrors.Add(new ValidationError_VM("Invalid file provided"));
                return commonFileErrors;
            }
            using (Stream stream = file.OpenReadStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string headerLine = reader.ReadLine();
                    if (headerLine != null)
                    {
                        string[] actualColumnNames = headerLine.Split(',');
                        foreach (var expectedColumnName in expectedColumnNames)
                        {
                            if (!actualColumnNames.Any(x => string.Equals(x, expectedColumnName, StringComparison.OrdinalIgnoreCase)))
                            {
                                commonFileErrors.Add(new ValidationError_VM($"Required column '{expectedColumnName}' not found'."));
                            }
                        }
                    }
                    else
                    {
                        commonFileErrors.Add(new ValidationError_VM("CSV file is empty."));
                    }
                }
            }
            return commonFileErrors;
        }

        public async Task<ImportData_DIFSurveyEmployeeResponse_Results_VM> ImportDIFSurveyEmployeeResponseAsync(ImportData_DIFSurveyEmployeeResponse_VM options)
        {
            ImportData_DIFSurveyEmployeeResponse_Results_VM result = new ImportData_DIFSurveyEmployeeResponse_Results_VM();
            result.Data = await DIFEmployeeResponseModelToDomainAsync(options.Data, options.DifSurveyId, true);
            if (options.ReturnFile)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture) { IncludePrivateMembers = true }))
                    {
                        csv.WriteHeader<ImportDatum_DIFSurveyEmployeeResponse_VM>();
                        csv.WriteField("ValidationErrors");
                        csv.NextRecord();

                        foreach (var data in result.Data)
                        {
                            csv.WriteRecord(data);
                            csv.WriteField(string.Join(",", data.ValidationErrors.Select(x => x.Error)));
                            csv.NextRecord();
                        }
                    }

                    result.ReturnedFile = memoryStream.ToArray();
                }
            }
            return result;
        }

        public async Task<ImportData_ILA_Results_VM> ImportILAAsync(ImportData_ILA_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            ImportData_ILA_Results_VM result = new ImportData_ILA_Results_VM();
            result.Data = await ILAModelToDomainAsync(options.Data, true, labelReplacement);
            if (options.ReturnFile)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture) { IncludePrivateMembers = true }))
                    {
                        csv.WriteHeader<ImportDatum_ILA_VM>();
                        csv.WriteField("ValidationErrors");
                        csv.NextRecord();

                        foreach (var data in result.Data)
                        {
                            csv.WriteRecord(data);
                            csv.WriteField(string.Join(",", data.ValidationErrors.Select(x => x.Error)));
                            csv.NextRecord();
                        }
                    }

                    result.ReturnedFile = memoryStream.ToArray();
                }
            }
            return result;
        }

        public async Task<ImportData_Employee_Results_VM> ImportEmployeeAsync(ImportData_Employee_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            ImportData_Employee_Results_VM result = new ImportData_Employee_Results_VM();
            result.Data = await EmployeeModelToDomainAsync(options.Data, true, labelReplacement);
            if (options.ReturnFile)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture) { IncludePrivateMembers = true }))
                    {
                        csv.WriteHeader<ImportDatum_Employee_VM>();
                        csv.WriteField("ValidationErrors");
                        csv.NextRecord();

                        foreach (var data in result.Data)
                        {
                            csv.WriteRecord(data);
                            csv.WriteField(string.Join(",", data.ValidationErrors.Select(x => x.Error)));
                            csv.NextRecord();
                        }
                    }

                    result.ReturnedFile = memoryStream.ToArray();
                }
            }
            return result;
        }

        public async Task<ImportData_Class_Results_VM> ImportClassAsync(ImportData_Class_VM options)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            ImportData_Class_Results_VM result = new ImportData_Class_Results_VM();
            result.Data = await ClassModelToDomainAsync(options.Data, true, labelReplacement);
            if (options.ReturnFile)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture) { IncludePrivateMembers = true }))
                    {
                        csv.WriteHeader<ImportDatum_Class_VM>();
                        csv.WriteField("ValidationErrors");
                        csv.NextRecord();

                        foreach (var data in result.Data)
                        {
                            csv.WriteRecord(data);
                            csv.WriteField(string.Join(",", data.ValidationErrors.Select(x => x.Error)));
                            csv.NextRecord();
                        }
                    }

                    result.ReturnedFile = memoryStream.ToArray();
                }
            }
            return result;
        }


        public async Task<byte[]> GetTemplateAsync(string type)
        {
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
            string templateFilePath = "";
            string difTemplateName = "";
            switch (type.ToLower())
            {
                case "dif survey employee response data":
                    difTemplateName = "DIF Survey Employee Response Data Import Template.csv";
                    templateFilePath = Path.Combine(templatePath, difTemplateName);
                    break;

                case "class data":
                    difTemplateName = "Class Data Import Template.csv";
                    templateFilePath = Path.Combine(templatePath, difTemplateName);
                    break;

                case "employee data":
                    difTemplateName = "Employee Data Import Template.csv";
                    templateFilePath = Path.Combine(templatePath, difTemplateName);
                    break;

                case "ila data":
                    difTemplateName = "ILA Data Import Template.csv";
                    templateFilePath = Path.Combine(templatePath, difTemplateName);
                    break;
            }

            if (File.Exists(templateFilePath))
            {
                var templateFilePathByte = await File.ReadAllBytesAsync(templateFilePath);
                var templateFilePathString = System.Text.Encoding.UTF8.GetString(templateFilePathByte);
                templateFilePathString = templateFilePathString.DynamicReplaceLabel(labelReplacement);
                return System.Text.Encoding.UTF8.GetBytes(templateFilePathString);
            }
            else
            {
                throw new FileNotFoundException("Template file not found.");
            }
        }
    }
}
