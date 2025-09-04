using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Model.CSE_ILACertLink_PartialCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;

namespace QTD2.Application.Services.Shared
{
    public class CSE_ILACertLink_PartialCreditService : ICSE_ILACertLink_PartialCreditService
    {
        private readonly IClassScheduleEmployee_ILACertificationLink_PartialCreditService _classScheduleEmployee_ILACertificationLink_PartialCreditService;
        private readonly IILADomainService _ilaService;
        private readonly IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService _classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService;
        public CSE_ILACertLink_PartialCreditService(IClassScheduleEmployee_ILACertificationLink_PartialCreditService classScheduleEmployee_ILACertificationLink_PartialCreditService, IILADomainService ilaService, IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService) 
        {
            _classScheduleEmployee_ILACertificationLink_PartialCreditService = classScheduleEmployee_ILACertificationLink_PartialCreditService;
            _ilaService = ilaService;
            _classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService = classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService;
        }

        public async Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetClassScheduleEmployee_ILACertificationLink_PartialCreditByClassEmpIdsAsync(List<int> clsEmpIds)
        {
            return (await _classScheduleEmployee_ILACertificationLink_PartialCreditService.GetByClassScheduleEmployeeIdsAsync(clsEmpIds));
        }

        public async System.Threading.Tasks.Task AddOrUpdateCSE_ILACertLink_PartialCreditHoursAsync(int id,CSE_ILACertPartialCreditCreateUpdateOption options)
        {
            foreach(var option in options.CSE_ILACertPartialCredits)
            {
                var existing = await _classScheduleEmployee_ILACertificationLink_PartialCreditService.GetByClassScheduleEmployeeIdWithSubRequirementAsync(option.ClassScheduleEmployeeId);
                if(existing.Any())
                {
                    foreach (var pc in existing)
                    {
                        if (option.PartialCreditHours == null)
                        {
                            pc.Delete();
                            continue;
                        }
                        else
                        {
                            pc.PartialCreditHours = option.PartialCreditHours;
                        }

                        foreach (var sub in pc.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits)
                        {
                            var match = option.subRequirements.FirstOrDefault(x =>
                                x.Reqname == sub.ILACertificationSubRequirementLink.CertificationSubRequirement.ReqName);

                            if (match?.PartialCreditHours == null)
                            {
                                sub.Delete();
                            }
                            else
                            {
                                sub.PartialCreditHours = match.PartialCreditHours;
                            }
                        }

                        var existingReqNames = pc.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits.Select(x => x.ILACertificationSubRequirementLink.CertificationSubRequirement.ReqName).ToHashSet();

                        var missingSubs = option.subRequirements
                            .Where(x => x.PartialCreditHours != null && !existingReqNames.Contains(x.Reqname)).ToList();

                        foreach (var newSub in missingSubs)
                        {
                            var matchingSubLink = pc.ILACertificationLink.ILACertificationSubRequirementLink
                                .FirstOrDefault(s => s.CertificationSubRequirement.ReqName == newSub.Reqname);

                            if (matchingSubLink != null)
                            {
                                var newSubEntry = new ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit
                                {
                                    ClassScheduleEmployee_ILACertificationLink_PartialCreditId = pc.Id,
                                    ILACertificationSubRequirementLinkId = matchingSubLink.Id,
                                    PartialCreditHours = newSub.PartialCreditHours
                                };

                                pc.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits.Add(newSubEntry);
                            }
                        }
                    }
                    await _classScheduleEmployee_ILACertificationLink_PartialCreditService.BulkUpdateAsync(existing);
                }
                else
                {
                    var ila = (await _ilaService.GetILAsWithCertificationInformationAsync(new List<int> { id })).FirstOrDefault();
                    var nercILACertificationLink = ila?.ILACertificationLinks?.Where(x => x.Certification?.CertifyingBody?.Name == "NERC" && x.IsPartialCreditHours);
                    foreach(var ic in nercILACertificationLink)
                    {
                        var newRecord = new ClassScheduleEmployee_ILACertificationLink_PartialCredit(option.ClassScheduleEmployeeId,ic.Id,option.PartialCreditHours);
                        await _classScheduleEmployee_ILACertificationLink_PartialCreditService.AddAsync(newRecord);
                        foreach (var sub in ic.ILACertificationSubRequirementLink)
                        {
                            var partialCreditHour = option.subRequirements.FirstOrDefault(x => x.Reqname == sub.CertificationSubRequirement.ReqName).PartialCreditHours;
                            var newSubRecord = new ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit(newRecord.Id, sub.Id, partialCreditHour);
                            await _classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService.AddAsync(newSubRecord);
                        }
                    }
                }
            }
        }
    }
}
