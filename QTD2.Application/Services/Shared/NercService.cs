using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Exporting.Interfaces;
using QTD2.Infrastructure.Model.Nerc;
using IClassScheduleEmployeeService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IILAService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using System.Text.Json;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Certifications.Factories.Implimentations;
using QTD2.Domain.Services.Core;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Application.Services.Shared
{
    public class NercService : INercService
    {
        Domain.Interfaces.Service.Core.IClassScheduleService _classScheduleService;
        IClassScheduleEmployeeService _classScheduleEmployeeService;
        IILAService _ilaService;
        ICSVExporter _csvExporter;
        private readonly QTD2.Domain.Interfaces.Service.Core.ICertificationService _certificationService;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;

        public NercService(
            Domain.Interfaces.Service.Core.IClassScheduleService classScheduleService,
            IClassScheduleEmployeeService classScheduleEmployeeService,
            IILAService ilaService,
            ICSVExporter csvExporter,
            QTD2.Domain.Interfaces.Service.Core.ICertificationService certificationService,
            ICertificationFulfillmentCalculatorFactory certificationFulfillmentCalculatorFactory)
        {
            _classScheduleService = classScheduleService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _ilaService = ilaService;
            _csvExporter = csvExporter;
            _certificationService = certificationService;
            _certificationFulfillmentCalculatorFactory = certificationFulfillmentCalculatorFactory;
        }

        public async Task<List<CehUploadResultModel>> GetCehUploadAsync(CehUploadGetOptions options)
        {
            var data = new List<CehUploadResultModel>();

            var nercCertificationIds = (await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC")).Select(c => c.Id).ToList();
            var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();

            foreach (var csId in options.ClassScheduleIds) 
            {
                var classScheduleEmployees = await _classScheduleEmployeeService.GetForCEHExportAsync(csId);
                if (classScheduleEmployees.Count() == 0) {
                    var dataRow = new CehUploadResultModel(csId, false);
                    data.Add(dataRow);
                    continue;
                };

                var classSchedule = (await _classScheduleService.FindAsync(cs => cs.Id == csId)).FirstOrDefault();

                var nercCertFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesForDateAsync(
                    classScheduleEmployees.Select(cse => cse.EmployeeId).Distinct().ToList(),
                    nercCertificationIds,
                    classSchedule.EndDateTime);

                foreach (var cse in classScheduleEmployees)
                {
                    var employeeNERCCertFulfillmentStatuses = nercCertFulfillmentStatuses.Where(ncfs => ncfs.EmployeeId == cse.EmployeeId);
                    //Invalid if no NERC certs or no grade entered
                    if (employeeNERCCertFulfillmentStatuses.Count() == 0 || (cse.FinalGrade == null)) 
                    {
                        var dataRow = new CehUploadResultModel(cse.ClassScheduleId,false);
                        data.Add(dataRow);
                        continue;
                    }
                    //Ignore failed CSEs but don't mark invalid
                    if ((cse.FinalGrade ?? "").Trim().ToLower() != "p") 
                    {
                        continue;
                    }
                    foreach (var employeeNERCCertFulfillmentStatus in employeeNERCCertFulfillmentStatuses)
                    {
                        var cseFulfillment = employeeNERCCertFulfillmentStatus.FulfillmentRecords.FirstOrDefault(fs => fs.ClassScheduleEmployeeId == cse.Id); // May not be a fulfillment for this CSE for the given Employee Cert

                        var dataRow = new CehUploadResultModel
                            (
                                cse.ClassSchedule.ILA.Number,
                                cse.ClassSchedule.ILA.IsSelfPaced ? (cse?.CompletionDate.HasValue == true ? cse.CompletionDate.Value : cse.ClassSchedule.EndDateTime) : cse.ClassSchedule.EndDateTime,
                                employeeNERCCertFulfillmentStatus == null ? "NA" : employeeNERCCertFulfillmentStatus.CertificationNumber ?? "NA",
                                cseFulfillment != null ? cseFulfillment.CEHAwarded.ToString() : "0",
                                cseFulfillment != null ? cseFulfillment.CertificationFulfillmentSubRequirements.Where(subReq => subReq.CertificationSubRequirementName == "Standards").Sum(subReq => subReq.AwardedHours).ToString() : "0",
                                cseFulfillment != null ? cseFulfillment.CertificationFulfillmentSubRequirements.Where(subReq => subReq.CertificationSubRequirementName == "Simulations").Sum(subReq => subReq.AwardedHours).ToString() : "0",
                                cse.ClassScheduleId,
                                true
                            );
                        data.Add(dataRow);
                    }
                }
            }
            var validData = data.Where(x => x.IsValid).ToList();
            var invalidResData = data.Where(x => !x.IsValid && validData.Any(m=>m.ClassScheduleId == x.ClassScheduleId));
            var resultData = data.Except(invalidResData).ToList();
            return resultData;
        }
    }
}
