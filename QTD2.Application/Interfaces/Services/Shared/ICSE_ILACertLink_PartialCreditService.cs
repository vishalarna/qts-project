using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.CSE_ILACertLink_PartialCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICSE_ILACertLink_PartialCreditService
    {
        Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetClassScheduleEmployee_ILACertificationLink_PartialCreditByClassEmpIdsAsync(List<int> clsEmpIds);
        System.Threading.Tasks.Task AddOrUpdateCSE_ILACertLink_PartialCreditHoursAsync(int id,CSE_ILACertPartialCreditCreateUpdateOption option);
    }
}
