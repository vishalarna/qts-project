using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService : Common.Service<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>, IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService
    {
        public ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditService(IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditRepository repository, IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditValidation validation)
            : base(repository, validation)
        {
        }
    }
}
